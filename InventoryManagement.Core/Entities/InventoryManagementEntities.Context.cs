﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagement.Core.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InventoryManagementEntities : DbContext
    {
        public InventoryManagementEntities()
            : base("name=InventoryManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<GetUsers_Result> GetUsers(Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchValue)
        {
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchValueParameter = searchValue != null ?
                new ObjectParameter("SearchValue", searchValue) :
                new ObjectParameter("SearchValue", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetUsers_Result>("GetUsers", pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchValueParameter);
        }
    
        public virtual ObjectResult<GetCars_Result> GetCars(Nullable<int> userId, Nullable<int> pageNo, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchValue)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchValueParameter = searchValue != null ?
                new ObjectParameter("SearchValue", searchValue) :
                new ObjectParameter("SearchValue", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCars_Result>("GetCars", userIdParameter, pageNoParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchValueParameter);
        }
    }
}
