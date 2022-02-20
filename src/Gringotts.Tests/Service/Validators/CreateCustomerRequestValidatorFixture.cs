using FluentAssertions;
using Gringotts.Services.Contracts;
using Gringotts.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gringotts.Tests.Service.Validators
{
    public class CreateCustomerRequestValidatorFixture
    {
        [Fact]
        public async Task validateAsync_WithValidCustomerRequest_ShouldReturn_True()
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                Name =  "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com"
            };

            var validator = new CreateCustomerRequestValidator();

            var result = await validator.ValidateAsync(createCustomerRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task validateAsync_WithInvalidName_ShouldReturn_False(string name)
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                Name = name,
                Address = "test address",
                Mobile = "123456789",
                Email = "abc@abc.com"
            };

            var validator = new CreateCustomerRequestValidator();

            var result = await validator.ValidateAsync(createCustomerRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Mandatory Field: name");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task validateAsync_WithInvalidAddress_ShouldReturn_False(string address)
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                Name = "test_name",
                Address = address,
                Mobile = "123456789",
                Email = "abc@abc.com"
            };

            var validator = new CreateCustomerRequestValidator();

            var result = await validator.ValidateAsync(createCustomerRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Mandatory Field: address");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task validateAsync_WithInvalidMobile_ShouldReturn_False(string mobile)
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                Name = "test_name",
                Address = "test address",
                Mobile = mobile,
                Email = "abc@abc.com"
            };

            var validator = new CreateCustomerRequestValidator();

            var result = await validator.ValidateAsync(createCustomerRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Mandatory Field: mobile");
        }

        [Fact]
        public async Task validateAsync_WithInvalidEmailLength_ShouldReturn_False()
        {
            var createCustomerRequest = new CreateCustomerRequest
            {
                Name = "test_name",
                Address = "test address",
                Mobile = "123456789",
                Email = new string('a', 51)
            };

            var validator = new CreateCustomerRequestValidator();

            var result = await validator.ValidateAsync(createCustomerRequest);

            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
            result.Errors.First().ErrorMessage.Should().Be("Maximum length allowed for email is 50");
        }
    }
}
