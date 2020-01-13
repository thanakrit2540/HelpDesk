using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Models
{
    public static class SessionExtensions
    {
        /// <summary>
        /// setlogin เก็บuserID เข้าsession
        /// </summary>
        /// <param name="key"></param>
        /// <param name="empLogin"></param>
        public static void SetLogin(this ISession session, Employee empLogin)
        {
            session.SetInt32("login", empLogin.EmployeeID);
        }

        public static Employee GetLogin(this ISession session, DbSet<Employee> dbSet)
        {
            int EmployeeID = session.GetInt32("login").Value;
            return dbSet
                 .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.EmployeeType)
                .Include(e => e.Location)
                .Include(e => e.Position).FirstOrDefault(m => m.EmployeeID == EmployeeID);
        }

        public static int GetID(this ISession session)
        {
            return session.GetInt32("login").Value;
        }





        /* public static bool? GetBoolean(string key)
         {
             var data = session.Get(key);
             if (data == null)
             {
                 return null;
             }
             return BitConverter.ToBoolean(data, 0);
         }*/





        /* public static void SetBoolean(this ISession session, string key, bool value)
         {
             session.Set(key, BitConverter.GetBytes(value));
         }*/
    }

}
