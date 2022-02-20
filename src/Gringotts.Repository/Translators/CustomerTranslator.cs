using Model = Gringotts.Core.Models;
using Dal = Gringotts.DAL.Entities;

namespace Gringotts.Repository.Translators
{
    internal static class CustomerTranslator
    {
        internal static Dal.Customer ToDto(this Model.Customer customer)
        {
            return new Dal.Customer
            {
                Name = customer.Name,
                Address = customer.Address,
                Mobile = customer.Mobile,
                Email = customer.Email
            };
        }

        internal static Model.Customer ToModel(this Dal.Customer customer)
        {
            return new Model.Customer
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
