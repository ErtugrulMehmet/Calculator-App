using Calculator_App;
using static System.Runtime.InteropServices.JavaScript.JSType;

public  class TableMethod:ITableMethod
{  

    public string GetDrinkingWaterTableQuery()
    {
        return "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Drinking_Water') BEGIN CREATE TABLE Drinking_Water (ID INT PRIMARY KEY IDENTITY(1,1), Date NVARCHAR(50), Quantity INT); END";
    }

    public string GetAll()
    {
        return "SELECT * FROM dbo.Drinking_Water";
    }

    public string Add(string date, int quantity)
    {
        return $"INSERT INTO Drinking_Water (Date, Quantity) VALUES ({date}, {quantity})";
    }

    public string Delete(int id)
    {
        return $"DELETE * FROM dbo.Drinking_Water WHERE ID =  {id}";
    }

    public string Update(int id, string date = "", int quantity = 0)
    {
        string query1 = date == "" ? "" : $"Date = {date}";
        string query2 = quantity == 0 ? "" : $"Quantity = {quantity}";

        string result = System.String.Join(", ", query1, query2);

        return $"UPDATE Drinking_Water SET {result} WHERE id = {id}";
    }
}

