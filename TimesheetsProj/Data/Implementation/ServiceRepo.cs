using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Data.Implementation
{
    public class ServiceRepo : IServiceRepo
    {
        public Task Add(Service item)
        {
            throw new NotImplementedException();
        }

        public Task<Service> GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Service>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task Update(Service item)
        {
            throw new NotImplementedException();
        }
    }
}
