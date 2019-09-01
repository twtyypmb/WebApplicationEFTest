﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationEFTest.Entity;

namespace WebApplicationEFTest.Migrations
{
    [DbContext(typeof(TestDBContext))]
    partial class TestDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplicationEFTest.Entity.LoginLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnName("login_time");

                    b.Property<DateTime?>("LogoutTime")
                        .HasColumnName("logout_time");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginLog");
                });

            modelBuilder.Entity("WebApplicationEFTest.Entity.OperateLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("UserId1")
                        .HasColumnName("user_id1");

                    b.Property<string>("UserId2")
                        .HasColumnName("user_id2");

                    b.HasKey("Id");

                    b.HasIndex("UserId1");

                    b.HasIndex("UserId2");

                    b.ToTable("OperateLog");
                });

            modelBuilder.Entity("WebApplicationEFTest.Entity.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("WebApplicationEFTest.Entity.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<double>("Age")
                        .HasColumnName("age");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<int>("Sex")
                        .HasColumnName("sex");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebApplicationEFTest.Entity.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("RoleId")
                        .HasColumnName("role_id");

                    b.Property<string>("UserIdCurrent")
                        .HasColumnName("user_id_current");

                    b.Property<string>("UserIdParent")
                        .HasColumnName("user_id_parent");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserIdCurrent");

                    b.HasIndex("UserIdParent");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("WebApplicationEFTest.Entity.LoginLog", b =>
                {
                    b.HasOne("WebApplicationEFTest.Entity.User", "User")
                        .WithMany("LoginLogs")
                        .HasForeignKey("UserId")
                        .HasConstraintName("ForeignKey_LoginLog_User_User");
                });

            modelBuilder.Entity("WebApplicationEFTest.Entity.OperateLog", b =>
                {
                    b.HasOne("WebApplicationEFTest.Entity.User", "User1")
                        .WithMany("ResourceOperateLogs1")
                        .HasForeignKey("UserId1")
                        .HasConstraintName("ForeignKey_OperateLog_User_User1");

                    b.HasOne("WebApplicationEFTest.Entity.User", "User2")
                        .WithMany("ResourceOperateLogs2")
                        .HasForeignKey("UserId2")
                        .HasConstraintName("ForeignKey_OperateLog_User_User2");
                });

            modelBuilder.Entity("WebApplicationEFTest.Entity.UserRole", b =>
                {
                    b.HasOne("WebApplicationEFTest.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("ForeignKey_UserRole_Role_Role");

                    b.HasOne("WebApplicationEFTest.Entity.User", "UserCurrent")
                        .WithMany("UserRolesCurrent")
                        .HasForeignKey("UserIdCurrent")
                        .HasConstraintName("ForeignKey_UserRole_User_UserCurrent");

                    b.HasOne("WebApplicationEFTest.Entity.User", "UserParent")
                        .WithMany("UserRolesParent")
                        .HasForeignKey("UserIdParent")
                        .HasConstraintName("ForeignKey_UserRole_User_UserParent");
                });
#pragma warning restore 612, 618
        }
    }
}
