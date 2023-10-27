





namespace Keepr.Repositories;
public class VaultsRepository
{
  private readonly IDbConnection _db;
  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    if (vaultData.isPrivate == null)
    {
      vaultData.isPrivate = false;
    }
    string sql = @"
        INSERT INTO vaults
        (creatorId, name, description, img, isPrivate)
        VALUES
        (@creatorId, @name, @description, @img, @isPrivate);
        SELECT
        a.*,
        v.*
        FROM vaults v
        JOIN accounts a 
        ON a.id = v.creatorId
        WHERE v.id = LAST_INSERT_ID()
        ;";
    Vault newVault = _db.Query<Account, Vault, Vault>(sql, (account, vault) =>
    {
      vault.Creator = account;
      return vault;
    }, vaultData).FirstOrDefault();
    return newVault;
  }


  internal Vault GetVaultById(int vaultId)
  {
    string sql = @"
        SELECT
        v.*,
        a.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.id = @vaultId
        ;";
    Vault foundVault = _db.Query<Vault, Account, Vault>(sql, (vault, creator) =>
    {
      vault.Creator = creator;
      return vault;
    }, new { vaultId }).FirstOrDefault();
    return foundVault;
  }


  internal Vault UpdateVault(Vault original)
  {
    string sql = @"
        UPDATE vaults
        SET
        name = @name,
        description = @description,
        img = @img,
        isPrivate = @isPrivate
        WHERE id = @id;
        SELECT * FROM vaults WHERE id = @id;
    ;";
    Vault vault = _db.Query<Vault>(sql, original).FirstOrDefault();
    return vault;
  }


  internal void DeleteVault(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE id = @vaultId";
    int rowsAffected = _db.Execute(sql, new { vaultId });
  }

  internal List<Vault> GetProfileVaults(string profileId)
  {
    string sql = @"
        SELECT
        *
        FROM vaults
        WHERE creatorId = @profileId
        ;";
    List<Vault> vaults = _db.Query<Vault>(sql, new { profileId }).ToList();
    return vaults;
  }

  internal List<Vault> GetAccountVaults(string id)
  {
    string sql = @"
        SELECT
        *
        FROM
        vaults
        WHERE creatorId = @id
        ;";
    List<Vault> vaults = _db.Query<Vault>(sql, new { id }).ToList();
    return vaults;
  }
}



