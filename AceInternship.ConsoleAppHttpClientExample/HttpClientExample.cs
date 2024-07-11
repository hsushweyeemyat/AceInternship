using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AceInternship.ConsoleAppHttpClientExample
{
	internal class HttpClientExample
	{
		private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7155") };
		private readonly string _blogEndpoint = "api/blog";
		public async Task RunAsync()
		{
			await ReadAsync();
			await EditAsync(1);
		}
		private async Task ReadAsync()
		{
			var response = await _client.GetAsync(_blogEndpoint);
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				Console.WriteLine(jsonStr);
				List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;
				foreach (var blog in lst)
				{
					Console.WriteLine(JsonConvert.SerializeObject(blog));
					Console.WriteLine($"Title == > {blog.BlogTitle}");
					Console.WriteLine($"Author == > {blog.BlogAuthor}");
					Console.WriteLine($"Content == > {blog.BlogContent}");
				}
			}
		}
		private async Task EditAsync(int id)
		{
			var response = await _client.GetAsync($"{_blogEndpoint}/{id}");
			if (response.IsSuccessStatusCode)
			{
				string jsonStr = await response.Content.ReadAsStringAsync();
				var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
					Console.WriteLine(JsonConvert.SerializeObject(item));
					Console.WriteLine($"Title == > {item.BlogTitle}");
					Console.WriteLine($"Author == > {item.BlogAuthor}");
					Console.WriteLine($"Content == > {item.BlogContent}");
			}
			else
			{
				string message = await response.Content.ReadAsStringAsync();
			}
		}
		private async Task CreateAsync(string title, string author, string content)
		{
			BlogModel model = new BlogModel()
			{
				BlogTitle = title,
				BlogAuthor = author,
				BlogContent = content
			};//C#

			//To Json
			string blogJson = JsonConvert.SerializeObject(model);
			HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
			var response = await _client.PostAsync(_blogEndpoint, httpContent);
			if (response.IsSuccessStatusCode)
			{
				string message = await response.Content.ReadAsStringAsync();
				Console.WriteLine(message);
			}
		}
		private async Task UpdateAsync(int id, string title, string author, string content)
		{
			BlogModel model = new BlogModel()
			{
				BlogTitle = title,
				BlogAuthor = author,
				BlogContent = content
			};//C#

			//To Json
			string blogJson = JsonConvert.SerializeObject(model);
			HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
			var response = await _client.PutAsync($"{_blogEndpoint}/{id}", httpContent);
			if (response.IsSuccessStatusCode)
			{
				string message = await response.Content.ReadAsStringAsync();
				Console.WriteLine(message);
			}
		}
		private async Task DeleteAsync(int id)
		{
			var response = await _client.DeleteAsync($"{_blogEndpoint}/{id}");
			if (response.IsSuccessStatusCode)
			{
				string message = await response.Content.ReadAsStringAsync();
				Console.WriteLine(message);
			}
			else
			{
				string message = await response.Content.ReadAsStringAsync();
				Console.WriteLine(message);
			}
		}
	}
}
