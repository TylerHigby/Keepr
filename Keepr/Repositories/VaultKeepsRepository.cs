




namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }



    //NOTE - REFERENCE POSTITSHARP-COLLABORATORSREPOSITORY
    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      string sql = @"
          INSERT INTO vaultkeeps
          (creatorId, vaultId, keepId)
          VALUES
          (@creatorId, @vaultId, @keepId);
          SELECT LAST_INSERT_ID()
          ;";
      int lastInsertId = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = lastInsertId;
      return vaultKeepData;
    }



  }
}
