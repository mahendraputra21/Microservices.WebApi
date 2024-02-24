﻿using Domain;
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
    }

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if(customer != null) 
            {
                await DeleteAsync(customer);
                return true;
            }

            return false;
        }

        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await db.Customers.ToListAsync();

            List<CustomerDTO> listCustomerDTO = [];
            foreach (var customer in customers)
            {
                CustomerDTO customerDTO = new()
                {
                    Name = customer.Name,
                    Contact = customer.Contact,
                    City = customer.City,
                    Email = customer.Email,
                };

                listCustomerDTO.Add(customerDTO);
            }

            return listCustomerDTO;
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

        public async Task<bool> UpdateCustomerAsync(CustomerDTO customerDTO, int id)
        {
            var customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == id);
            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;
            customer.Contact = customerDTO.Contact;
            customer.City = customerDTO.City;

            await UpdateAsync(customer);
            return true;
        }
    }
}
