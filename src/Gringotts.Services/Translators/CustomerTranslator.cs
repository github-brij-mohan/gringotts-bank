using System.Runtime.CompilerServices;
using Contract = Gringotts.Services.Contracts;
using Model = Gringotts.Core.Models;
[assembly: InternalsVisibleTo("Gringotts.Tests")]

namespace Gringotts.Services.Translators
{
    internal static class CustomerTranslator
    {
        internal static Model.Customer ToModel(this Contracts.CreateCustomerRequest customerRequest)
        {
            return new Model.Customer
            {
                Name = customerRequest.Name,
                Address = customerRequest.Address,
                Mobile = customerRequest.Mobile,
                Email = customerRequest.Email
            };
        }

        internal static Contract.CustomerResponse ToResponse(this Model.Customer customer)
        {
            return new Contract.CustomerResponse
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Mobile = customer.Mobile,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }
    }
}
