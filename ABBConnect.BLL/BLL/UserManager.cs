using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd;
using System.Data;

namespace BLL
{
    public class UserManager: IUserManager
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
            DataTable infoTable = userDbData.RetrieveUserInformation(userName);
            User selectedUser = new User();

            foreach (DataRow row in infoTable.Rows)
            {
                selectedUser.UserName = row["UserName"].ToString();
                selectedUser.FirstName = row["FirstName"].ToString();
                selectedUser.LastName = row["LastName"].ToString();
                selectedUser.PhoneNumber = row["PhoneNumber"].ToString();
                selectedUser.Email = row["Email"].ToString();

            }

            return selectedUser;
        }

        public bool UpdateUserInformation(User user)
        {
            throw new NotImplementedException();
        }
    }
}
