using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AceInternship.PizzaApi.Features.Pizza
{
	[Route("api/[controller]")]
	[ApiController]
	public class PizzaController : ControllerBase
	{
		private readonly AppDbContext _appDbContext;
		public PizzaController()
		{
			_appDbContext = new AppDbContext();
		}
		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			var lst = await _appDbContext.Pizzas.ToListAsync();
			return Ok(lst);
		}
		[HttpGet("Extras")]
		public async Task<IActionResult> GetExtrasAsync()
		{
			var lst = await _appDbContext.PizzaExtras.ToListAsync();
			return Ok(lst);
		}
		[HttpPost("Order")]
		public async Task<IActionResult> OrderAsync(OrderRequest orderRequest)
		{
			var itemPizza = await _appDbContext.Pizzas.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
			var total = itemPizza.PizzaPrice;

			if (orderRequest.Extras.Length > 0)
			{

				var lstExtra = await _appDbContext.PizzaExtras.Where(x => orderRequest.Extras.Contains(x.Id)).ToListAsync();
				total += lstExtra.Sum(x => x.ExtraPrice);
			}
			var incoiceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
			PizzaOrderModel pizzaOrderModels = new PizzaOrderModel()
			{
				PizzaId = orderRequest.PizzaId,
				PizzaOrderInvoiceNo = incoiceNo,
				TotalAmount = total
			};
			List<PizzaOrderDetailModel> pizzaExtraModels = orderRequest.Extras.Select(extraId =>new PizzaOrderDetailModel
			{
				PizzaExtraId = extraId,
				PizzaOrderInvoiceNo= incoiceNo,
			}).ToList();

			await _appDbContext.PizzaOrders.AddAsync(pizzaOrderModels);
			await _appDbContext.PizzaOrderDetails.AddRangeAsync(pizzaExtraModels);
			await _appDbContext.SaveChangesAsync();
			
			OrderResponse response = new OrderResponse()
			{
				InvoiceNo = incoiceNo,
				Message	= "Thank you for your order! Enjoy your pizza!!",
				TotalAmount= total,
			};

			return Ok(orderRequest);
		}
	}
}
