using AceInternship.RestApiWithNLayer.Models;

namespace AceInternship.RestApiWithNLayer.Features.Food
{
	[Route("api/[controller]")]
	[ApiController]
	public class FoodController : ControllerBase
	{
		private readonly BL_Food _blFood;
		public  FoodController() 
		{
			_blFood = new BL_Food();
		}

		[HttpGet]
		public IActionResult Read()
		{
			var lst = _blFood.GetBlogs();
			return Ok(lst);
		}

		[HttpGet("{id}")]
		public IActionResult Edit(int id)
		{
			var item = _blFood.GetBlog(id);
			if (item is null)
			{
				return NotFound("No data found.");
			}
			return Ok(item);
		}

		[HttpPost]
		public IActionResult Create(FoodModel food)
		{
			var result = _blFood.CreateFood(food);
			string message = result > 0 ? "Saving Successful." : "Saving Failed.";

			return Ok(message);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, FoodModel food)
		{
			var item = _blFood.GetBlog(id);
			if (item is null)
			{
				return NotFound("No data found.");
			}
			var result = _blFood.UpdateFood(id, food);

			string message = result > 0 ? "Update Successful." : "Update Failed.";
			return Ok(message);
		}

		[HttpPatch("{id}")]
		public IActionResult Patch(int id, FoodModel Food)
		{
			var item = _blFood.GetBlog(id);
			if (item is null)
			{
				return NotFound("No data found.");
			}

			var result = _blFood.PatchFood(id, Food);
			string message = result > 0 ? "Update Successful." : "Update Failed.";
			return Ok(message);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var item = _blFood.GetBlog(id);
			if (item is null)
			{
				return NotFound("No data found.");
			}
			var result = _blFood.DeleteFood(id);
			string message = result > 0 ? "Delete Successful." : "Delete Failed.";
			return Ok(message);
		}
	}
}
