using Backend.BLL.Services.Interfaces;
using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Classes
{
    public class AddressService:GenericService<Address,AddressRequest,AddressFullResponse>,IAddressService
    {
        private readonly IAddressRepository repository;

        public AddressService(IAddressRepository repo):base(repo)
        {
            this.repository = repo;
        }

        public async Task<AddressFullResponse> GetByUserIdAsync(string id)
        {
            var address =await repository.GetByUserIdAsync(id);
            if (address == null)
            {
                return null;
            }
            return address.Adapt<AddressFullResponse>();
        }

        
    }
}
