



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

  internal Vault GetVaultById(int vaultId)
  {
    Vault foundVault = _repo.GetVaultById(vaultId);
    return foundVault;
  }



  // //FIXME - 
  // internal Vault UpdateVault(Vault updateData, int vaultId, string userId)
  // {
  //   Vault original = this.GetVaultById(vaultId, userId);
  //   if (original.CreatorId != userId) throw new Exception("that isn't yours to edit");
  //   original.Name = updateData.Name != null ? updateData.Name : original.Name;
  //   original.Description = updateData.Description != null ? updateData.Description : original.Description;
  //   original.Img = updateData.Img != null ? updateData.Img : original.Img;
  //   original.isPrivate = updateData.isPrivate ?? original.isPrivate;
  //   _repo.UpdateVault(original);
  //   return original;
  // }

  // //FIXME - 
  //   internal Vault DeleteVault(int vaultId, string id)
  //   {
  //     Vault vault = this.GetVaultById(vaultId);
  //     if (vault.CreatorId != id) throw new Exception("That's not your vault");
  //     _repo.DeleteVault(vaultId);
  //     return vault;
  //   }





}