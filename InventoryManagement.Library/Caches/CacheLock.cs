using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagement.Library.Caches
{
    public class CacheLock
    {
        public static object ThisLock = new object();      
 
    }
}
