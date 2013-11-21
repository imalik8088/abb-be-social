using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transformation_Layer;
using System.Data;

namespace BLL
{
    public class HumanManager : IHumanManager
    {
        private HumanData userDbData;

        public HumanManager()
        {
            userDbData = new HumanData();
        }

        public bool Login(string userName, string password)
        {
            if (!(String.IsNullOrEmpty(userName) && String.IsNullOrEmpty(password)))
                return userDbData.CheckCredentials(userName, password);
            else
                return false;
        }

        public Human LoadUserInformation(int humanId)
        {
            DataSet infoTable = userDbData.GetHumanInformation(humanId);
            Human selectedUser = new Human();

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

        public bool UpdateUserInformation(Human user)
        {
            throw new NotImplementedException();
        }
    }
}
