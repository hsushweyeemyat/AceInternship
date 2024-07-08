using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;

namespace AceInternship.RestApiWithNLayer.Features.MyanmarProverb;

[Route("api/[controller]")]
[ApiController]
public class MyanmarProverbsController : ControllerBase
{
	private async Task<Mmproverb> GetDataFormApi()
	{
		/*HttpClient client = new HttpClient();
		var response =await client.GetAsync("https://raw.githubusercontent.com/sannlynnhtun-coding/Myanmar-Proverbs/main/MyanmarProverbs.json");
		if (!response.IsSuccessStatusCode) return null;

		string jsonStr = await response.Content.ReadAsStringAsync();
			var model = JsonConvert.DeserializeObject<Mmproverb>(jsonStr);
			return model!;*/
		var jsonStr = await System.IO.File.ReadAllTextAsync("data2.json");
		var model = JsonConvert.DeserializeObject<Mmproverb>(jsonStr);
		return model!;
	}
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var model = await GetDataFormApi();
		return Ok(model.Tbl_MMProverbsTitle);
	}
	[HttpGet("{titleName}")]
	public async Task<IActionResult> Get(string titleName)
	{
		var model = await GetDataFormApi();
		var item = model.Tbl_MMProverbsTitle.FirstOrDefault(x => x.TitleName == titleName);
		if (item is null) return NotFound();

		var titleId = item.TitleId;
		var result = model.Tbl_MMProverbs.Where(x => x.TitleId == titleId);
		List<Tbl_MmproverbHead> lst = result.Select(x => new Tbl_MmproverbHead
		{
			ProverbId = x.ProverbId,
			ProverbName = x.ProverbName,
			TitleId = x.TitleId,
		}).ToList();
		return Ok(lst);
	}
	[HttpGet("{titleId}/{proverbId}")]
	public async Task<IActionResult> Get(int titleId, int proverbId)
	{
		var model = await GetDataFormApi();
		var item = model.Tbl_MMProverbs.FirstOrDefault(x => x.TitleId == titleId && x.ProverbId == proverbId);
		return Ok(item);
	}
}

public class Mmproverb
{
	public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
	public Tbl_MmproverbDetail[] Tbl_MMProverbs { get; set; }
}

public class Tbl_Mmproverbstitle
{
	public int TitleId { get; set; }
	public string TitleName { get; set; }
}

public class Tbl_MmproverbDetail
{
	public int TitleId { get; set; }
	public int ProverbId { get; set; }
	public string ProverbName { get; set; }
	public string ProverbDesp { get; set; }
}
public class Tbl_MmproverbHead
{
	public int TitleId { get; set; }
	public int ProverbId { get; set; }
	public string ProverbName { get; set; }
}
