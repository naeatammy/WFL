using Microsoft.EntityFrameworkCore;
using PBL3.Models;

namespace PBL3.Data
{
	public class ApplicationDBContext : DbContext
	{

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		
		public DbSet<Freelancer> Freelancers { get; set; }
		public DbSet<Skill> ListSkill { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Job> job { get; set; }
		public DbSet<Review> Review { get; set; }
		public DbSet<JobRegistration> jobRegistrations { get; set; }
		public DbSet<hasSkill> hasSkill { get; set; }
		public DbSet<RequiredSkill> RequiredSkill { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasIndex(u => u.Username)
				.IsUnique();

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();
            modelBuilder.Entity<UserRole>()
           .HasKey(ur => new { ur.UserId, ur.RoleId });
			modelBuilder.Entity<hasSkill>()
			  .HasKey(ur => new { ur.FreelancerId, ur.SkillId });
			modelBuilder.Entity<RequiredSkill>()
			  .HasKey(ur => new { ur.JobId, ur.SkillId });
		}

	}

}
