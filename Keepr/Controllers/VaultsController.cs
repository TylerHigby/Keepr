namespace Keepr.Controllers;
[ApiController]
[Route("api/vaults")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vaultsService;
  private readonly Auth0Provider _auth0;
  public VaultsController(VaultsService vaultsService, Auth0Provider auth0)
  {
    _vaultsService = vaultsService;
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
  public ActionResult<Vault> GetVaultById(int vaultId)
  {
    try
    {
      Vault vault = _vaultsService.GetVaultById(vaultId);
      return vault;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  //FIXME - 
  // [Authorize]
  // [HttpPut("{vaultId}")]
  // public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault updateData, int vaultId)
  // {
  //   try
  //   {
  //     Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
  //     Vault edited = _vaultsService.UpdateVault(updateData, vaultId, userInfo.Id);
  //     return Ok(edited);
  //   }
  //   catch (Exception e)
  //   {
  //     return BadRequest(e.Message);
  //   }
  // }


}