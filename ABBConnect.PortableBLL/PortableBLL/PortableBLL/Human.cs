using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;

namespace PortableBLL
{
    public class Human: User
    {
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
        }

        public Human(GetHumanInformation_Result result)
        {
            base.ID = -1;
            base.UserName = result.Name;
            lastName = result.LastName;
            firstName = result.FirstName;
            this.phoneNumber = result.PhoneNumber;
            email = result.Email;
            base.Location = result.Location;
            companyTitle = ""; 
        }
        public Human(GetHumanInformationByUsername_Result result)
        {
            base.ID = result.Id;
            base.UserName = result.Name;
            lastName = result.LastName;
            firstName = result.FirstName;
            this.phoneNumber = result.PhoneNumber;
            email = result.Email;
            base.Location = result.Location;
            companyTitle = "";
        }

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

        private string phoneNumber;
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

        private string email;
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

        private string companyTitle;
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

        private string firstName;
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

        private string lastName;
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
