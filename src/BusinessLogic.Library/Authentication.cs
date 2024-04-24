﻿using DataAccessLayer.Library;
using Model.Library;
using System.Data;

namespace BusinessLogic.Library
{
    public class Authentication : IAuthenticate
    {
        public Result CheckCredentials(string username, string password)
        {
            DataTableAccess<User> da = new();
            DataTable dt = new();
            UserHandler data = new(da, dt);

            var user = data.GetByUsernamePassword(username,password);
            if (user != null)
            {
                return new() { Success = true, Message = string.Empty };
            }
            else
            {
                return new() { Success = false, Message = "User not found" };
            }
        }
    }
}
