
namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;
    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }

    internal Profile GetProfile(string profileId)
    {
      Profile profile = _repo.GetProfile(profileId);
      if (profile == null)
      {
        throw new Exception("No profile with that Id");
      }
      return profile;
    }
  }
}