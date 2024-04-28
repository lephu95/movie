using Microsoft.EntityFrameworkCore;

namespace movie.Entities
{
    public class AppDbcontex:DbContext
    {
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillFood>BillFoods { get; set; }
        public DbSet<BillStatus> BillStatus { get; set; }
        public DbSet<BillTicket> BillTickets{ get; set; }
        public DbSet<Cinema> Cinemas{ get; set; }
        public DbSet<ComfimEmail> ComfimEmails { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<GeneralSetting> GeneralSettings { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieType> MoviesTypes{ get; set; }
        public DbSet<Promotion> Promotions{ get; set; }
        public DbSet<RankCustomer> RankCustomers{ get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<SeatStatus> SeatsStatus { get; set; }
        public DbSet<SeatType> SeatsTypes{ get; set; }
        public DbSet<Ticket> Tickets{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatus> UsersStatus { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server =LAPTOP-GHPQCUCA\\SQLEXPRESS02; Database = Movies; Trusted_Connection = True;Encrypt=false;TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bill)
                .HasForeignKey(b => b.Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Ticket>()
             .HasOne(b => b.Seat)
             .WithMany(u => u.Ticket)
             .HasForeignKey(b => b.SeatId)
             .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
