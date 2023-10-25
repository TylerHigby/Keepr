



namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    private readonly VaultsRepository _vrepo;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, VaultsRepository vrepo)
    {
      _repo = repo;
      _vaultsService = vaultsService;
      _vrepo = vrepo;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
      return vaultKeep;
    }


  }
}