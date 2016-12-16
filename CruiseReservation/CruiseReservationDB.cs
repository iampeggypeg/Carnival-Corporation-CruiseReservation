namespace CruiseReservation
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CruiseReservationDB : DbContext
    {
        public CruiseReservationDB()
            : base("name=CruiseReservationDB")
        {
        }

        public virtual DbSet<cabin> cabin { get; set; }
        public virtual DbSet<cruise> cruise { get; set; }
        public virtual DbSet<customer_itinerary> customer_itinerary { get; set; }
        public virtual DbSet<destination> destination { get; set; }
        public virtual DbSet<itinerary> itinerary { get; set; }
        public virtual DbSet<port> port { get; set; }
        public virtual DbSet<ship> ship { get; set; }
        public virtual DbSet<itinerary_port> itinerary_port { get; set; }
        public virtual DbSet<ship_cabin> ship_cabin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cabin>()
                .Property(e => e.cabin_name)
                .IsUnicode(false);

            modelBuilder.Entity<cabin>()
                .HasMany(e => e.customer_itinerary)
                .WithRequired(e => e.cabin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cabin>()
                .HasMany(e => e.ship_cabin)
                .WithRequired(e => e.cabin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cruise>()
                .Property(e => e.cruise_name)
                .IsUnicode(false);

            modelBuilder.Entity<cruise>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<cruise>()
                .HasMany(e => e.ship)
                .WithMany(e => e.cruise)
                .Map(m => m.ToTable("cruise_ship").MapLeftKey("cruise_id").MapRightKey("ship_id"));

            modelBuilder.Entity<destination>()
                .Property(e => e.destination_name)
                .IsUnicode(false);

            modelBuilder.Entity<destination>()
                .HasMany(e => e.port)
                .WithMany(e => e.destination)
                .Map(m => m.ToTable("destination_port").MapLeftKey("destination_id").MapRightKey("port_id"));

            modelBuilder.Entity<itinerary>()
                .Property(e => e.itinerary_title)
                .IsUnicode(false);

            modelBuilder.Entity<itinerary>()
                .Property(e => e.cruise_price)
                .HasPrecision(10, 4);

            modelBuilder.Entity<itinerary>()
                .HasMany(e => e.customer_itinerary)
                .WithRequired(e => e.itinerary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<itinerary>()
                .HasMany(e => e.itinerary_port)
                .WithRequired(e => e.itinerary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<itinerary>()
                .HasMany(e => e.ship)
                .WithMany(e => e.itinerary)
                .Map(m => m.ToTable("ship_itinerary").MapLeftKey("itinerary_id").MapRightKey("ship_id"));

            modelBuilder.Entity<port>()
                .Property(e => e.port_name)
                .IsUnicode(false);

            modelBuilder.Entity<port>()
                .HasMany(e => e.itinerary_port)
                .WithRequired(e => e.port)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ship>()
                .Property(e => e.ship_name)
                .IsUnicode(false);

            modelBuilder.Entity<ship>()
                .HasMany(e => e.ship_cabin)
                .WithRequired(e => e.ship)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ship_cabin>()
                .Property(e => e.price)
                .HasPrecision(10, 4);
        }
    }
}
