using FluentAssertions;
using Gringotts.Core;
using Gringotts.Core.Models;
using Gringotts.Models.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests
{
    public class CustomerManagerFixture
    {
        [Fact]
        public async Task CreateAsync_WithValidReuest_ShouldReturn_ValidResponse()
        {
            var customer = new Core.Models.Customer
            {
                Id = 0,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var expectedResult = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.IsValidCustomerToCreate(customer)).ReturnsAsync(true);
            mockCustomerRepository.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(expectedResult);

            var manager = new CustomerManager(mockCustomerRepository.Object);

            var result = await manager.CreateAsync(customer);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
            mockCustomerRepository.Verify(x => x.IsValidCustomerToCreate(It.IsAny<Customer>()), Times.Once);
            mockCustomerRepository.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_WithInValidCustomerRequest_ShouldThrow_Exception()
        {
            var customer = new Core.Models.Customer
            {
                Id = 0,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.IsValidCustomerToCreate(customer)).ReturnsAsync(false);

            var manager = new CustomerManager(mockCustomerRepository.Object);

            Func<Task> result = async () => await manager.CreateAsync(customer);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Invalid Customer Details.");
            mockCustomerRepository.Verify(x => x.IsValidCustomerToCreate(It.IsAny<Customer>()), Times.Once);
            mockCustomerRepository.Verify(x => x.CreateAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidReuest_ShouldReturn_ValidResponse()
        {
            var customer = new Core.Models.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var manager = new CustomerManager(mockCustomerRepository.Object);

            var result = await manager.GetByIdAsync(1);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(customer);
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithNonExistingCustomerId_ShouldThrow_Exception()
        {
            var mockCustomerRepository = new Mock<ICustomerRepository>();

            var manager = new CustomerManager(mockCustomerRepository.Object);

            Func<Task> result = async () => await manager.GetByIdAsync(1);

            result.Should().ThrowExactlyAsync<Exception>().WithMessage("Customer with Id: 1 does not exists.");
            mockCustomerRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
