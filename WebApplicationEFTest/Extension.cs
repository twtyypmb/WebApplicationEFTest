using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApplicationEFTest
{
    public static class Extension
    {
        public static void Fun(this ModelBuilder modelBuilder, DbContext context)
        {
            var props = context.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Microsoft.EntityFrameworkCore.DbSet<>));
            foreach (var p in props)
            {
                if (p.PropertyType?.GenericTypeArguments?.Length > 0)
                {
                    foreach (var item in p.PropertyType.GenericTypeArguments[0].GetProperties())
                    {
                        if (item.GetAccessors().Where(a => a.IsVirtual).Count() > 0)
                        {

                        }
                        else
                        {
                            modelBuilder.Entity(p.PropertyType.GenericTypeArguments[0]).Property(item.Name).HasColumnName(CamelCaseToUnderScoreCase(item.Name));
                        }
                    }
                    //modelBuilder.Entity(p.PropertyType.GenericTypeArguments[0]).Property(item.Name).HasColumnName(CamelCaseToUnderScoreCase(item.Name));
                }
            }
            
        }

        public static string CamelCaseToUnderScoreCase(string s)
        {
           
            Regex regex = new Regex("[A-Z]");
            string result = regex.Replace(s, m => "_" + m.Value.ToLower());
            if (result[0] == '_')
            {
                return result.Substring(1);
            }
            else
            {
                return result;
            }
        }

        public static string UnderScoreCaseToCamelCase(string s)
        {
            Regex regex = new Regex("_[a-z]");
            string result = regex.Replace(s, m => m.Value.Replace("_","").ToUpper());
            if (Char.IsLower(result[0]))
            {
                return $"{result[0].ToString().ToUpper()}{result.Substring(1)}";
            }
            else
            {
                return result;
            }
            
        }
    }
}
