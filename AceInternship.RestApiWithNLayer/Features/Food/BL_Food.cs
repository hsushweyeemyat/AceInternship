using Microsoft.EntityFrameworkCore;

namespace AceInternship.RestApiWithNLayer.Features.Food
{
	public class BL_Food
	{
		private readonly DA_Food _daFood;

		public BL_Food()
		{
			_daFood = new DA_Food();
		}
		public List<FoodModel> GetBlogs()
		{
			var lst = _daFood.GetBlogs();
			return lst;
		}
		public FoodModel GetBlog(int id)
		{
			var item = _daFood.GetBlog(id);
			return item;
		}
		public int CreateFood(FoodModel requestModel)
		{
			var result = _daFood.CreateFood(requestModel);
			return result;
		}
		public int UpdateFood(int id, FoodModel requestModel)
		{
			var result = _daFood.UpdateFood(id, requestModel);
			return result;
		}
		public int PatchFood(int id, FoodModel reequestModel)
		{
			var result = _daFood.PatchFood(id, reequestModel);
			return result;
		}
		public int DeleteFood(int id)
		{
			var result = _daFood.DeleteFood(id);
			return result;
		}
	}
}