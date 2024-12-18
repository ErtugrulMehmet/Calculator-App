using Calculator_App;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Globalization;
using System.IO.Pipelines;

string connectionString = "Server=DESKTOP-2PVQEO2;Database=Calculator;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";


UserAction userAction = new UserAction(connectionString);
userAction.GetUserInput();



public static class TableMethod
{
    public static string GetDrinkingWaterTableQuery()
    {
        return "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Drinking_Water') BEGIN CREATE TABLE Drinking_Water (ID INT PRIMARY KEY IDENTITY(1,1), Date NVARCHAR(50), Quantity INT); END";
    }
    public static string GetAll()
    {
        return "SELECT * FROM dbo.Drinking_Water";
    }
    public static string Add(string date,int quantity)
    {
        return $"INSERT INTO Drinking_Water (Date, Quantity) VALUES ({date}, {quantity})";
    }
    public static string Delete(string id)
    {
        return $"DELETE * FROM dbo.Drinking_Water WHERE ID =  {id}";
    }
    public static string Update(int id, string date = "", int quantity = 0 )
    {
        string query1 = date == "" ? "" : $"Date = {date}";
        string query2 = quantity == 0 ? "" : $"Quantity = {quantity}";

        string result = String.Join(", ",query1, query2);

        return $"UPDATE Drinking_Water SET {result} WHERE id = {id}";
    }
}

public  class UserAction
{
    private string _connectionString = "";

    public UserAction(string connectionString)
    {
       _connectionString = connectionString;
        CreateTable();
    }
    public void GetUserInput()
    {
        Console.Clear();
        bool isCloseApp = true;
        while (isCloseApp)
        {
            Console.WriteLine(" ---------------------------------- ");
            Console.WriteLine("| MAIN MENU                        |");
            Console.WriteLine("| What would you like to do?.      |");
            Console.WriteLine("| Type 0 to Close Application.     |");
            Console.WriteLine("| Type 1 to View All Records.      |");
            Console.WriteLine("| Type 2 to Insert Record.         |");
            Console.WriteLine("| Type 3 to Delete Record.         |");
            Console.WriteLine("| Type 4 to Update Record.         |");
            Console.WriteLine("----------------------------------");

            string commandInput = Console.ReadLine();

            switch (commandInput)
            {
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    break;
                case "1":
                    GetAllRecords();
                    break;
                case "2":
                    InsertRecord();
                    break;
                default:
                    break;
            }
        }

    }
    public void CreateTable()
    {
        Console.Clear();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var tableCommand = connection.CreateCommand();
            tableCommand.CommandText = TableMethod.GetDrinkingWaterTableQuery();


            SqlDataReader reader = tableCommand.ExecuteReader();           

            connection.Close();          

        }

    }
    public void GetAllRecords ()
    {
        List<DrinkingWater> drinkingWater = new List<DrinkingWater>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var tableCommand = connection.CreateCommand();
            tableCommand.CommandText = TableMethod.GetAll();


            SqlDataReader reader = tableCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    drinkingWater.Add(new DrinkingWater()
                    {
                        ID = reader.GetInt32(0),
                        Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                        Quantity = reader.GetInt32(2)
                    });
                }

            }
            else
            {
                Console.WriteLine("No rows");
            }

            connection.Close();

            Console.WriteLine("-------------------------------------\n");

            foreach (var dw in drinkingWater)            
                Console.WriteLine($"{dw.ID} - {dw.Date} - {dw.Quantity}");

            Console.WriteLine("--------------------------------------\n");
            
        }




    }

    public void InsertRecord()
    {
        Console.WriteLine("Please enter a day. And use this format (dd-mm-yy)");
        string date = Console.ReadLine();

        DateTime convertedDate = DateTime.ParseExact(date, "dd-MM-yy", CultureInfo.InvariantCulture);
        Console.WriteLine("Please type a quantity");
        int quantity = Convert.ToInt32(Console.ReadLine());       

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var tableCommand = connection.CreateCommand();
            tableCommand.CommandText = TableMethod.Add(date, quantity);


            SqlDataReader reader = tableCommand.ExecuteReader();

            connection.Close();
           

        }
        GetAllRecords();
    }
}

