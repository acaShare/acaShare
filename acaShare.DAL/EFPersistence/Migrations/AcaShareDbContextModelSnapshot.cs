﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using acaShare.DAL.Configuration;

namespace acaShare.DAL.EFPersistence.Migrations
{
    [DbContext(typeof(AcaShareDbContext))]
    partial class AcaShareDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("acaShare.BLL.Models.ChangeReason", b =>
                {
                    b.Property<int>("ChangeReasonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChangeType");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ChangeReasonId");

                    b.HasIndex("Reason")
                        .IsUnique()
                        .HasName("UQ_ChangeReason_Reason");

                    b.ToTable("ChangeReason");

                    b.HasData(
                        new { ChangeReasonId = 1, ChangeType = 1, Reason = "Nieodpowiednie treści" },
                        new { ChangeReasonId = 2, ChangeType = 1, Reason = "Naruszenie praw autorskich" },
                        new { ChangeReasonId = 3, ChangeType = 1, Reason = "Bezwartościowe informacje" },
                        new { ChangeReasonId = 4, ChangeType = 1, Reason = "Nieprawidłowa lokalizacja" },
                        new { ChangeReasonId = 5, ChangeType = 1, Reason = "Inne" }
                    );
                });

            modelBuilder.Entity("acaShare.BLL.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<int>("MaterialId");

                    b.Property<int>("UserId");

                    b.HasKey("CommentId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("acaShare.BLL.Models.DeleteRequest", b =>
                {
                    b.Property<int>("DeleteRequestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalComment")
                        .HasMaxLength(500);

                    b.Property<string>("DeclineReason")
                        .HasMaxLength(1000);

                    b.Property<int>("DeleteReasonId");

                    b.Property<int>("DeleterId");

                    b.Property<int?>("MaterialToDeleteId");

                    b.Property<int?>("ModeratorId");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime");

                    b.Property<int>("RequestState");

                    b.HasKey("DeleteRequestId");

                    b.HasIndex("DeleteReasonId");

                    b.HasIndex("DeleterId");

                    b.HasIndex("MaterialToDeleteId");

                    b.HasIndex("ModeratorId");

                    b.ToTable("DeleteRequest");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("UniversityId");

                    b.HasKey("DepartmentId");

                    b.HasIndex("UniversityId");

                    b.HasIndex("Abbreviation", "UniversityId")
                        .IsUnique()
                        .HasName("UQ_Department_Abbreviation");

                    b.HasIndex("Name", "UniversityId")
                        .IsUnique()
                        .HasName("UQ_Department_Name");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("acaShare.BLL.Models.EditRequest", b =>
                {
                    b.Property<int>("EditRequestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaterialToUpdateId");

                    b.Property<string>("NewDescription")
                        .HasMaxLength(10000);

                    b.Property<string>("NewName")
                        .HasMaxLength(80);

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("UpdaterId");

                    b.HasKey("EditRequestId");

                    b.HasIndex("MaterialToUpdateId");

                    b.HasIndex("UpdaterId");

                    b.ToTable("EditRequest");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Favorites", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("MaterialId");

                    b.HasKey("UserId", "MaterialId");

                    b.HasIndex("MaterialId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("acaShare.BLL.Models.File", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<int?>("EditRequestId");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("MaterialId");

                    b.Property<string>("RelativePath")
                        .IsRequired();

                    b.HasKey("FileId");

                    b.HasIndex("EditRequestId");

                    b.HasIndex("MaterialId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentId");

                    b.Property<int>("SemesterId");

                    b.Property<int>("SubjectId");

                    b.HasKey("LessonId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("SemesterId", "SubjectId", "DepartmentId")
                        .IsUnique()
                        .HasName("UQ_Lesson");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Material", b =>
                {
                    b.Property<int>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApproverId");

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000);

                    b.Property<int>("LessonId");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int>("StateId");

                    b.Property<int?>("UpdaterId");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime");

                    b.HasKey("MaterialId");

                    b.HasIndex("ApproverId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("LessonId");

                    b.HasIndex("StateId");

                    b.HasIndex("UpdaterId");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("acaShare.BLL.Models.MaterialState", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("StateId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ_MaterialState_Name");

                    b.ToTable("MaterialState");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsRead");

                    b.Property<int?>("MaterialId");

                    b.Property<int>("UserId");

                    b.HasKey("NotificationId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("UserId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Semester", b =>
                {
                    b.Property<int>("SemesterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.HasKey("SemesterId");

                    b.HasIndex("Number")
                        .IsUnique()
                        .HasName("UQ_Semester_Number");

                    b.ToTable("Semester");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("SubjectId");

                    b.HasIndex("Name", "Abbreviation")
                        .IsUnique()
                        .HasName("UQ_Subject_Name_Abbreviation");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("acaShare.BLL.Models.University", b =>
                {
                    b.Property<int>("UniversityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(126);

                    b.HasKey("UniversityId");

                    b.HasIndex("Abbreviation")
                        .IsUnique()
                        .HasName("UQ_University_Abbreviation");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ_University_Name");

                    b.ToTable("University");
                });

            modelBuilder.Entity("acaShare.BLL.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IdentityUserId")
                        .IsRequired();

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(22);

                    b.HasKey("UserId");

                    b.HasIndex("IdentityUserId")
                        .IsUnique()
                        .HasName("UQ_AspNetUsers_IdentityUserId");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasName("UQ_User_Username");

                    b.ToTable("User");
                });

            modelBuilder.Entity("acaShare.BLL.Models.UserInUniversity", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("UniversityId");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime");

                    b.Property<int>("TypeId");

                    b.HasKey("UserId", "UniversityId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UniversityId");

                    b.HasIndex("UserId", "UniversityId")
                        .IsUnique()
                        .HasName("UQ_UserInUniversity");

                    b.ToTable("UserInUniversity");
                });

            modelBuilder.Entity("acaShare.BLL.Models.UserType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("TypeId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ_UserType_Name");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new { Id = "51825ef9-7c53-41c0-88a2-53768f3fdb4b", ConcurrencyStamp = "c6f86064-ac00-4fa1-9e51-9c8c1ea6fce9", Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
                        new { Id = "b1f6687c-5bff-425f-af5c-5341b44c64c0", ConcurrencyStamp = "178c3ef7-ad05-44a6-93c4-a28c9bf72571", Name = "MainModerator", NormalizedName = "MAINMODERATOR" },
                        new { Id = "2eb6a235-de89-4d75-9ef7-3f44b352fb58", ConcurrencyStamp = "4b976fc9-50fa-4c24-9544-d7fad5f7ab5a", Name = "Moderator", NormalizedName = "MODERATOR" },
                        new { Id = "b64c4ab9-b764-49ba-aff9-8d5cf733751c", ConcurrencyStamp = "4284daa1-3427-48d3-8732-885b0c63addf", Name = "Member", NormalizedName = "MEMBER" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(22);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Comment", b =>
                {
                    b.HasOne("acaShare.BLL.Models.Material", "Material")
                        .WithMany("Comments")
                        .HasForeignKey("MaterialId")
                        .HasConstraintName("Comment_Material")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("acaShare.BLL.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("Comment_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("acaShare.BLL.Models.DeleteRequest", b =>
                {
                    b.HasOne("acaShare.BLL.Models.ChangeReason", "DeleteReason")
                        .WithMany()
                        .HasForeignKey("DeleteReasonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("acaShare.BLL.Models.User", "Deleter")
                        .WithMany("DeleteRequests")
                        .HasForeignKey("DeleterId")
                        .HasConstraintName("DeleteRequest_User")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("acaShare.BLL.Models.Material", "MaterialToDelete")
                        .WithMany("DeleteRequests")
                        .HasForeignKey("MaterialToDeleteId")
                        .HasConstraintName("DeleteRequest_Material")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("acaShare.BLL.Models.User", "Moderator")
                        .WithMany("HandledDeleteRequests")
                        .HasForeignKey("ModeratorId")
                        .HasConstraintName("FK_DeleteRequest_User")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("acaShare.BLL.Models.Department", b =>
                {
                    b.HasOne("acaShare.BLL.Models.University", "University")
                        .WithMany("Departments")
                        .HasForeignKey("UniversityId")
                        .HasConstraintName("Department_University")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("acaShare.BLL.Models.EditRequest", b =>
                {
                    b.HasOne("acaShare.BLL.Models.Material", "MaterialToUpdate")
                        .WithMany("EditRequests")
                        .HasForeignKey("MaterialToUpdateId")
                        .HasConstraintName("EditRequest_Material")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("acaShare.BLL.Models.User", "Updater")
                        .WithMany("EditRequests")
                        .HasForeignKey("UpdaterId")
                        .HasConstraintName("EditRequest_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("acaShare.BLL.Models.Favorites", b =>
                {
                    b.HasOne("acaShare.BLL.Models.Material", "Material")
                        .WithMany("Favorites")
                        .HasForeignKey("MaterialId")
                        .HasConstraintName("Favorites_Material")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("acaShare.BLL.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .HasConstraintName("Favorites_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("acaShare.BLL.Models.File", b =>
                {
                    b.HasOne("acaShare.BLL.Models.EditRequest", "EditRequest")
                        .WithMany("Files")
                        .HasForeignKey("EditRequestId")
                        .HasConstraintName("File_EditRequest")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("acaShare.BLL.Models.Material", "Material")
                        .WithMany("Files")
                        .HasForeignKey("MaterialId")
                        .HasConstraintName("File_Material")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("acaShare.BLL.Models.Lesson", b =>
                {
                    b.HasOne("acaShare.BLL.Models.Department", "Department")
                        .WithMany("Lessons")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("Lesson_Department")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("acaShare.BLL.Models.Semester", "Semester")
                        .WithMany("Lessons")
                        .HasForeignKey("SemesterId")
                        .HasConstraintName("Subject_Semester")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("acaShare.BLL.Models.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("Lesson_Subject")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("acaShare.BLL.Models.Material", b =>
                {
                    b.HasOne("acaShare.BLL.Models.User", "Approver")
                        .WithMany("ApprovedMaterials")
                        .HasForeignKey("ApproverId")
                        .HasConstraintName("Material_Moderator");

                    b.HasOne("acaShare.BLL.Models.User", "Creator")
                        .WithMany("CreatedMaterials")
                        .HasForeignKey("CreatorId")
                        .HasConstraintName("Material_Creator")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("acaShare.BLL.Models.Lesson", "Lesson")
                        .WithMany("Materials")
                        .HasForeignKey("LessonId")
                        .HasConstraintName("Material_Lesson")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("acaShare.BLL.Models.MaterialState", "State")
                        .WithMany("Materials")
                        .HasForeignKey("StateId")
                        .HasConstraintName("Material_MaterialState")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("acaShare.BLL.Models.User", "Updater")
                        .WithMany("UpdatedMaterials")
                        .HasForeignKey("UpdaterId")
                        .HasConstraintName("Material_WhoChanged");
                });

            modelBuilder.Entity("acaShare.BLL.Models.Notification", b =>
                {
                    b.HasOne("acaShare.BLL.Models.Material", "Material")
                        .WithMany("Notifications")
                        .HasForeignKey("MaterialId")
                        .HasConstraintName("FK_Material_Notification")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("acaShare.BLL.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_User_Notification")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("acaShare.BLL.Models.UserInUniversity", b =>
                {
                    b.HasOne("acaShare.BLL.Models.UserType", "Type")
                        .WithMany("UsersInUniversity")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("UserUniversity_UserType")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("acaShare.BLL.Models.University", "University")
                        .WithMany("UsersInUniversity")
                        .HasForeignKey("UniversityId")
                        .HasConstraintName("UserUniversity_University")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("acaShare.BLL.Models.User", "User")
                        .WithMany("UsersInUniversity")
                        .HasForeignKey("UserId")
                        .HasConstraintName("UserUniversity_User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
