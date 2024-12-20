using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_App
{
    internal interface ITableMethod
    {
        string GetDrinkingWaterTableQuery();
        string GetAll();
        string Add(string date, int quantity);
        string Delete(int id);
        string Update(int id, string date = "", int quantity = 0);
    }
}
