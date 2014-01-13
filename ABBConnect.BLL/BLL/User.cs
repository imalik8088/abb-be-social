using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    /// <summary>
    /// Class that rappresent a general user, without distinctions of the kind of user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Constructor that automatically instantiate the attributes of the class
        /// </summary>
        public User()
        {
            this.iD = -1;
            this.location = "";
            this.username = "";
        }

        /// <summary>
        /// Constructor that instantiate tha attributes of the class to the values given in input
        /// </summary>
        /// <param name="entityUser">Class that contains all the informations about the user</param>
        public User(GetUserSavedFiltersTagedUsers_Result entityUser)
        {
            this.iD = entityUser.Id;
            this.username = entityUser.Name;
        }

        /// <summary>
        /// Attribute that rappresent the ID of the user
        /// </summary>
        private int iD;
        /// <summary>
        /// Properties that allow to modify or take the ID of a user
        /// </summary>
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

        /// <summary>
        /// Attribute that rappresent the location of the user
        /// </summary>
        private string location;
        /// <summary>
        /// Properties that allow to modify or take the location of a user
        /// </summary>
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

        /// <summary>
        /// Attribute that rappresent the username of the user
        /// </summary>
        private string username;
        /// <summary>
        /// Properties that allow to modify or take the username of a user
        /// </summary>
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

        /// <summary>
        /// Attribute representing the avatar image of a user
        /// </summary>
        private string avatar;
        /// <summary>
        /// Properties that allow to modify or receive the base64 string representation of the avatar image
        /// </summary>
        public string Avatar
        {
            get
            {
                return avatar;
            }
            set
            {
                avatar = value;
            }
        }
    }
}
