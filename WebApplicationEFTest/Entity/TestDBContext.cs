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
            model_builder.GenerateUnderScoreCaseColumn(this);
            model_builder.GenerateForeignKey(this);

            //model_builder.Entity<UserRole>()
            //    .HasOne(p => p.User1)
            //    .WithMany(p => p.UserRoles1)
            //    .HasForeignKey(p => p.UserId1)
            //    .HasConstraintName("ForeignKey_UserRole_User1");
            //model_builder.Entity<UserRole>()
            //    .HasOne(p => p.User2)
            //    .WithMany(p => p.UserRoles2)
            //    .HasForeignKey(p => p.UserId2)
            //    .HasConstraintName("ForeignKey_UserRole_User2");
            //model_builder.Entity<UserRole>()
            //    .HasOne(p => p.Role)
            //    .WithMany(p => p.UserRoles)
            //    .HasForeignKey(p => p.RoleId)
            //    .HasConstraintName("ForeignKey_UserRole_Role");


            //model_builder.Entity<UserRole>()
            //    .HasOne(p => p.Role)
            //    .WithMany(p => p.UserRoles)
            //    .HasForeignKey(p => p.RoleId)
            //    .HasConstraintName("ForeignKey_UserRole_Role");
            //model_builder.Entity(typeof(UserRole))
            //.HasOne(typeof(User)) //不能用string，只能用type https://stackoverflow.com/questions/50366754/entity-type-microsoft-aspnetcore-identity-identityrole-is-in-shadow-state
            //.WithMany("UserRoles")
            //.HasForeignKey("UserId")
            //.HasConstraintName("ForeignKey_UserRole_User");

            base.OnModelCreating(model_builder);
        }
    }
}
