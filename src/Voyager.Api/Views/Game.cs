namespace Voyager.Api.Views;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Genre { get; set; }
    public List<string> Developers { get; set; }
    public List<string> Publishers { get; set; }
    public Dictionary<string, string> ReleaseDates { get; set; }
}
