using Microsoft.EntityFrameworkCore;
using Telegram_clone.Hubs;
using Telegram_clone.Models;


namespace Telegram_clone.Data
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Chat> Chats { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<ChatMember> ChatMembers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();

			modelBuilder.Entity<User>()
				.HasIndex(u => u.UserName)
				.IsUnique();

			
			modelBuilder.Entity<Message>()
				.HasOne(m => m.Chat)
				.WithMany(c => c.Messages)
				.HasForeignKey(m => m.ChatId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Message>()
				.HasOne(m => m.Sender)
				.WithMany(u => u.Messages)
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ChatMember>()
				.HasOne(cm => cm.Chat)
				.WithMany(c => c.Members)
				.HasForeignKey(cm => cm.ChatId);

			modelBuilder.Entity<ChatMember>()
				.HasOne(cm => cm.User)
				.WithMany(u => u.ChatMembers)
				.HasForeignKey(cm => cm.UserId);

			base.OnModelCreating(modelBuilder);
		}
	}
}