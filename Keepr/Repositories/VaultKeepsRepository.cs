



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

    // internal List<AltKeep> GetKeepsInVault(int vaultId)
    // {
    //   string sql = @"
    //         SELECT
    //         vaultKeeps.*,
    //         keeps.*,
    //         accounts.*
    //         FROM vaultKeeps
    //         JOIN accounts ON accounts.id = keeps.creatorId
    //         WHERE vaultKeeps.vaultId = vaultId
    //         ;";
    //   List<AltKeep> vaultKeeps = _db.Query<VaultKeep, AltKeep, Profile, AltKeep>(sql, (vaultKeep, altKeep, profile) =>
    //   {
    //     altKeep.VaultKeepId = vaultKeep.Id;
    //     altKeep.Creator = profile;
    //     return altKeep;
    //   }, new { vaultId }).ToList();
    //   return vaultKeeps;
    // }


    // internal VaultKeep getVaultKeepById(int vaultKeepId)
    // {
    //   string sql = @"
    //   SELECT
    //   *
    //   FROM vaultkeeps
    //   WHERE id = @vaultKeepId
    //   ;";
    //   VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
    //   return vaultKeep;
    // }

    // internal List<AccountVaultKeepsViewModel> GetVaultKeepsByVaultId(int vaultId)
    // {
    //   string sql = @"
    //         SELECT
    //         vk.*,
    //         act.*
    //         FROM vaultkeeps vk
    //         JOIN accounts act ON act.id = vk.accountId
    //         ;";
    // }
    // internal int DeleteVaultKeep(int vaultKeepId)
    // {
    //   string sql = @"
    //         DELETE FROM vaultkeeps
    //         WHERE id = @vaultKeepId
    //         LIMIT 1
    //         ;";
    //   int rows = _db.Execute(sql, new { vaultKeepId });
    //   return rows;
    // }




  }
}
