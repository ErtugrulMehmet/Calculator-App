﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_App
{
    public interface IUserAction
    {
        void GetUserInput();

        void GetAllRecords();
        void InsertRecord();
    }
}
