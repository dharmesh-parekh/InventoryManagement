using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Library.Enums
{
    public enum RoleEnum
    {
        [Description("Administrator")]
        Administrator = 1,
        [Description("User")]
        User = 2
    }
}
