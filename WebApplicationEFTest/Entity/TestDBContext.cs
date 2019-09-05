using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationEFTest.Entity
{
    public class TestDBContext : DbContext
    {
        public TestDBContext(DbContextOptions options) : base(options)
        {

        }

        public TestDBContext() 
        {

        }

        /// <summary>
        /// 映射用户信息表
        /// </summary>
        public virtual DbSet<User> User { get; set; }

        /// <summary>
        /// 映射用户信息表
        /// </summary>
        public virtual DbSet<Role> Role { get; set; }

        /// <summary>
        /// 映射用户信息表
        /// </summary>
        public virtual DbSet<UserRole> UserRole { get; set; }

        /// <summary>
        /// 映射用户信息表
        /// </summary>
        public virtual DbSet<OperateLog> OperateLog { get; set; }

        /// <summary>
        /// 映射用户信息表
        /// </summary>
        public virtual DbSet<LoginLog> LoginLog { get; set; }

        public virtual DbSet<UserExtraInfo> UserExtraInfo
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder model_builder)
        {

            model_builder.ConfigDatabaseDescription();
            base.OnModelCreating(model_builder); 
        }
    }
}
