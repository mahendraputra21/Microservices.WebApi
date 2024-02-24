using Infrastructure.Repositories;
using Model;

namespace Services.Services
{
    public interface ICustomerService
    {
        Task<int> InsertCustomerAsync(CustomerDTO customerDTO);
        Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO, int id);
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

        public async Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO, int id)
        {
            return await customerRepository.UpdateCustomerAsync(customerDTO, id);
        }
    }
}
