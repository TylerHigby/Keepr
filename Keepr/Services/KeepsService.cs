


namespace Keepr.Services;
public class KeepsService
{
  private readonly KeepsRepository _repo;
  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
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

  internal Keep UpdateKeep(Keep updateData)
  {
    Keep original = this.GetKeepById(updateData.Id);
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
}