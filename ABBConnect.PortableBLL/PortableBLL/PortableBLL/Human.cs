using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;

namespace PortableBLL
{
    /// <summary>
    /// Class that rappresent a human user allowing to use its data
    /// </summary>
    public class Human: User
    {
        /// <summary>
        /// Constructor that authomaticaly instantiate the attributes of the class to empty values
        /// </summary>
        public Human()
        {
            base.UserName = "";
            this.phoneNumber = "";
            this.lastName = "";
            this.companyTitle = "";
            this.email = "";
            this.firstName = "";
            base.Location = "";
            base.ID = -1;
        }

        /// <summary>
        /// Constructor that instatiate the attributes of the class to the data given in input
        /// </summary>
        /// <param name="result">Class that contain all the informations about a user, used to instatiate the attribute of the class</param>
        public Human(GetUsersByName_Result result)
        {
            base.ID = result.Id;
            base.UserName = result.Name;
            this.firstName = result.FirstName;
            this.lastName = result.LastName;
            base.Location = "";
            this.companyTitle = "";
            this.email = "";
            this.phoneNumber = "";
            base.Avatar = "";
        }

        /// <summary>
        /// Constructor that instatiate the attributes of the class to the data given in input
        /// </summary>
        /// <param name="result">Class that contain some of the informations about a user, used to instatiate the attribute of the class</param>
        /// <param name="humanId">Integer that rappresent the ID of the human user</param>
        public Human(GetHumanInformation_Result result, int humanId)
        {
            base.ID = humanId;
            base.UserName = result.Name;
            lastName = result.LastName;
            firstName = result.FirstName;
            this.phoneNumber = result.PhoneNumber;
            email = result.Email;
            base.Location = result.Location;
            base.Avatar = result.Image;
            companyTitle = ""; 
        }

        /// <summary>
        /// Constructor that instatiate the attributes of the class to the data given in input
        /// </summary>
        /// <param name="result">Class that contain all the informations about a user, used to instatiate the attribute of the class</param>
        public Human(GetHumanInformationByUsername_Result result)
        {
            base.ID = result.Id;
            base.UserName = result.Name;
            lastName = result.LastName;
            firstName = result.FirstName;
            this.phoneNumber = result.PhoneNumber;
            email = result.Email;
            base.Location = result.Location;
            base.Avatar = result.Image;
            companyTitle = "";
        }

        /// <summary>
        /// Constructor that instatiate the attributes of the class to the data given in input
        /// </summary>
        /// <param name="result">Class that contain all the informations about a user, used to instatiate the attribute of the class</param>
        public Human(GetFeedTags_Result res)
        {
            base.ID = res.UserId;
            base.UserName = res.UserName;
            firstName = res.FirstName;
            lastName = res.LastName;
            this.phoneNumber = "";
            this.companyTitle = "";
            this.email = "";
            base.Location = "";
            
        }

        /// <summary>
        /// Attribute that rappresent the telephone or mobile number of the user
        /// </summary>
        private string phoneNumber;
        /// <summary>
        /// Properties that allow to retrieve or change the telephone or mobile number of the user
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the e-mail address of the user
        /// </summary>
        private string email;
        /// <summary>
        /// Properties that allow to retrieve or change the e-mail address of the user
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        /// <summary>
        /// Attribute that represents the name of the company for which the user is working
        /// </summary>
        private string companyTitle;
        /// <summary>
        /// Properties that allow to retrieve or change the name of the company for which the user is working
        /// </summary>
        public string CompanyTitle
        {
            get
            {
                return companyTitle;
            }
            set
            {
                companyTitle = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the first name of the user
        /// </summary>
        private string firstName;
        /// <summary>
        /// Properties that allow to retrieve or change the first name of the user
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }

        /// <summary>
        /// Attribute that rappresent the last name of the user
        /// </summary>
        private string lastName;
        /// <summary>
        /// Properties that allow to retrieve or change the last name of the user
        /// </summary>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }
        
    }
}
