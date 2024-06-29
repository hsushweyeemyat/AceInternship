using System.Data.SqlClient;

namespace AceInternship.RestApiWithNLayer
{
	public static class ConnectionStrings
	{
		public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
		{
			DataSource = ".",
			InitialCatalog = "AceInternship",
			UserID = "sa",
			Password = "sasa@123",
			TrustServerCertificate = true,
		};
	}
}
