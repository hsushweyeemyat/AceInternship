using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AceInternship.PizzaApi;

    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<PizzaExtraModel> PizzaExtras { get; set; }
        public DbSet<PizzaOrderModel> PizzaOrders { get; set; }
        public DbSet<PizzaOrderDetailModel> PizzaOrderDetails { get; set; }
    }
[Table ("Tbl_Pizza")]
public class PizzaModel
{
	[Key]
    [Column("PizzaId")]
    public int Id { get; set; }
    
    [Column("Pizza")]
    public string? PizzaName { get; set; }

    [Column("Price")]
    public decimal PizzaPrice { get; set; }
}
[Table ("Tbl_PizzaExtra")]
public class PizzaExtraModel
{
    [Key]
    [Column ("PizzaExtraId")]
    public int Id { get; set; }

    [Column ("PizzaExtraName")]
    public string? ExtraName { get; set;}

    [Column ("Price")]
    public decimal ExtraPrice { get; set; }
    [NotMapped]
    public string PriceStr { get { return "$" + ExtraPrice; } }
}
public class OrderRequest
{
    public int PizzaId { get; set; }
    public int[] Extras { get; set; }
}
public class OrderResponse
{
    public string Message { get; set; }
    public string InvoiceNo {  get; set; }
    public decimal TotalAmount { get; set; }
}
[Table ("Tbl_PizzaOrder")]
public class PizzaOrderModel
{
    [Key]
    public int PizzaOrderId { get; set; }
    public string? PizzaOrderInvoiceNo { get; set; }
    public int PizzaId { get; set; }
    public decimal TotalAmount { get; set; }
}
[Table("Tbl_PizzaOrderDetail")]
public class PizzaOrderDetailModel
{
    [Key]
	public int PizzaOrderDetailId { get; set; }
	public string? PizzaOrderInvoiceNo { get; set; }
	public int PizzaExtraId { get; set; }
}
public class PizzaOrderInvoiceHeadModel
{
	public int PizzaOrderId { get; set; }
	public string? PizzaOrderInvoiceNo { get; set; }
	public decimal TotalAmount { get; set; }
	public int PizzaId { get; set; }
	public string? Pizza { get; set; }
	public decimal Price { get; set; }
}
public class PizzaOrderInvoicDetailModel
{
	public int PizzaOrderDetailId { get; set; }
	public string? PizzaOrderInvoiceNo { get; set; }
	public int PizzaExtraId { get; set; }
	public string? PizzaExtraName { get; set; }
	public decimal Price { get; set; }
}
public class PizzaOrderInvoicResponse
{
    public PizzaOrderInvoiceHeadModel Order { get; set; }
    public List<PizzaOrderInvoicDetailModel> OrderDetail { get; set; }
}