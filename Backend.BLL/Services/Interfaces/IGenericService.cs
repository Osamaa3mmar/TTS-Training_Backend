using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BLL.Services.Interfaces
{
    public interface IGenericService <TEntity,TEntityRequest,TEntityResponse>where TEntity : class
    {
    }
}
