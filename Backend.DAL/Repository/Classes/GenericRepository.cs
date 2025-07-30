using Backend.DAL.Data;
using Backend.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Repository.Classes
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

    }
}
