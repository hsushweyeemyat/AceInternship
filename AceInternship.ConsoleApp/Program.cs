using AceInternship.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

AdoDotNetCRUD adoDotNetCRUD = new AdoDotNetCRUD();

//adoDotNetCRUD.Read();
//adoDotNetCRUD.Create("Test Title", "Test Author", "Test Content");
/*adoDotNetCRUD.Update(2, "Test", "Test", "Test");*/
adoDotNetCRUD.Delete(10);