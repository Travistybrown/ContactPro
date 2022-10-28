using ContactPro.Models;

namespace ContactPro.Services.Interfaces
{
    public interface IAddressBookService
    {
        public Task AddContactToCategoryAsync(int categoryId,int contactId);

        // Add method: add to a list of CategoryIds
        public Task AddContactToCategoriesAsync(IEnumerable<int> categoyIds, int contactId);
        public Task<bool> IsContactInCatergory(int categoryId,int contactId);

        public Task<IEnumerable<Category>> GetAppUserCategoriesAsync(string appUserId);

        // Add method to remove form all Categories 

        public Task RemoveAllContactCategoriesAsync(int contactId);
    }
}
