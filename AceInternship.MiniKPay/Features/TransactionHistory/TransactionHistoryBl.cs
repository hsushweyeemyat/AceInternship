using AceInternship.MiniKPay.Models;

namespace AceInternship.MiniKPay.Features.TransactionHistory
{
    public class TransactionHistoryBl
	{
		private readonly TransactionHistoryDA _transactionHistoryDA;

		public TransactionHistoryBl(TransactionHistoryDA transactionHistoryDA)
		{
			_transactionHistoryDA = transactionHistoryDA;
		}

		public TransactionHistoryResponseModel TransactionHistory(TransactionHistoryRequestModel requestModel)
		{
			TransactionHistoryResponseModel model = new TransactionHistoryResponseModel();
			//Exist CustomerCode
			bool isExist = _transactionHistoryDA.IsExistCustomerCode(requestModel.CustomerCode!);
			if (!isExist)
			{
				model.IsSuccess = false;
				model.Message = "Customer doesn't exist.";
				return model;
			}

			//Transaction History By Customer Code
			var lst = _transactionHistoryDA.TransactionHistoryByCustomerCode(requestModel.CustomerCode!);
			model.IsSuccess = true;
			model.Message = "Success";
			model.Data = lst;
			return model;
		}
	}
}
