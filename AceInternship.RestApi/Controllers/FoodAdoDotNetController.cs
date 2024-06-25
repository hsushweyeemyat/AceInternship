using AceInternship.RestApi.Model;
using AceInternship.RestApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace AceInternship.RestApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FoodAdoDotNetController : ControllerBase
	{
		[HttpGet]
		public IActionResult GetFood()
		{
			string query = "select * from Tbl_Food";
			SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

			connection.Open();
			SqlCommand cmd = new SqlCommand(query, connection);
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			adapter.Fill(dt);
			connection.Close();

			//List<BlogModel> lst = new List<BlogModel>();
			//foreach (DataRow dr in dt.Rows)
			//{
			//    //BlogModel blog = new BlogModel();
			//    //blog.BlogId = Convert.ToInt32(dr["BlogId"]);
			//    //blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
			//    //blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
			//    //blog.BlogContent = Convert.ToString(dr["BlogContent"]);

			//    BlogModel blog = new BlogModel
			//    {
			//        BlogId = Convert.ToInt32(dr["BlogId"]),
			//        BlogTitle = Convert.ToString(dr["BlogTitle"]),
			//        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
			//        BlogContent = Convert.ToString(dr["BlogContent"])
			//    };
			//    lst.Add(blog);
			//}

			List<FoodModel> lst = dt.AsEnumerable().Select(dr => new FoodModel
			{
				FoodId = Convert.ToInt32(dr["FoodId"]),
				FoodName = Convert.ToString(dr["FoodName"]),
				FoodType = Convert.ToString(dr["FoodType"]),
				FoodPrice = Convert.ToString(dr["FoodPrice"])
			}).ToList();
			return Ok(lst);
		}
		[HttpGet("{id}")]
		public IActionResult EditFood(int id)
		{
			string query = "select * from Tbl_Food where FoodId = @FoodId";

			SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			connection.Open();

			SqlCommand cmd = new SqlCommand(query, connection);
			cmd.Parameters.AddWithValue("@FoodId", id);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			sqlDataAdapter.Fill(dt);

			connection.Close();
			if (dt.Rows.Count == 0)
			{
				return NotFound("No Data Found!!");
			}
			DataRow dr = dt.Rows[0];
			var item = new FoodModel
			{
				FoodId = Convert.ToInt32(dr["FoodId"]),
				FoodName = Convert.ToString(dr["FoodName"]),
				FoodType = Convert.ToString(dr["FoodType"]),
				FoodPrice = Convert.ToString(dr["FoodPrice"])
			};
			return Ok(item);
		}
		[HttpPost]
		public IActionResult CreateFood(FoodModel food)
		{
			string query = @"INSERT INTO [dbo].[Tbl_Food]
           ([FoodName]
           ,[FoodType]
           ,[FoodPrice])
     VALUES
           (@FoodName
           ,@FoodType
           ,@FoodPrice)";
			SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			connection.Open();

			SqlCommand cmd = new SqlCommand(query, connection);
			cmd.Parameters.AddWithValue("@FoodName", food.FoodName);
			cmd.Parameters.AddWithValue("@FoodType", food.FoodType);
			cmd.Parameters.AddWithValue("@FoodPrice", food.FoodPrice);
			int result = cmd.ExecuteNonQuery();
			connection.Close();

			string message = result > 0 ? "Saving Successful." : "Saving Failed.";
			return Ok(message);
		}
		[HttpPut("{id}")]
		public IActionResult UpdateFood(int id, FoodModel food)
		{
			string query = @"UPDATE [dbo].[Tbl_Food]
   SET [FoodName] = @FoodName
      ,[FoodType] = @FoodType
      ,[FoodPrice] = @FoodPrice
 WHERE FoodId = @FoodId";

			SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			connection.Open();
			food.FoodId = id;
			SqlCommand cmd = new SqlCommand(query, connection);
			cmd.Parameters.AddWithValue("@FoodId", id);
			cmd.Parameters.AddWithValue("@FoodName", food.FoodName);
			cmd.Parameters.AddWithValue("@FoodType", food.FoodType);
			cmd.Parameters.AddWithValue("@FoodPrice", food.FoodPrice);
			int result = cmd.ExecuteNonQuery();

			connection.Close();

			string message = result > 0 ? "Update Successful." : "Update Failed.";
			return Ok(message);
		}
		[HttpPatch("{id}")]
		public IActionResult PatchFood(int id, FoodModel food)
		{
			// Build the conditions string dynamically
			List<string> conditionsList = new List<string>();

			if (!string.IsNullOrEmpty(food.FoodName))
			{
				conditionsList.Add("[FoodName] = @FoodName");
			}
			if (!string.IsNullOrEmpty(food.FoodType))
			{
				conditionsList.Add("[FoodType] = @FoodType");
			}
			if (food.FoodPrice != null)
			{
				conditionsList.Add("[FoodPrice] = @FoodPrice");
			}

			if (conditionsList.Count == 0)
			{
				return NotFound("No data to update.");
			}

			// Join the conditions list into a single string separated by commas
			string conditions = string.Join(", ", conditionsList);

			// Build the query string
			string query = $@"UPDATE [dbo].[Tbl_Food]
                      SET {conditions}
                      WHERE FoodId = @FoodId";

			// Update the FoodId in the model
			food.FoodId = id;

			SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
				connection.Open();

				SqlCommand cmd = new SqlCommand(query, connection);
					cmd.Parameters.AddWithValue("@FoodId", id);

					if (!string.IsNullOrEmpty(food.FoodName))
					{
						cmd.Parameters.AddWithValue("@FoodName", food.FoodName);
					}
					if (!string.IsNullOrEmpty(food.FoodType))
					{
						cmd.Parameters.AddWithValue("@FoodType", food.FoodType);
					}
					if (food.FoodPrice != null)
					{
						cmd.Parameters.AddWithValue("@FoodPrice", food.FoodPrice);
					}

					int result = cmd.ExecuteNonQuery();
					string message = result > 0 ? "Update Successful." : "Update Failed.";
					return Ok(message);
		}

		/*public IActionResult PatchFood(int id, FoodModel food)
		{
			string conditions = string.Empty;

			if (!string.IsNullOrEmpty(food.FoodName))
			{
				conditions += "[FoodName] = @FoodName, ";
			}
			if (!string.IsNullOrEmpty(food.FoodType))
			{
				conditions += "[FoodType] = @FoodType, ";
			}
			if (!string.IsNullOrEmpty(food.FoodPrice))
			{
				conditions += "[FoodPrice] = @FoodPrice, ";
			}
			if (conditions.Length == 0)
			{
				return NotFound("No data to update.");
			}
			String query = $@"string query = $@""UPDATE [dbo].[Tbl_Food]
  SET {conditions}
WHERE FoodId = @FoodId";
			string condition = string.Join(",", conditions);
			food.FoodId = id;

			SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			connection.Open();

			SqlCommand cmd = new SqlCommand(query, connection);
			cmd.Parameters.AddWithValue("@FoodId", id);

			if (!string.IsNullOrEmpty(food.FoodName))
			{
				cmd.Parameters.AddWithValue("@FoodName", food.FoodName);
			}
			if (!string.IsNullOrEmpty(food.FoodType))
			{
				cmd.Parameters.AddWithValue("@FoodType", food.FoodType);
			}
			if (!string.IsNullOrEmpty(food.FoodPrice))
			{
				cmd.Parameters.AddWithValue("@FoodPrice", food.FoodPrice);
			}
			int result = cmd.ExecuteNonQuery();
			connection.Close();
			string message = result > 0 ? "Update Successful." : "Update Failed.";
			return Ok(message);
		}*/
		[HttpDelete("{id}")]
		public IActionResult DeleteFood(int id)
		{
			string query = @"DELETE FROM [dbo].[Tbl_Food]
      WHERE FoodId = @FoodId";
			SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			connection.Open();
			SqlCommand cmd = new SqlCommand (query, connection);
			cmd.Parameters.AddWithValue("@FoodId", id);
			int result = cmd.ExecuteNonQuery();

			connection.Close();
			string message = result > 0 ? "Delete Successful." : "Delete Failed.";
			return Ok(message);
		}
	}
}
