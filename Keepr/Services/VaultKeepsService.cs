




namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;
    private readonly VaultsRepository _vrepo;
    private readonly KeepsRepository _krepo;
    public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService, VaultsRepository vrepo, KeepsRepository krepo)
    {
      _repo = repo;
      _vaultsService = vaultsService;
      _keepsService = keepsService;
      _vrepo = vrepo;
      _krepo = krepo;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
      if (vault.CreatorId != vaultKeepData.CreatorId)
      {
        throw new Exception("Unauthorized");
      }
      List<VaultKeep> vaultKeeps = _repo.GetVaultKeeps();
      vaultKeeps.ForEach(vaultKeep =>
      {
        if (vaultKeep.KeepId == vaultKeepData.KeepId && vaultKeep.VaultId == vaultKeepData.VaultId)
        {
          throw new Exception("You've already added that to your vault.");
        }
      });
      Keep keep = _keepsService.GetKeepById(vaultKeepData.KeepId);

      keep.Kept++;
      _krepo.UpdateKeep(keep);

      VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
      return vaultKeep;
    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
      VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
      if (vaultKeep == null)
      {
        throw new Exception($"Invalid Id: {vaultKeepId}");
      }
      return vaultKeep;
    }
    internal void DeleteVaultKeep(int vaultKeepId, string id)
    {
      VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
      if (vaultKeep.CreatorId != id)
      {
        throw new Exception("That doesn't belong to you");
      }
      _repo.DeleteVaultKeep(vaultKeepId);
    }
  }
}