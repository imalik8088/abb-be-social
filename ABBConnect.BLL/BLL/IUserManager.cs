using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IUserManager
    {
        bool Login(string userName, string password);
        User LoadUserInformation(string userName);
        bool UpdateUserInformation(User user);
    }
}
