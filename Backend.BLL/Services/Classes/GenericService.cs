using Backend.BLL.Services.Interfaces;
using Backend.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Classes
{
    public class GenericService<TEntity,TEntityRequest,TEntityResponse>:IGenericService<TEntity, TEntityRequest, TEntityResponse> where TEntity : class
    {
    }
}
