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






}