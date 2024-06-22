using AceInternship.RestApi.Database;
using AceInternship.RestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AceInternship.RestApi.Controllers
{
	//https://localhost:3000 ==> domain url
	//api/food ==> endpoint
	[Route("api/[controller]")]
	[ApiController]
	public class FoodController : ControllerBase
	{
		private readonly AppDbContext _appDbcontext;

		public FoodController()
		{
			_appDbcontext = new AppDbContext();
		}

		[HttpGet]
		public IActionResult Read()
		{
			var lst = _appDbcontext.Food.ToList();
			return Ok(lst);
		}
		[HttpGet ("{id}")]
		public IActionResult Edit(int id)
		{
			var item = _appDbcontext.Food.FirstOrDefault(x => x.FoodId == id);
			if (item is null)
			{
				return NotFound("No data found.");
			}
			return Ok(item);
		}
		[HttpPost]
		public IActionResult Create(FoodModel Food)
		{
			_appDbcontext.Food.Add(Food);

			var result = _appDbcontext.SaveChanges();
			string message = result > 0 ? "Saving Successful." : "Saving Failed.";

			return Ok(message);
		}
		[HttpPut ("{id}")]
		public IActionResult Update(int id, FoodModel Food)
		{
			var item = _appDbcontext.Food.FirstOrDefault(x => x.FoodId == id);
			if (item is null)
			{
				return NotFound("No data found.");
			}
			item.FoodName = Food.FoodName;
			item.FoodType = Food.FoodType;
			item.FoodPrice = Food.FoodPrice;

			var result = _appDbcontext.SaveChanges();
			string message = result > 0 ? "Update Successful." : "Update Failed.";
			return Ok(message);  
		}
		[HttpPatch("{id}")]
		public IActionResult Patch(int id, FoodModel Food)
		{
			var item = _appDbcontext.Food.FirstOrDefault(x => x.FoodId == id);
			if (item is null)
			{
				return NotFound("No data found.");
			}
			if (!string.IsNullOrEmpty(Food.FoodName))
			{
				item.FoodName = Food.FoodName;
			}
			if (!string.IsNullOrEmpty(Food.FoodType))
			{
				item.FoodType = Food.FoodType;
			}
			if (!string.IsNullOrEmpty(Food.FoodPrice))
			{
				item.FoodPrice = Food.FoodPrice;
			}

			var result = _appDbcontext.SaveChanges();
			string message = result > 0 ? "Update Successful." : "Update Failed.";
			return Ok(message);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var item = _appDbcontext.Food.FirstOrDefault(x => x.FoodId == id);
			if (item is null)
			{
				return NotFound("No data found.");
			}
			_appDbcontext.Food.Remove(item);
			var result = _appDbcontext.SaveChanges();
			string message = result > 0 ? "Delete Successful." : "Delete Failed.";
			return Ok(message);
		}
	}
}
