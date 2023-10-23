


namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsRepository _vrepo;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vrepo)
    {
      _repo = repo;
      _vrepo = vrepo;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
      return vaultKeep;
    }


    // internal List<AccountVaultKeepsViewModel> GetVaultKeepsByVaultId(int vaultId)
    // {
    //   List<AccountVaultKeepsViewModel> vaultKeeps = _repo.GetVaultKeepsByVaultId(vaultId);
    //   return vaultKeeps;
    // }


    // internal void DeleteVaultKeep(int vaultKeepId, string id)
    // {
    //   VaultKeep foundVaultKeep = _repo.getVaultKeepById(vaultKeepId);
    //   int rows = _repo.DeleteVaultKeep(vaultKeepId);
    // }

  }
}