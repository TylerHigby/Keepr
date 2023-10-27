







namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }



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



    internal List<VaultKeep> GetVaultKeeps()
    {
      string sql = @"
        SELECT
        *
        FROM vaultkeeps
        ;";

      List<VaultKeep> vaultkeeps = _db.Query<VaultKeep>(sql).ToList();
      return vaultkeeps;
    }


    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
      string sql = @"
            SELECT
            * 
            FROM vaultkeeps
            WHERE id = @vaultKeepId
            ;";
      VaultKeep vaultkeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
      return vaultkeep;
    }


    internal void DeleteVaultKeep(int vaultKeepId)
    {
      string sql = @"
            DELETE FROM vaultkeeps
            WHERE id = @vaultKeepId
            ;";
      _db.Execute(sql, new { vaultKeepId });
    }




  }
}