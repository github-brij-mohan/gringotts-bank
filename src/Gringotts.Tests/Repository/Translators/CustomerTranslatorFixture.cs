using FluentAssertions;
using Gringotts.Core.Models;
using Gringotts.Repository.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Repository.Translators
{
    public class CustomerTranslatorFixture
    {
        [Fact]
        public async Task ToDto_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customer = new Customer
            {
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com"
            };

            var result = customer.ToDto();

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
        public async Task ToModel_WithValidRequest_ShouldReturn_ValidResponse()
        {
            var customer = new DAL.Entities.Customer
            {
                Id = 1,
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com",
                CreatedAt = DateTime.MinValue,
                UpdatedAt = DateTime.MinValue
            };

            var result = customer.ToModel();

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
