﻿using FinalProjectOfUnittest.Data.DAL;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectOfUnittest.Data.BLL
{
    //declare dal first
    public class RoleBLL
    {
        public RoleDAL RoleDAL;

        public RoleBLL(RoleDAL rd)
        {
            RoleDAL = rd;
        }

        //To get all list check RoleDAL GETALL
        public ICollection<IdentityRole> GetAllRoles()
        {
            return RoleDAL.GetAll();
        }
        public IdentityRole Get(Func<IdentityRole, bool> firstFuction)
        {
            return RoleDAL.Get(firstFuction);
        }
        //SAVE FUNCTION
        public void Save()
        {
            RoleDAL.Save();
        }
    }
}