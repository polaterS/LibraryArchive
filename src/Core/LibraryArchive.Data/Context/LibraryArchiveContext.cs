using LibraryArchive.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryArchive.Data.Context
{
    public class LibraryArchiveContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryArchiveContext(DbContextOptions<LibraryArchiveContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<BookShare> BookShares { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<NoteShare> NoteShares { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Kategori ve Kitap İlişkisi
            builder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            // Kullanıcı ve Kitap/Not İlişkileri
            builder.Entity<Book>()
                .HasOne(b => b.User)
                .WithMany(u => u.Books)
                .HasForeignKey(b => b.UserId);

            builder.Entity<Note>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Note>()
                .HasOne(n => n.Book)
                .WithMany(b => b.Notes)
                .HasForeignKey(n => n.BookId)
                .OnDelete(DeleteBehavior.NoAction);

            // Kullanıcı ve Sipariş İlişkisi
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            // Sipariş ve Sipariş Detayı İlişkisi
            builder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderDetail>()
                .HasOne(od => od.Book)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(od => od.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sipariş Detayında ondalık tür için hassasiyet yapılandırması
            builder.Entity<OrderDetail>()
                .Property(od => od.Price)
                .HasColumnType("decimal(18, 2)");

            // Kullanıcı ve Adres İlişkisi
            builder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);

            // Bildirim Ayarları ve Kullanıcı İlişkisi
            builder.Entity<NotificationSettings>()
                .HasOne(ns => ns.User)
                .WithMany(u => u.NotificationSettings)
                .HasForeignKey(ns => ns.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bildirim ve Kullanıcı İlişkisi
            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bildirim ve Kitap İlişkisi (bildirimler belirli kitaplarla ilişkiliyse)
            builder.Entity<Notification>()
                .HasOne(n => n.Book)
                .WithMany(b => b.Notifications)
                .HasForeignKey(n => n.BookId)
                .OnDelete(DeleteBehavior.SetNull);

            // Bildirim ve Not İlişkisi (bildirimler belirli notlarla ilişkiliyse)
            builder.Entity<Notification>()
                .HasOne(n => n.Note)
                .WithMany(n => n.Notifications)
                .HasForeignKey(n => n.NoteId)
                .OnDelete(DeleteBehavior.SetNull);

            // Bildirim ve Sipariş İlişkisi (bildirimler belirli siparişlerle ilişkiliyse)
            builder.Entity<Notification>()
                .HasOne(n => n.Order)
                .WithMany(o => o.Notifications)
                .HasForeignKey(n => n.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
