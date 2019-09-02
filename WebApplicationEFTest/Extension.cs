using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace WebApplicationEFTest
{
    public static class Extension
    {
        public static bool IsSimpleType( this Type t )
        {
            return t.IsPrimitive || t == typeof( string );
        }

        /// <summary>
        /// 按照下划线规则生成的数据库字段名
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="context"></param>
        public static void GenerateUnderScoreCaseColumn(this ModelBuilder modelBuilder, DbContext context)
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

        public static void GenerateForeignKey(this ModelBuilder model_builder, DbContext context, string foreign_key_suffix = "Id", Type[] except_dbset_types = null)
        {
            // 找到上下文中定义的deset泛型类，且泛型类不在指定的例外之内
            var props = context.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Microsoft.EntityFrameworkCore.DbSet<>) );

            // 循环context中的dbset泛型属性
            foreach (var p in props)
            {
                if (except_dbset_types!=null && except_dbset_types.Contains(p.PropertyType))
                {
                    continue;
                }

                if (p.PropertyType?.GenericTypeArguments?.Length > 0)
                {
                    // 根据dbset指定的泛型类型反射该类型的属性
                    var dbset_type = p.PropertyType.GenericTypeArguments[0];
                    foreach (var item in dbset_type.GetProperties())
                    {
                        // 找到其中virtual的属性,不是列表IEnumerable<>类型的属性
                        if( !item.PropertyType.IsSimpleType() && item.GetAccessors().Where(a => a.IsVirtual).Count() > 0 && !( item.PropertyType.IsGenericType && typeof( IEnumerable<int> ).IsAssignableFrom( item.PropertyType.GetGenericTypeDefinition().MakeGenericType( typeof( int ) ) ) ) )
                        {
                            // 属性对应的类型，就是这个外键对应的类
                            var virtual_type = item.PropertyType;

                            // 先找到名称的后缀，以后缀区分多个外键
                            Regex regex = new Regex( $@"(?<={virtual_type.Name})\w+" );
                            var m = regex.Match( item.Name );
                            string suffix = m.Success ? m.Value : string.Empty;
                            // 找到主键类型中对应的外键的列表类型的属性
                            var list_property = virtual_type.GetProperties().Where( q => !q.PropertyType.IsSimpleType() );
                            foreach( var list_item_pro in list_property )
                            {
                                // 后缀为空或者后缀相同，则说明是同一个外键
                                if( suffix != string.Empty )
                                {
                                    var index = list_item_pro.Name.IndexOf( suffix );
                                    // 如果不为空，则需要和当前dbset类中的外键后缀相同
                                    if( index < 0 || list_item_pro.Name.Substring( index ) != suffix )
                                    {
                                        continue;
                                    }

                                }

                                if( list_item_pro.PropertyType.IsGenericType && typeof( IEnumerable<int> ).IsAssignableFrom( list_item_pro.PropertyType.GetGenericTypeDefinition().MakeGenericType( typeof( int ) ) ) && list_item_pro.PropertyType?.GenericTypeArguments[0] == dbset_type )
                                {

                                    model_builder.Entity( dbset_type )
                                           .HasOne( virtual_type, item.Name )
                                           .WithMany( list_item_pro.Name )
                                           .HasForeignKey( $"{virtual_type.Name}{foreign_key_suffix}{suffix}" )
                                           .HasConstraintName( $"ForeignKey_{dbset_type.Name}_{virtual_type.Name}_{item.Name}" );
                                    break;
                                }

                                if( list_item_pro.PropertyType == dbset_type )
                                {
                                    try
                                    {
                                        model_builder.Entity( dbset_type )
                                           .HasOne( virtual_type, item.Name )
                                           .WithOne( list_item_pro.Name )
                                           .HasForeignKey( $"{virtual_type.Name}{foreign_key_suffix}{suffix}" )
                                           .HasConstraintName( $"ForeignKey_{dbset_type.Name}_{virtual_type.Name}_{item.Name}" );
                                    }
                                    catch( Exception )
                                    {
                                        continue;
                                    }
                                    break;
                                }

                            }


                            //model_builder.Entity(typeof(UserRole))
                            //.HasOne(typeof(User),"User") //不能用string，只能用type https://stackoverflow.com/questions/50366754/entity-type-microsoft-aspnetcore-identity-identityrole-is-in-shadow-state
                            //.WithMany("UserRoles")
                            //.HasForeignKey("UserId")
                            //.HasConstraintName("ForeignKey_UserRole_User");
                        }
                        else
                        {
                            //modelBuilder.Entity(p.PropertyType.GenericTypeArguments[0]).Property(item.Name).HasColumnName(CamelCaseToUnderScoreCase(item.Name));
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
