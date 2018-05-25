using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Models
{
    public partial class cse136Context : DbContext
    {
        public virtual DbSet<DietPlans> DietPlans { get; set; }
        public virtual DbSet<Followings> Followings { get; set; }
        public virtual DbSet<FoodInMeals> FoodInMeals { get; set; }
        public virtual DbSet<Foods> Foods { get; set; }
        public virtual DbSet<Meals> Meals { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<PinnedDietPlans> PinnedDietPlans { get; set; }
        public virtual DbSet<PinnedWorkouts> PinnedWorkouts { get; set; }
        public virtual DbSet<Workouts> Workouts { get; set; }
        public virtual DbSet<WorkoutSteps> WorkoutSteps { get; set; }

        /// <summary>
        /// added constructor for Startup.cs to use
        /// </summary>
        /// <param name="options"></param>
        public cse136Context(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Added constructor for empty parameter
        /// </summary>
        public cse136Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=SURFACEYE\SQLEXPRESS;Database=cse136;Trusted_connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DietPlans>(entity =>
            {
                entity.ToTable("Diet_Plans");

                entity.Property(e => e.Information).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.DietPlans)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK__Diet_Plan__Perso__2180FB33");
            });

            modelBuilder.Entity<Followings>(entity =>
            {
                entity.HasOne(d => d.FollowerNavigation)
                    .WithMany(p => p.FollowingsFollowerNavigation)
                    .HasForeignKey(d => d.Follower)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Followings_Follower");

                entity.HasOne(d => d.FollowingNavigation)
                    .WithMany(p => p.FollowingsFollowingNavigation)
                    .HasForeignKey(d => d.Following)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Followings_Following");
            });

            modelBuilder.Entity<FoodInMeals>(entity =>
            {
                entity.ToTable("Food_In_Meals");

                entity.Property(e => e.Amount).HasDefaultValueSql("((1))");

                entity.Property(e => e.FoodId).HasColumnName("Food_Id");

                entity.Property(e => e.MealId).HasColumnName("Meal_Id");

                entity.Property(e => e.Units).HasMaxLength(20);

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodInMeals)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Food_In_Meals_Foods");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.FoodInMeals)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Food_In_Meals_Meals");
            });

            modelBuilder.Entity<Foods>(entity =>
            {
                entity.Property(e => e.Category).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nutrition).HasMaxLength(200);
            });

            modelBuilder.Entity<Meals>(entity =>
            {
                entity.Property(e => e.Alarm)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DietPlanId).HasColumnName("Diet_Plan_Id");

                entity.Property(e => e.Information).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.DietPlan)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.DietPlanId)
                    .HasConstraintName("FK_Meals_Diet_Plans");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Profile).HasColumnType("text");

                entity.Property(e => e.Sex).HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<PinnedDietPlans>(entity =>
            {
                entity.ToTable("Pinned_Diet_Plans");

                entity.Property(e => e.DietPlanId).HasColumnName("Diet_Plan_Id");

                entity.Property(e => e.PersonId).HasColumnName("Person_Id");

                entity.HasOne(d => d.DietPlan)
                    .WithMany(p => p.PinnedDietPlansDietPlan)
                    .HasForeignKey(d => d.DietPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pinned_Diet_Plans_Diet_Plans");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PinnedDietPlansPerson)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pinned_Diet_Plans_Persons");
            });

            modelBuilder.Entity<PinnedWorkouts>(entity =>
            {
                entity.ToTable("Pinned_Workouts");

                entity.Property(e => e.PersonId).HasColumnName("Person_id");

                entity.Property(e => e.WorkoutId).HasColumnName("Workout_id");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PinnedWorkouts)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pinned_Workouts_Person");

                entity.HasOne(d => d.Workout)
                    .WithMany(p => p.PinnedWorkouts)
                    .HasForeignKey(d => d.WorkoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pinned_Workouts_Workout");
            });

            modelBuilder.Entity<Workouts>(entity =>
            {
                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.CreatorId).HasColumnName("Creator_id");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workouts_Persons");
            });

            modelBuilder.Entity<WorkoutSteps>(entity =>
            {
                entity.ToTable("Workout_Steps");

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Instruction)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.StepNum).HasColumnName("Step_num");

                entity.Property(e => e.WorkoutId).HasColumnName("Workout_id");

                entity.HasOne(d => d.Workout)
                    .WithMany(p => p.WorkoutSteps)
                    .HasForeignKey(d => d.WorkoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workout_Steps_Workouts");
            });
        }
    }
}
