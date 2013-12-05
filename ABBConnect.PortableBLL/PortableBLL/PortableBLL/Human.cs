using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableTransformationLayer.ABBConnectServiceRef;

namespace PortableBLL
{
    public class Human
    {
        public Human()
        {
            this.userName = "";
            this.phoneNumber = "";
            this.lastName = "";
            this.companyTitle = "";
            this.email = "";
            this.firstName = "";
            this.workRoom = "";
            this.iD = -1;
        }
        public Human(GetHumanInformation_Result result)
        {
            iD = -1;
            userName = result.Name;
            lastName = result.LastName;
            firstName = result.FirstName;
            this.phoneNumber = result.PhoneNumber;
            email = result.Email;
            workRoom = result.Location;
            companyTitle = ""; 
        }
        public Human(GetHumanInformationByUsername_Result result)
        {
            iD = result.Id;
            userName = result.Name;
            lastName = result.LastName;
            firstName = result.FirstName;
            this.phoneNumber = result.PhoneNumber;
            email = result.Email;
            workRoom = result.Location;
            companyTitle = "";
        }

        public Human(GetFeedTags_Result res)
        {
            iD = res.UserId;
            userName = res.UserName;
            firstName = res.FirstName;
            lastName = res.LastName;
            this.phoneNumber = "";
            this.companyTitle = "";
            this.email = "";
            this.workRoom = "";
            
        }

        private string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
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

        private string workRoom;
        public string WorkRoom
        {
            get
            {
                return workRoom;
            }
            set
            {
                workRoom = value;
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

        private int iD;
        private GetFeedTags_Result res;
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
        
    }
}
