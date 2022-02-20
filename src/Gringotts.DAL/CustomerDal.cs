using Gringotts.DAL.Entities;
using Gringotts.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gringotts.DAL
{
    public class CustomerDal: ICustomerDal
    {
        private readonly BankDbContext _bankDbContext;

        public CustomerDal(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
            _bankDbContext.Database.EnsureCreated();
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            _bankDbContext.Customers.Add(customer);
            await _bankDbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetByIdAsync(int customerId)
        {
            var result = await _bankDbContext.Customers.FindAsync(customerId);
            return result;
        }

        public async Task<Customer> GetByNameAndMobileAsync(string name, string mobile)
        {
            var customer = _bankDbContext.Customers.FirstOrDefault(x => EF.Functions.Collate(x.Name, "SQL_Latin1_General_CP1_CS_AS") == name &&
                                                                        EF.Functions.Collate(x.Mobile, "SQL_Latin1_General_CP1_CS_AS") == mobile);

            return customer;
        }
    }
}
