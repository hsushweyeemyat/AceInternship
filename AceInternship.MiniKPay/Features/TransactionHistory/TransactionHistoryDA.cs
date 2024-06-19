using AceInternship.MiniKPay.Models;
using Dapper;
using System.Data;

namespace AceInternship.MiniKPay.Features.TransactionHistory
{
	public class TransactionHistoryDA
	{
		private readonly IDbConnection _db;

		public TransactionHistoryDA(IDbConnection db)
		{
			_db = db;
		}

		public bool IsExistCustomerCode(string customercode)
		{
			string query = "Select * From Tbl_Customer with (nolock) where CustomerCode = @CustomerCode";
			var item = _db.Query <CustomerModel> (query, new { CustomerCode = customercode}).FirstOrDefault();

			//Transaction History By Customer Code
			return item is not null; //return item != null;
		}
		public List<CustomerTransactionHistoryModel> TransactionHistoryByCustomerCode(string customercode)
		{
			var lst = new List<CustomerTransactionHistoryModel>();
			string query = @"Select CTH.* From Tbl_CustomerTransactionHistory CTH
inner join Tbl_Customer C on CTH.FromMobileNo = C.MobileNo
where CustomerCode = @CustomerCode";
			lst = _db.Query<CustomerTransactionHistoryModel>(query, new { CustomerCode = customercode }).ToList();
			
			return lst;
		}
	}
}
