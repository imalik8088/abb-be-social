using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;

namespace PortableBLL
{
    public class User
    {

        public User()
        {
            this.iD = -1;
            this.location = "";
            this.username = "";
        }

        public User(GetUserSavedFiltersTagedUsers_Result entityUser)
        {
            this.iD = entityUser.Id;
            this.username = entityUser.Name;
        }

        private int iD;
        public int ID
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        private string username;
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
    }
}
