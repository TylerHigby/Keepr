namespace Keepr.Models;
public class Vault
{
  public int Id { get; set; }
  public string CreatorId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public bool? isPrivate { get; set; }
  public int Visits { get; set; }
  public Account Creator { get; set; }
}