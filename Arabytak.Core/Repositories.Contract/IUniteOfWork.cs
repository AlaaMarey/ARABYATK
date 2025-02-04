using Arabytak.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arabytak.Core.Repositories.Contract
{
    public interface IUnitOfWork
    {
        public IGenericRepository<T> Repositorey<T>() where T : BaseEntity;
    }
}
