using InventoryManagement.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagement.Library.Caches
{
    public class RoleCache : CacheLock
    {        
     
        public RoleCache()
        {
           
        }

       

        private static RoleCache GetCache(System.Web.Caching.Cache cache, int teamOrterritoryManagerID, bool isTeamManager = false)
        {

            RoleCache dataObject = (RoleCache)cache["RoleCache"];

            if (dataObject == null)
            {
                dataObject = new RoleCache();
            }
            return dataObject;
        }
        private static void UpdateCache(RoleCache Cache)
        {
            lock (ThisLock)
            {
                HttpContext.Current.Cache["RoleCache"] = Cache;
            }
        }
    }
}
