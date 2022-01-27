using System.Reflection;
using DiplomaChat.Common.DataAccess.Context;
using DiplomaChat.SingleSignOn.DataAccess.Entities;
using DiplomaChat.SingleSignOn.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DiplomaChat.SingleSignOn.DataAccess.Context
{
    public class SSOContext : DatabaseContext, ISSOContext
    {
        public DbSet<Account> Accounts { get; set; }

        public SSOContext(DbContextOptions<SSOContext> options)
            : base(options, typeof(AccountConfiguration).Assembly)
        {
        }
    }
}