using Backend.DAL.Data;
using Backend.DAL.Models;
using Backend.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Classes
{
    public class AddressRepository:GenericRepository<Address>,IAddressRepository
    {
        private readonly ApplicationDbContext context;
        public AddressRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

        public async Task<Address> GetByUserIdAsync(string id)
        {
            var address = await context.Addresses.FirstOrDefaultAsync(a => a.AppUserId == id);
            if (address == null)
            {
                return null;
            }
            return address;
        }
    }
}
