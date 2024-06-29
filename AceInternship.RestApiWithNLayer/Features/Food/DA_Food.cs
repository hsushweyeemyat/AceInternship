namespace AceInternship.RestApiWithNLayer.Features.Food
{
	//DataAccess
	public class DA_Food
	{
		private readonly AppDbContext _context;

		public DA_Food()
		{
			_context =new AppDbContext();
		}
		public List<FoodModel> GetBlogs()
		{
			var lst = _context.Food.ToList();
			return lst;
		}
		public FoodModel GetBlog(int id)
		{
			var item = _context.Food.FirstOrDefault(x => x.FoodId == id);
			return item;
		}
		public int CreateFood(FoodModel requestModel)
		{
			_context.Food.Add(requestModel);
			var result = _context.SaveChanges();
			return result;
		}
		public int UpdateFood(int id, FoodModel requestModel)
		{
			var item = _context.Food.FirstOrDefault(x => x.FoodId == id);
			if (item is null) return 0;

			item.FoodName = requestModel.FoodName;
			item.FoodType = requestModel.FoodType;
			item.FoodPrice = requestModel.FoodPrice;

			var result = _context.SaveChanges();
			return result;
		}
		public int PatchFood(int id, FoodModel requestModel)
		{var item = _context.Food.FirstOrDefault(x => x.FoodId == id);

			if (item is null) return 0;
			if (!string.IsNullOrEmpty(requestModel.FoodName)) item.FoodName = requestModel.FoodName;
			if (!string.IsNullOrEmpty(requestModel.FoodType)) item.FoodType = requestModel.FoodType;
			if (!string.IsNullOrEmpty(requestModel.FoodPrice)) item.FoodPrice = requestModel.FoodPrice;

			var result = _context.SaveChanges();
			return result;
		}
		public int DeleteFood(int id)
		{
			var item = _context.Food.FirstOrDefault(x => x.FoodId == id);
			if (item is null) return 0;

			_context.Remove(item);
			var result = _context.SaveChanges();
			return result;
		}
	}
}
