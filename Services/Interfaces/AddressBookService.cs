using ContactPro.Data;
using ContactPro.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ContactPro.Services.Interfaces
{
    public class AddressBookService : IAddressBookService
    {
        ApplicationDbContext _context;

        public AddressBookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddContactToCategoriesAsync(IEnumerable<int> categoyIds, int contactId)
        {
            try
            {
                Contact? contact = await _context.Contacts.FindAsync(contactId);

                foreach (int categoryId in categoyIds)
                {
                    Category? category = await _context.Categories.FindAsync(categoryId);

                    if(contact != null && category != null)
                    {
                        

                        contact.Categories.Add(category);
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddContactToCategoryAsync(int categoryId, int contactId)
        {
            try
            {
                // Check to see if contact is already in the category
                if (!await IsContactInCatergory(categoryId,contactId))
                {
                    // If not..... Add the Category to the Contact's collection of Categories

                    Contact? contact = await _context.Contacts.FindAsync(contactId);

                    Category? category = await _context.Categories.FindAsync(categoryId);


                    if (contact != null && category != null)
                    {
                        category.Contacts.Add(contact);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<Category>> GetAppUserCategoriesAsync(string appUserId)
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = await _context.Categories.Where(c=>c.AppUserId == appUserId)
                                                             .OrderBy(c=>c.Name)
                                                             .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return categories;
        }

        public async Task<bool> IsContactInCatergory(int categoryId, int contactId)
        {
            Contact? contact = await _context.Contacts.FindAsync(contactId);

            bool isInCategory = await _context.Categories
                                        .Include(c=>c.Contacts)
                                        .Where(c=>c.Id == categoryId && c.Contacts.Contains(contact!))
                                        .AnyAsync();

            return isInCategory;
        }

        public async Task RemoveAllContactCategoriesAsync(int contactId)
        {
            try
            {
                Contact? contact = await _context.Contacts
                                                 .Include(c => c.Categories)
                                                 .FirstOrDefaultAsync(c => c.Id == contactId);

                contact!.Categories.Clear();
                _context.Update(contact);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
