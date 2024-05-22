namespace Voyager.Api.Views;

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Course { get; set; }
    public string Cuisine { get; set; }
    public string MainIngredient { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public string Url { get; set; }
    public string UrlHost { get; set; }
    public int PrepTime { get; set; }
    public int CookTime { get; set; }
    public int TotalTime { get; set; }
    public int Servings { get; set; }
    public string Ingredients { get; set; }
    public string Directions { get; set; }
    public string Tags { get; set; }
    public string Rating { get; set; }
    public string PublicUrl { get; set; }
    public string PhotoUrl { get; set; }
    public string Private { get; set; }
    public string NutritionalScoreGeneric { get; set; }
    public string Fat { get; set; }
    public string Cholesterol { get; set; }
    public string Sodium { get; set; }
    public string Sugar { get; set; }
    public string Carbohydrate { get; set; }
    public string Fiber { get; set; }
    public string Protein { get; set; }
    public string Cost { get; set; }
}
