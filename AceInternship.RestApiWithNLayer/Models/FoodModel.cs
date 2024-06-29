namespace AceInternship.RestApiWithNLayer.Models;
[Table("Tbl_Food")]
public class FoodModel
{
	[Key]
	public int FoodId { get; set; }
	public string? FoodName { get; set; }
	public string? FoodType { get; set; }
	public string? FoodPrice { get; set; }
}
