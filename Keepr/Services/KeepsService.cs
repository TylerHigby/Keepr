


namespace Keepr.Services;
public class KeepsService
{
  private readonly KeepsRepository _repo;
  private readonly VaultsService _vaultsService;
  public KeepsService(KeepsRepository repo, VaultsService vaultsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep newKeep = _repo.CreateKeep(keepData);
    return newKeep;
  }


  internal List<Keep> GetKeeps()
  {
    List<Keep> keeps = _repo.GetKeeps();
    return keeps;
  }


  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repo.GetKeepById(keepId);
    if (keep == null) throw new Exception($"There's no keep with the id of {keepId}");
    return keep;
  }

  internal Keep UpdateKeep(Keep updateData, string id)
  {
    Keep original = this.GetKeepById(updateData.Id);
    if (original.CreatorId != id)
    {
      throw new Exception("That isn't your keep!");
    }
    original.Name = updateData.Name != null ? updateData.Name : original.Name;
    original.Description = updateData.Description != null ? updateData.Description : original.Description;
    original.Img = updateData.Img != null ? updateData.Img : original.Img;
    Keep keep = _repo.UpdateKeep(original);
    return keep;
  }

  internal Keep DeleteKeep(int keepId, string id)
  {
    Keep keep = this.GetKeepById(keepId);
    if (keep.CreatorId != id) throw new Exception("That isn't yours");
    _repo.DeleteKeep(keepId);
    return keep;
  }

  internal List<Keep> GetKeepsInVault(int vaultId, string userId)
  {
    Vault vault = _vaultsService.GetVaultById(vaultId, userId);
    List<Keep> keeps = _repo.GetKeepsInVault(vaultId);
    return keeps;
  }

  internal List<Keep> GetProfileKeeps(string profileId)
  {
    List<Keep> keeps = _repo.GetProfileKeeps(profileId);
    return keeps;
  }

  internal void IncreaseViews(Keep keep)
  {
    keep.Views++;
    _repo.UpdateKeep(keep);
  }

}