using FluentAssertions;
using Gringotts.Core.Models;
using Gringotts.DAL.Interfaces;
using Gringotts.Repository;
using Gringotts.Repository.Translators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Repository
{
    public class CustomerRepositoryFixture
    {
        [Fact]
        public async Task CreateAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var customer = new Customer
            {
                Id = 0,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerDal = new Mock<ICustomerDal>();
            mockCustomerDal.Setup(x => x.CreateAsync(It.IsAny<DAL.Entities.Customer>())).ReturnsAsync(dto);

            var repository = new CustomerRepository(mockCustomerDal.Object);

            var result = await repository.CreateAsync(customer);

            result.Should().BeEquivalentTo(new Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            });
            mockCustomerDal.Verify(x => x.CreateAsync(It.IsAny<DAL.Entities.Customer>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerDal = new Mock<ICustomerDal>();
            mockCustomerDal.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(dto);

            var repository = new CustomerRepository(mockCustomerDal.Object);

            var result = await repository.GetByIdAsync(1);

            result.Should().BeEquivalentTo(new Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            });
            mockCustomerDal.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task IsValidCustomerToCreateAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var dto = new DAL.Entities.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var customer = new Customer
            {
                Id = 0,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerDal = new Mock<ICustomerDal>();
            mockCustomerDal.Setup(x => x.GetByNameAndMobileAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(dto);

            var repository = new CustomerRepository(mockCustomerDal.Object);

            var result = await repository.IsValidCustomerToCreateAsync(customer);

            result.Should().BeFalse();
            mockCustomerDal.Verify(x => x.GetByNameAndMobileAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
