using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/vaultkeeps")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth0;
    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0)
    {
      _vaultKeepsService = vaultKeepsService;
      _auth0 = auth0;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
        vaultKeepData.CreatorId = userInfo.Id;
        VaultKeep newVaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
        return newVaultKeep;
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }






    // [Authorize]
    // [HttpDelete("{vaultKeepId}")]
    // public async Task<ActionResult<VaultKeep>> DeleteVaultKeep(int vaultKeepId)
    // {
    //   try
    //   {
    //     Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
    //     VaultKeep vaultKeep = _vaultKeepsService.DeleteVaultKeep(vaultKeepId, userInfo.Id);
    //     return vaultKeep;
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }


  }
}