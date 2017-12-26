using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace InventoryManagement.Web.Helpers
{
    public class UserDataHelper
    {
        public static UserData LoggedInUserInfo()
        {
            var user = new UserData();
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User.Identity is FormsIdentity)
            {
                user.UserName = HttpContext.Current.User.Identity.Name;
                FormsIdentity id =
                    (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;
                string userData = ticket.UserData;
                string[] data = userData.Split('|');
                if (data.Length > 0)
                    user.UserID = Convert.ToInt32(data[0]);
                if (data.Length > 1)
                    user.FullName = Convert.ToString(data[1]);
                if (data.Length > 2)
                    user.RoleID = Convert.ToInt32(data[2]);
                if (data.Length > 3)
                    user.RoleName = Convert.ToString(data[3]);
            }
            return user;
        }
    }

    public class UserData
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }

    }
}
