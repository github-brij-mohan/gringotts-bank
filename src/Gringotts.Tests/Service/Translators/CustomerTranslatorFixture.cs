using FluentAssertions;
using Gringotts.Core.Models;
using Gringotts.Services.Contracts;
using Gringotts.Services.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Service.Translators
{
    public class CustomerTranslatorFixture
    {
        [Fact]
        public async Task ToModel_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com"
            };

            var result = createCustomerRequest.ToModel();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
            {
                Id = 0,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            });
        }

        [Fact]
        public async Task ToResponse_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customer = new Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var result = customer.ToResponse();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            });
        }
    }
}
