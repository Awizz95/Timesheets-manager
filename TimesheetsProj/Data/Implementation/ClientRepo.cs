using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class ClientRepo : IClientRepo
    {
        public Task Add(Client item)
        {
            throw new NotImplementedException();
        }

        public Task<Client> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Client>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Client item)
        {
            throw new NotImplementedException();
        }
    }
}
