using InventoryManagement.Core.Entities;
using InventoryManagement.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Library.Services
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        Role GetById(short roleId);
    }


    public class RoleService : IRoleService
    {
        IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        
        public List<Role> GetRoles()
        {
            return _roleRepository.Table.OrderBy(x=>x.Name).ToList();
        }

        public Role GetById(short roleId)
        {
            return _roleRepository.GetById(roleId);
        }

    }
}
