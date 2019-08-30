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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.GenerateUnderScoreCaseColumn(this);
            modelBuilder.GenerateForeignKey(this);


            base.OnModelCreating(modelBuilder);
        }
    }
}
