using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User
    {
        public User()
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
