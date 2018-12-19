using System;
using acaShare.BLL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace acaShare.DAL.Configuration
{
    public partial class AcaShareDbContext : IdentityDbContext<IdentityUser>
    {
        public AcaShareDbContext()
        {
        }

        public AcaShareDbContext(DbContextOptions<AcaShareDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChangeReason> ChangeReason { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<DeleteRequest> DeleteRequest { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<EditRequest> EditRequest { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<MaterialState> MaterialState { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Semester> Semester { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<University> University { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserInUniversity> UserInUniversity { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChangeReason>(entity =>
            {
                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(e => e.Reason)
                    .HasName("UQ_ChangeReason_Reason")
                    .IsUnique();

                entity.HasData(
                    new ChangeReason
                    {
                        ChangeReasonId = 1,
                        Reason = "Nieodpowiednie treści",
                        ChangeType = ChangeType.DELETE
                    },
                    new ChangeReason
                    {
                        ChangeReasonId = 2,
                        Reason = "Naruszenie praw własności",
                        ChangeType = ChangeType.DELETE
                    },
                    new ChangeReason
                    {
                        ChangeReasonId = 3,
                        Reason = "Bezwartościowe informacje",
                        ChangeType = ChangeType.DELETE
                    },
                    new ChangeReason
                    {
                        ChangeReasonId = 4,
                        Reason = "Inne",
                        ChangeType = ChangeType.DELETE
                    }
                );
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.CreatedDate)
                    .IsRequired()
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Comment_Material")
                    .IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Comment_User")
                    .IsRequired();
            });

            modelBuilder.Entity<DeleteRequest>(entity =>
            {
                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(e => e.AdditionalComment)
                   .HasMaxLength(500);

                entity.Property(p => p.DeclineReason)
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Deleter)
                    .WithMany(p => p.DeleteRequests)
                    .HasForeignKey(d => d.DeleterId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("DeleteRequest_User")
                    .IsRequired();

                entity.HasOne(d => d.MaterialToDelete)
                    .WithMany(p => p.DeleteRequests)
                    .HasForeignKey(d => d.MaterialToDeleteId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("DeleteRequest_Material");

                entity.HasOne(d => d.Moderator)
                    .WithMany(p => p.HandledDeleteRequests)
                    .HasForeignKey(d => d.ModeratorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DeleteRequest_User");
                
                entity.HasOne(d => d.DeleteReason);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Department_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Department_University")
                    .IsRequired();
            });

            modelBuilder.Entity<EditRequest>(entity =>
            {
                entity.Property(e => e.NewDescription).HasMaxLength(4000);

                entity.Property(e => e.NewName).HasMaxLength(255);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.MaterialToUpdate)
                    .WithMany(p => p.EditRequests)
                    .HasForeignKey(d => d.MaterialToUpdateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("EditRequest_Material")
                    .IsRequired();

                entity.HasOne(d => d.Updater)
                    .WithMany(p => p.EditRequests)
                    .HasForeignKey(d => d.UpdaterId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("EditRequest_User")
                    .IsRequired();
            });

            modelBuilder.Entity<Favorites>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.MaterialId });

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Favorites_Material");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Favorites_User");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RelativePath)
                    .IsRequired();

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.EditRequest)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.EditRequestId)
                    .HasConstraintName("File_EditRequest")
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.MaterialId)
                    .HasConstraintName("File_Material")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasIndex(e => new { e.SemesterId, e.SubjectDepartmentId })
                    .HasName("UQ_Lesson")
                    .IsUnique();

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Subject_Semester")
                    .IsRequired();

                entity.HasOne(d => d.SubjectDepartment)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.SubjectDepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Lesson_SubjectDepartment")
                    .IsRequired();
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(4000)
                    .IsRequired();

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.ApprovedMaterials)
                    .HasForeignKey(d => d.ApproverId)
                    .HasConstraintName("Material_Moderator");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.CreatedMaterials)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Material_Creator")
                    .IsRequired();

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Material_Lesson")
                    .IsRequired();

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Material_MaterialState")
                    .IsRequired();

                entity.HasOne(d => d.Updater)
                    .WithMany(p => p.UpdatedMaterials)
                    .HasForeignKey(d => d.UpdaterId)
                    .HasConstraintName("Material_WhoChanged");
            });

            modelBuilder.Entity<MaterialState>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_MaterialState_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Date)
                    .IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_User_Notification")
                    .IsRequired();

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.MaterialId) 
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Material_Notification");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.HasIndex(e => e.Number)
                    .HasName("UQ_Semester_Number")
                    .IsUnique();

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_Subject_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<SubjectDepartment>(entity =>
            {
                entity.HasIndex(e => new { e.SubjectId, e.DepartmentId })
                    .HasName("UQ_SubjectDepartment")
                    .IsUnique();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SubjectDepartment)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SubjectDepartment_Department")
                    .IsRequired();

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectDepartment)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SubjectDepartment_Subject")
                    .IsRequired();
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_University_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(126);

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.RegisterDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IdentityUserId)
                    .IsRequired();

                entity.HasIndex(e => new { e.IdentityUserId })
                    .HasName("UQ_AspNetUsers_IdentityUserId")
                    .IsUnique();
            });

            modelBuilder.Entity<UserInUniversity>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.UniversityId });

                entity.HasIndex(e => new { e.UserId, e.UniversityId })
                    .HasName("UQ_UserInUniversity")
                    .IsUnique();

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.UsersInUniversity)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("UserUniversity_UserType");

                entity.HasOne(d => d.University)
                    .WithMany(p => p.UsersInUniversity)
                    .HasForeignKey(d => d.UniversityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UserUniversity_University");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersInUniversity)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("UserUniversity_User");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_UserType_Name")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
