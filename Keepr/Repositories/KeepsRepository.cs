






using System.Collections.Immutable;

namespace Keepr.Repositories;
public class KeepsRepository
{
  private readonly IDbConnection _db;
  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
        INSERT INTO keeps
        (creatorId, name, description, img, views, kept)
        VALUES
        (@creatorId, @name, @description, @img, @views, @kept);
        SELECT
        a.*,
        k.*
        FROM keeps k
        JOIN accounts a
        ON a.id = k.creatorId
        WHERE k.id = LAST_INSERT_ID()
        ;";
    Keep newKeep = _db.Query<Account, Keep, Keep>(sql, (account, keep) =>
    {
      keep.Creator = account;
      return keep;
    }, keepData).FirstOrDefault();
    return newKeep;
  }


  internal List<Keep> GetKeeps()
  {
    string sql = @"
        SELECT
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        ;";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }


  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
        SELECT
        k.*,
        a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.id = @keepId
        ;";
    Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { keepId }).FirstOrDefault();
    return keep;
  }

  internal Keep UpdateKeep(Keep original)
  {
    string sql = @"
        UPDATE keeps
        SET
        name = @name,
        description = @description,
        img = @img,
        views = @views,
        kept = @kept
        WHERE id = @id;
        SELECT * FROM keeps WHERE id = @id;
        ;";
    Keep keep = _db.Query<Keep>(sql, original).FirstOrDefault();
    return keep;
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId";
    _db.Execute(sql, new { keepId });
  }

  internal List<VaultedKeep> GetKeepsInVault(int vaultId)
  {
    string sql = @"
        SELECT
        vk.*,
        keeps.*,
        acc.*
        FROM vaultkeeps vk
        JOIN keeps ON vk.keepId = keeps.Id
        JOIN accounts acc ON acc.id = keeps.creatorId
        WHERE vk.vaultId = @vaultId
        ;";
    List<VaultedKeep> keeps = _db.Query<VaultKeep, VaultedKeep, Account, VaultedKeep>(sql, (vaultKeep, vaultedKeep, account) =>
    {
      vaultedKeep.vaultKeepId = vaultKeep.Id;
      vaultedKeep.Creator = account;
      return vaultedKeep;
    }, new { vaultId }).ToList();
    return keeps;
  }


  internal List<Keep> GetProfileKeeps(string profileId)
  {
    string sql = @"
      SELECT
      *
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE creatorId = @profileId
      ;";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(
          sql,
          (keep, profile) =>
          {
            keep.Creator = profile;
            return keep;
          }
          , new { profileId }).ToList();
    return keeps;
  }
}

