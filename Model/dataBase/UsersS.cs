﻿using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.dataBase
{

    [Serializable]
    public partial class Users : IUser
    {

        public bool Equals(Users other)
        {

            return this.UserID == other.UserID;
        }

    }

}
