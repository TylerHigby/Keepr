
namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;
    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetProfile(string profileId)
    {
      string sql = @"
          SELECT
          *
          FROM accounts
          WHERE accounts.id = @profileId
          ;";
      Profile profile = _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
      return profile;
    }
  }
}