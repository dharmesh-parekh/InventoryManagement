using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace InventoryManagement.Library.Caches
{
    public class CommonCache : CacheLock
    {  
        private static void UpdateCache(CommonCache commonCache)
        {
            lock (ThisLock)
            {
                HttpContext.Current.Cache["CommonCache"] = commonCache;
            }
        }
        public static void Clear()
        {

            if (HttpContext.Current.Cache["CommonCache"] != null)
                HttpContext.Current.Cache.Remove("CommonCache");
        }


      

    }
}
