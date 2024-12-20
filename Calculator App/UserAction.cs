using Calculator_App;
using Microsoft.Data.SqlClient;
using System.Globalization;
using AppContext = Calculator_App.AppContext;

public  class UserAction: IUserAction
{
    private readonly AppContext _context;

    public UserAction(AppContext context)
    {
        _context = context;
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

            string commandInput = Console.ReadLine() ?? "";

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
                case "3":
                    DeleteRecord();
                    break;
                case "4":
                    UpdateRecord();
                    break;
                default:
                    break;
            }
        }

    }
    public void GetAllRecords ()
    {
        Console.WriteLine("-------------------------------------\n");

        foreach (var dw in _context.DrinkingWaters.ToList())            
            Console.WriteLine($"{dw.ID} - {dw.Date} - {dw.Quantity}");

        Console.WriteLine("--------------------------------------\n");
    }

    public void InsertRecord()
    {
        Console.WriteLine("Please enter a day. And use this format (dd-mm-yy)");
        string date = Console.ReadLine();

        DateTime convertedDate = DateTime.ParseExact(date, "dd-MM-yy", CultureInfo.InvariantCulture);
        Console.WriteLine("Please type a quantity");
        int quantity = Convert.ToInt32(Console.ReadLine());

        DrinkingWater item = new DrinkingWater() { Date = convertedDate, Quantity = quantity };

        _context.DrinkingWaters.Add(item);
        _context.SaveChanges();
       
        GetAllRecords();
    }
    public void DeleteRecord()
    {
        GetAllRecords();
        Console.WriteLine("Please enter Record ID");

        int ID = Convert.ToInt32(Console.ReadLine());

        DrinkingWater item = _context.DrinkingWaters.FirstOrDefault(x => x.ID == ID);

        if (item != null) 
        _context.DrinkingWaters.Remove(item);        
        _context.SaveChanges();
        GetAllRecords();
    }

    private void UpdateRecord()
    {
        Console.WriteLine("Enter ID you weant to update recor");

        int ID = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Please enter a day. And use this format (dd-mm-yy)");
        string date = Console.ReadLine();


        DateTime convertedDate = DateTime.ParseExact(date, "dd-MM-yy", CultureInfo.InvariantCulture);
        Console.WriteLine("Please type a quantity");

        int quantity = Convert.ToInt32(Console.ReadLine());

        DrinkingWater item = _context.DrinkingWaters.FirstOrDefault(x => x.ID == ID);

        if(item != null)
        {
            item.Date = convertedDate;
            item.Quantity = quantity;
            _context.SaveChanges();
        }
        GetAllRecords();

    }
}

