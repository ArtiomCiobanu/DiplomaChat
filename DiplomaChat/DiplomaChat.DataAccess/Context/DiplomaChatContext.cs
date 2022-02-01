using System.Reflection;
using DiplomaChat.Common.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.Context
{
    public class DiplomaChatContext : DatabaseContext, IDiplomaChatContext
    {
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUser> RoomUsers { get; set; }
        public DbSet<User> Users { get; set; }

        public DiplomaChatContext(DbContextOptions<DiplomaChatContext> options) : base(options)
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