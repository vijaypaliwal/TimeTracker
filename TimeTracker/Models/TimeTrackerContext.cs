using System.Data.Entity;

namespace TimeTracker.Models
{
    public class TimeTrackerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 



        public TimeTrackerContext()
            : base("name=TimeTrackerContext")
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.CreateDatabaseIfNotExists<TimeTracker.Models.TimeTrackerContext>());
        }

        public DbSet<Status> Status { get; set; }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Tracker> Trackers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tracker>()
            .HasRequired(p => p.Patient)
            .WithMany()
            .HasForeignKey(p => p.PatientID);
            modelBuilder.Entity<Tracker>()
            .HasRequired(p => p.Status)
            .WithMany()
            .HasForeignKey(p => p.StatusId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
