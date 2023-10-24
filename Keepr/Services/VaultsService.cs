




namespace Keepr.Services;
public class VaultsService
{
  private readonly VaultsRepository _repo;
  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    Vault newVault = _repo.CreateVault(vaultData);
    return newVault;
  }

  internal Vault GetVaultById(int vaultId, string userId, bool increaseVisits = false)
  {
    Vault foundVault = _repo.GetVaultById(vaultId);
    if (foundVault == null) throw new Exception($"Invalid id {vaultId}");
    if (foundVault.isPrivate == true && foundVault.CreatorId != userId) throw new Exception("That isn't for you!");
    if (increaseVisits && foundVault.CreatorId != userId)
    {
      this.IncreaseVisits(foundVault);
    }
    return foundVault;
  }




  internal Vault UpdateVault(Vault updateData, int vaultId, string userId)
  {
    Vault original = this.GetVaultById(vaultId, userId);
    if (original.CreatorId != userId) throw new Exception("that isn't yours to edit");
    original.Name = updateData.Name != null ? updateData.Name : original.Name;
    original.Description = updateData.Description != null ? updateData.Description : original.Description;
    original.Img = updateData.Img != null ? updateData.Img : original.Img;
    original.isPrivate = updateData.isPrivate ?? original.isPrivate;
    _repo.UpdateVault(original);
    Vault vault = _repo.UpdateVault(original);
    return original;
  }

  internal void IncreaseVisits(Vault vault)
  {
    vault.Visits++;
    _repo.UpdateVault(vault);
  }

  internal Vault DeleteVault(int vaultId, string id)
  {
    Vault vault = this.GetVaultById(vaultId, id);
    if (vault.CreatorId != id) throw new Exception("That's not your vault");
    _repo.DeleteVault(vaultId);
    return vault;
  }


  internal List<Vault> GetProfileVaults(string profileId, string userId)
  {
    List<Vault> vaults = _repo.GetProfileVaults(profileId);
    vaults = vaults.FindAll(vaults => vaults.isPrivate == false || vaults.isPrivate == true && vaults.CreatorId == userId);
    return vaults;
  }

  internal List<Vault> GetAccountVaults(string id)
  {
    List<Vault> vaults = _repo.GetAccountVaults(id);
    return vaults;
  }
}