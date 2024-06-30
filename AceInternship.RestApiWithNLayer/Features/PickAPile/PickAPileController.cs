using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AceInternship.RestApiWithNLayer.Features.PickAPile
{
	[Route("api/[controller]")]
	[ApiController]
	public class PickAPileController : ControllerBase
	{
		private async Task<PickAPile> GetDataAsync()
		{
			string jsonStr = await System.IO.File.ReadAllTextAsync("PickAPile.json");
			var model = JsonConvert.DeserializeObject<PickAPile>(jsonStr);
			return model;
		}
		[HttpGet("QuestionId")]
		public async Task<IActionResult> Questions()
		{
			var model = await GetDataAsync();
			return Ok(model.Questions);
		}
		[HttpGet("AnswerId")]
		public async Task<IActionResult> PileCard()
		{
			var model = await GetDataAsync();
			return Ok(model.Answers);
		}
		[HttpGet("{QuestionId}/{AnswerId}")]
		public async Task<IActionResult> Answers(int QuestionId, int AnswerId)
		{
			var model = await GetDataAsync();
			return Ok(model.Answers.FirstOrDefault(x => x.QuestionId == QuestionId && x.AnswerId == AnswerId));
		}
		public class PickAPile
		{
			public Question[] Questions { get; set; }
			public Answer[] Answers { get; set; }
		}

		public class Question
		{
			public int QuestionId { get; set; }
			public string QuestionName { get; set; }
			public string QuestionDesp { get; set; }
		}

		public class Answer
		{
			public int AnswerId { get; set; }
			public string AnswerImageUrl { get; set; }
			public string AnswerName { get; set; }
			public string AnswerDesp { get; set; }
			public int QuestionId { get; set; }
		}

	}
}
