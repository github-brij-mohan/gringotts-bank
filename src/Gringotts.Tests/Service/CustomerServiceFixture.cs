using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Gringotts.Core.Models;
using Gringotts.Models.Interfaces;
using Gringotts.Services;
using Gringotts.Services.Contracts;
using Gringotts.Services.Interfaces;
using Gringotts.Services.Translators;
using Gringotts.Services.Validators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Service
{
    public class CustomerServiceFixture
    {
        [Fact]
        public async Task CreateAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com"
            };

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

            var expectedResult = new CustomerResponse
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerManager = new Mock<ICustomerManager>();
            mockCustomerManager.Setup(x => x.CreateAsync(It.IsAny<Customer>())).ReturnsAsync(customer);

            var mockValidator = new Mock<IValidator<CreateCustomerRequest>>();
            mockValidator.Setup(x => x.ValidateAsync(createCustomerRequest, CancellationToken.None)).ReturnsAsync(new ValidationResult());

            var customerService = new CustomerService(mockCustomerManager.Object, mockValidator.Object);

            var result = await customerService.CreateAsync(createCustomerRequest);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task CreateAsync_WithInValidRequest_ShouldThrow_ValidationException()
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                Name = null,
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com"
            };


            var mockCustomerManager = new Mock<ICustomerManager>();

            var customerService = new CustomerService(mockCustomerManager.Object, new CreateCustomerRequestValidator());

            Func<Task> action = async () => await customerService.CreateAsync(createCustomerRequest);

            action.Should().ThrowExactlyAsync<ValidationException>().WithMessage("Mandatory Field: name");
        }

        [Fact]
        public async Task GetByIdAsync_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customerId = 1;
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

            var expectedResult = new CustomerResponse
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var mockCustomerManager = new Mock<ICustomerManager>();
            mockCustomerManager.Setup(x => x.GetByIdAsync(customerId)).ReturnsAsync(customer);

            var customerService = new CustomerService(mockCustomerManager.Object, new CreateCustomerRequestValidator());

            var result = await customerService.GetByIdAsync(customerId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
