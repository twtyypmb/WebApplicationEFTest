using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WebApplicationEFTest
{
    public static class Extension
    {
        public const string annotation = "MS_Description";
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

        public static ModelBuilder ConfigDatabaseDescription(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //添加表说明
                if (entityType.FindAnnotation(annotation) == null && entityType.ClrType?.CustomAttributes.Any(
                        attr => attr.AttributeType == typeof(DescriptionAttribute)) == true)
                {
                    entityType.AddAnnotation(annotation,
                        (entityType.ClrType.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute
                        )?.Description);
                }

                //添加列说明
                foreach (var property in entityType.GetProperties())
                {
                    if (property.FindAnnotation(annotation) == null && property.PropertyInfo?.CustomAttributes
                            .Any(attr => attr.AttributeType == typeof(DescriptionAttribute)) == true)
                    {
                        var propertyInfo = property.PropertyInfo;
                        var propertyType = propertyInfo?.PropertyType;
                        //如果该列的实体属性是枚举类型，把枚举的说明追加到列说明
                        var enumDbDescription = string.Empty;
                        if (propertyType.IsEnum
                            || (propertyType.IsDerivedFrom(typeof(Nullable<>)) && propertyType.GenericTypeArguments[0].IsEnum))
                        {
                            var @enum = propertyType.IsDerivedFrom(typeof(Nullable<>))
                                ? propertyType.GenericTypeArguments[0]
                                : propertyType;

                            var descList = new List<string>();
                            foreach (var field in @enum?.GetFields() ?? new FieldInfo[0])
                            {
                                if (!field.IsSpecialName)
                                {
                                    var desc = (field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                        .FirstOrDefault() as DescriptionAttribute)?.Description;
                                    descList.Add(
                                        $@"{field.GetRawConstantValue()} : {(desc.IsNullOrWhiteSpace() ? field.Name : desc)}");
                                }
                            }

                            var isFlags = @enum?.GetCustomAttribute(typeof(FlagsAttribute)) != null;
                            var enumTypeDbDescription =
                                (@enum?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as
                                    DescriptionAttribute)?.Description;
                            enumTypeDbDescription += enumDbDescription + (isFlags ? " [是标志位枚举]" : string.Empty);
                            enumDbDescription =
                                $@"( {(enumTypeDbDescription.IsNullOrWhiteSpace() ? "" : $@"{enumTypeDbDescription}; ")}{string.Join("; ", descList)} )";
                        }

                        property.AddAnnotation(annotation,
                            $@"{(propertyInfo.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute)
                                ?.Description}{(enumDbDescription.IsNullOrWhiteSpace() ? "" : $@" {enumDbDescription}")}");
                    }
                }
            }

            return modelBuilder;
        }

        public static MigrationBuilder ApplyDatabaseDescription(this MigrationBuilder migrationBuilder, Migration migration)
        {
            var defaultSchema = "dbo";
            var descriptionAnnotationName = annotation;

            foreach (var entityType in migration.TargetModel.GetEntityTypes())
            {
                //添加表说明
                var tableName = entityType.Relational().TableName;
                var schema = entityType.Relational().Schema;
                var tableDescriptionAnnotation = entityType.FindAnnotation(descriptionAnnotationName);

                if (tableDescriptionAnnotation != null)
                {
                    migrationBuilder.AddOrUpdateTableDescription(
                        tableName,
                        tableDescriptionAnnotation.Value.ToString(),
                        schema.IsNullOrEmpty() ? defaultSchema : schema);
                }

                //添加列说明
                foreach (var property in entityType.GetProperties())
                {
                    var columnDescriptionAnnotation = property.FindAnnotation(descriptionAnnotationName);

                    if (columnDescriptionAnnotation != null)
                    {
                        migrationBuilder.AddOrUpdateColumnDescription(
                            tableName,
                            property.Relational().ColumnName,
                            columnDescriptionAnnotation.Value.ToString(),
                            schema.IsNullOrEmpty() ? defaultSchema : schema);
                    }
                }
            }

            return migrationBuilder;
        }
        
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// type类型是否继承于target_type
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="target_type">目标类型</param>
        /// <returns></returns>
        public static bool IsDerivedFrom(this Type type,Type target_type )
        {
            if (type.IsGenericType)
            {
                Type target_type1 = null;
                Type type1 = null;
                if (target_type.GenericTypeArguments.Length == 0)
                {
                    if (type.GenericTypeArguments.Length== 0 )
                    {
                        target_type1 = target_type.MakeGenericType(typeof(object));
                    }
                    else
                    {
                        target_type1 = target_type.MakeGenericType(type.GenericTypeArguments);
                        
                    }
                }
                else
                {
                    target_type1 = target_type;
                }

                if (type.GenericTypeArguments.Length == 0)
                {
                    type1 = type.MakeGenericType(typeof(object));
                }
                else
                {
                    type1 = type;
                }

                return target_type1.IsAssignableFrom(type1);
            }
            else
            {
                return target_type.IsAssignableFrom(type);
            }
            
        }

        public static void AddOrUpdateTableDescription(this MigrationBuilder migrationBuilder, string table_name, string description,string schema)
        {
            var sql = $@"declare @cou int
                           select @cou=COUNT(*)  from 
                           (
	                        select t.name as tname,c.name as cname, d.value as Description
                            from sysobjects t
                            left join syscolumns c
                            on c.id=t.id and t.xtype='U' and t.name<>'dtproperties'
                            left join sys.extended_properties d
                            on c.id=d.major_id and c.colid=d.minor_id and d.name = 'MS_Description'
                            where t.name = '{table_name}' and d.value is not null
	                        ) as sdfsdf
                        if @cou > 0
	                        EXEC sys.sp_dropextendedproperty    @name=N'MS_Description'    , @level0type=N'SCHEMA'    , @level0name=N'{schema}'    , @level1type=N'TABLE'    , @level1name=N'{table_name}'    , @level2type=N'COLUMN'    , @level2name=N'status'
                        EXEC sys.sp_addextendedproperty    @name=N'MS_Description'    , @value=N'{description}'    , @level0type=N'SCHEMA'    , @level0name=N'{schema}'    , @level1type=N'TABLE'    , @level1name=N'{table_name}'    , @level2type=N'COLUMN'    , @level2name=N'status'	";
          var a=  migrationBuilder.Sql(sql);
           
        }
        public static void AddOrUpdateColumnDescription(this MigrationBuilder migrationBuilder, string table_name, string column_name, string description, string schema)
        {
            var sql = $@"declare @cou int
                           select @cou=COUNT(*)  from 
                           (
	                        select t.name as tname,c.name as cname, d.value as Description
                            from sysobjects t
                            left join syscolumns c
                            on c.id=t.id and t.xtype='U' and t.name<>'dtproperties'
                            left join sys.extended_properties d
                            on c.id=d.major_id and c.colid=d.minor_id and d.name = 'MS_Description'
                            where t.name = '{table_name}' and c.name = '{column_name}' and d.value is not null
	                        ) as sdfsdf
                        if @cou > 0
	                        EXEC sys.sp_dropextendedproperty    @name=N'MS_Description'    , @level0type=N'SCHEMA'    , @level0name=N'{schema}'    , @level1type=N'TABLE'    , @level1name=N'{table_name}'    , @level2type=N'COLUMN'    , @level2name=N'{column_name}'
                        EXEC sys.sp_addextendedproperty    @name=N'MS_Description'    , @value=N'{description}'    , @level0type=N'SCHEMA'    , @level0name=N'{schema}'    , @level1type=N'TABLE'    , @level1name=N'{table_name}'    , @level2type=N'COLUMN'    , @level2name=N'{column_name}'	";
            var a = migrationBuilder.Sql(sql);
        }
        
    }
}
