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

        public Human LoadHumanInformation(int humanId)
        {
            DataSet infoSet = userDbData.GetHumanInformation(humanId);
            Human selectedUser = new Human();

            DataTableCollection infoCollection = infoSet.Tables;

            DataTable infoTable = infoCollection[0];

            foreach (DataRow row in infoTable.Rows)
            {
                string tempString = row["Name"].ToString();
                
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
                selectedUser.ID = humanId;

            }

            return selectedUser;
        }


        public Human LoadHumanInformationByUsername(string username)
        {
            DataSet infoSet = userDbData.GetHumanInformationByUsername(username);
            Human selectedUser = new Human();

            DataTableCollection infoCollection = infoSet.Tables;

            DataTable infoTable = infoCollection[0];

            foreach (DataRow row in infoTable.Rows)
            {
                string tempString = row["Name"].ToString();
                selectedUser.UserName = tempString == null ? "" : tempString;

                tempString = row["FirstName"].ToString();
                selectedUser.FirstName = tempString == null ? "" : tempString;

                tempString = row["LastName"].ToString();
                selectedUser.LastName = tempString == null ? "" : tempString;

                tempString = row["PhoneNumber"].ToString();
                selectedUser.PhoneNumber = tempString == null ? "" : tempString;

                tempString = row["Email"].ToString();
                selectedUser.Email = tempString == null ? "" : tempString;

                tempString = row["Location"].ToString();
                selectedUser.WorkRoom = tempString == null ? "" : tempString;

                tempString = row["Id"].ToString();
                int tempInt;
                bool result = Int32.TryParse(tempString, out tempInt);
                if (true == result)
                    selectedUser.ID = tempInt;

            }

            return selectedUser;
        }
    }
}
