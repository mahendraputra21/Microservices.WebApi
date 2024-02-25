using AutoMapper;
using Common.Exceptions;
using Domain;
using Infrastructure.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Infrastructure.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<int> InsertCustomerAsync(CustomerDTO customerDTO);
        Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO, int id);
        Task<bool> DeleteCustomerAsync(int id);
        Task<List<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int id);
    }

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly IMapper _mapper;
        public CustomerRepository(
            ApplicationDbContext db,
            IAutoMapperProfile autoMapperProfile) : base(db)
        {
            _mapper = autoMapperProfile.Configuration;
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                throw new ApiException(
                    "Customer not found", isHandled: false,
                    statusCode: Common.Constants.Enums.NotificationTypes.BADREQUEST);

            await DeleteAsync(customer);
            return true;
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if(customer == null)
                throw new ApiException(
                    "Customer not found", isHandled: false,
                    statusCode: Common.Constants.Enums.NotificationTypes.BADREQUEST);

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await db.Customers.ToListAsync();

            if(customers.Count < 1)
                throw new ApiException(
                   "Customer data is empty", isHandled: false,
                   statusCode: Common.Constants.Enums.NotificationTypes.BADREQUEST);

            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        public async Task<int> InsertCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            await InsertAsync(customer);
            return customer.Id;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO, int id)
        {
            var customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);

            if (customer == null)
                throw new ApiException(
                    "Customer not found", isHandled: false,
                    statusCode: Common.Constants.Enums.NotificationTypes.BADREQUEST);

            customer.Name = customerDTO.Name == null ? "": customerDTO.Name;
            customer.Email = customerDTO.Email == null ? "" : customerDTO.Email;
            customer.Contact = customerDTO.Contact == null ? "" : customerDTO.Contact;
            customer.City = customerDTO.City == null ? "" : customerDTO.City;

            await UpdateAsync(customer);
            return true;
        }
    }
}
