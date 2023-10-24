namespace Keepr.Controllers;
[ApiController]
[Route("api/keeps")]
public class KeepsController : ControllerBase
{
  private readonly KeepsService _keepsService;
  private readonly Auth0Provider _auth0;
  public KeepsController(KeepsService keepsService, Auth0Provider auth0)
  {
    _keepsService = keepsService;
    _auth0 = auth0;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep newKeep = _keepsService.CreateKeep(keepData);
      return newKeep;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsService.GetKeeps();
      return keeps;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetKeepById(int keepId)
  {
    try
    {
      Keep keep = _keepsService.GetKeepById(keepId);
      return keep;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{keepId}")]
  public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep updateData, int keepId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      updateData.Id = keepId;
      Keep keep = _keepsService.UpdateKeep(updateData, userInfo.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{keepId}")]
  public async Task<ActionResult<Keep>> DeleteKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.DeleteKeep(keepId, userInfo.Id);
      return keep;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}