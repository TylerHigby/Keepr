namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/profiles")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth0;
    public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService, Auth0Provider auth0)
    {
      _profilesService = profilesService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
      _auth0 = auth0;
    }

    [HttpGet("{profileId}")]
    public ActionResult<Profile> GetProfile(string profileId)
    {
      try
      {
        Profile profile = _profilesService.GetProfile(profileId);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
    {
      try
      {
        List<Keep> keeps = _keepsService.GetProfileKeeps(profileId);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("{profileId}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetProfileVaults(string profileId)
    {
      try
      {
        Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
        List<Vault> vaults = _vaultsService.GetProfileVaults(profileId, userInfo?.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }





  }
}