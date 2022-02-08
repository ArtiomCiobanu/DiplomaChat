using DiplomaChat.Common.DataAccess.Context;
using DiplomaChat.DataAccess.Entities;
using DiplomaChat.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DiplomaChat.DataAccess.Context
{
    public class DiplomaChatContext : DatabaseContext, IDiplomaChatContext
    {
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUser> RoomUsers { get; set; }
        public DbSet<User> Users { get; set; }

        public DiplomaChatContext(DbContextOptions<DiplomaChatContext> options)
            : base(options, typeof(ChatRoomConfiguration).Assembly)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = Assembly.GetAssembly(GetType());
            modelBuilder.ApplyConfigurationsFromAssembly(assembly!);
        }
    }
}