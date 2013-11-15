using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BLL
{
    public class UserManager : IUserManager
    {
        private UserData userDbData;

        public UserManager()
        {
            userDbData = new UserData();
        }

        public bool Login(string userName, string password)
        {
            return userDbData.CheckCredentials(userName, password);
        }

        public User LoadUserInformation(string userName)
        {
            DataTable infoTable = userDbData.GetUserInformation(userName);
            User selectedUser = new User();

            foreach (DataRow row in infoTable.Rows)
            {
                string tempString = row["UserName"].ToString();
                
                if (tempString == null)
                    selectedUser.UserName = "";
                else
                    selectedUser.UserName = tempString;

                tempString = row["FirstName"].ToString();

                if (tempString == null)
                    selectedUser.FirstName = "";
                else
                    selectedUser.FirstName = tempString;

                tempString = row["LastName"].ToString();

                if (tempString == null)
                    selectedUser.LastName = "";
                else
                    selectedUser.LastName = tempString;

                tempString = row["PhoneNumber"].ToString();

                if (tempString == null)
                    selectedUser.PhoneNumber = "";
                else
                    selectedUser.PhoneNumber = tempString;

                tempString = row["Email"].ToString();

                if (tempString == null)
                    selectedUser.Email = "";
                else
                    selectedUser.Email = tempString;

                tempString = row["Location"].ToString();

                if (tempString == null)
                    selectedUser.WorkRoom = "";
                else
                    selectedUser.WorkRoom = tempString;

                selectedUser.CompanyTitle = "";


            }

            return selectedUser;
        }

        public bool UpdateUserInformation(User user)
        {
            throw new NotImplementedException();
        }
    }
}
