using Infrastructure.Repositories;
using Model;

namespace Services.Services
{
    public interface ICustomerService
    {
        Task<int> InsertCustomerAsync(CustomerDTO customerDTO);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<int> InsertCustomerAsync(CustomerDTO customerDTO)
        {
            return await customerRepository.InsertCustomerAsync(customerDTO);
        }
    }
}
