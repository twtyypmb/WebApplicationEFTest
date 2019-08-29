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
            modelBuilder.Fun(this);
            

            modelBuilder.Entity<UserRole>(entity => 
            {
                entity.HasOne(p => p.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("ForeignKey_UserRole_User");

                entity.HasOne(p => p.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(p => p.RoleId)
                .HasConstraintName("ForeignKey_UserRole_Role");
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
