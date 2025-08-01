using Backend.DAL.DTO.Request;
using Backend.DAL.DTO.Response;
using Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Interfaces
{
    public interface IAddressService:IGenericService<Address,AddressRequest,AddressFullResponse>
    {
        public Task<AddressFullResponse> GetByUserIdAsync(string id);
    }
}
