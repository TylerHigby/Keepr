namespace Keepr.Controllers;
[ApiController]
[Route("api/vaults")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;
  private readonly VaultKeepsService _vaultKeepsService;
  private readonly Auth0Provider _auth0;
  public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService, KeepsService keepsService, Auth0Provider auth0)
  {
    _vaultsService = vaultsService;
    _vaultKeepsService = vaultKeepsService;
    _keepsService = keepsService;
    _auth0 = auth0;
  }


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo.Id;
      Vault newVault = _vaultsService.CreateVault(vaultData);
      return newVault;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id, true);
      return vault;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize]
  [HttpPut("{vaultId}")]
  public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault updateData, int vaultId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.UpdateVault(updateData, vaultId, userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [Authorize]
  [HttpDelete("{vaultId}")]
  public async Task<ActionResult<Vault>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.DeleteVault(vaultId, userInfo.Id);
      return vault;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<Keep>>> GetKeepsInVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _keepsService.GetKeepsInVault(vaultId, userInfo?.Id);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}