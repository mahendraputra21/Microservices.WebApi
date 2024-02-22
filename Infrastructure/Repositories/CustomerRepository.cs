using Domain;
using Model;

namespace Infrastructure.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<int> InsertCustomerAsync(CustomerDTO customerDTO);
    }

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            
        }

        public async Task<int> InsertCustomerAsync(CustomerDTO customerDTO)
        {
            Customer customer = new();
            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.Contact = customerDTO.Contact;
            customer.City = customerDTO.City;

            await InsertAsync(customer);
            return customer.Id;
        }
    }
}
