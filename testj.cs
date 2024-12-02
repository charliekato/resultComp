using System;
using System.IO;
using System.Text;
using Microsoft.Data.Sqlclient;

class Program {
    static void Main() {
	string connectionString="Persist Security Info=False;User ID=Sw;Password=;Initial Catalog=Sw;Server=localhost\\SQLEXPRESS;Encrypt=True;TrustServerCertificate=True;";
	string sqlQuery = "select * from クラス";
	try {
	    using (SqlConnection connection = new SqlConnection(ConnectionString)) {
		connection.Open();
	    }
	    Console.WriteLine("OK.");
	} catch (Exception ex) {
	    Console.WriteLine("NG.");
	}
    }
}

