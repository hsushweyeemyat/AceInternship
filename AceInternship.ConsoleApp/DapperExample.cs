using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceInternship.ConsoleApp
{
	public class DapperExample
	{
		public void Run()
		{
			//Read();
			//Edit(1);
			//Create("Test Title", "Test Author", "Test Content");
			//Update(12,"eeee Title", "Test Author", "Test Content");
			Delete(11);
		}
		private void Read() 
		{
			using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			List<BlogDto> lst = db.Query<BlogDto>("Select * From Tbl_Blog").ToList();

			foreach (BlogDto item in lst)
			{
				Console.WriteLine(item.BlogId);
				Console.WriteLine(item.BlogTitle);
				Console.WriteLine(item.BlogAuthor);
				Console.WriteLine(item.BlogContent);
				Console.WriteLine("-----------------------------");
			}
		}
		private void Edit(int id)
		  {
			using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			var item = db.Query<BlogDto>("Select * From Tbl_Blog where BlogId = @BlogId", new BlogDto { BlogId = id}).FirstOrDefault();
			if(item is null)
			{
				Console.WriteLine("No data found.");
				return;
			}

			Console.WriteLine(item.BlogId);
			Console.WriteLine(item.BlogTitle);
			Console.WriteLine(item.BlogAuthor);
			Console.WriteLine(item.BlogContent);

		}
		private void Create(string title, string author,string content)
		{
			var item = new BlogDto
			{
				BlogTitle = title,
				BlogAuthor = author,
				BlogContent = content,
			};
			string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

			using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			int result = db.Execute(query,item);

			String message = result > 0 ? "Saving Successful." : "Saving Failed.";
			Console.WriteLine(message);
		}
		private void Update(int Id, string title, string author, string content)
		{
			var item = new BlogDto
			{
				BlogId = Id,
				BlogTitle = title,
				BlogAuthor = author,
				BlogContent = content,
			};
			string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

			using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			int result = db.Execute(query,item);

			String message = result > 0 ? "Update Successful." : "Update Failed.";
			Console.WriteLine(message);
		}
		private void Delete(int Id)
		{
			var item = new BlogDto
			{
				BlogId = Id,
			};
			string query = @"DELETE FROM Tbl_Blog where BlogId = @BlogId";

			using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
			int result = db.Execute(query,item);

			String message = result > 0 ? "Delete Successful." : "Delete Failed.";
			Console.WriteLine(message);
		}
	}
}
