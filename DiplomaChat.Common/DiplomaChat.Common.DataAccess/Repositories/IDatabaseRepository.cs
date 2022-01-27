using System.Threading.Tasks;

namespace DiplomaChat.Common.DataAccess.Repositories
{
    public interface IDatabaseRepository
    {
        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}