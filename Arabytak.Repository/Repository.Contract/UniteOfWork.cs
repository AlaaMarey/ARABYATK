using Arabytak.Core.Entities;
using Arabytak.Core.Repositories.Contract;
using Arabytak.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arabytak.Repository.Repository.Contract
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArabytakContext _context;
        private Hashtable TableForRepos = new Hashtable();

        public UnitOfWork(ArabytakContext context)
        {
            _context = context;
        }


        public IGenericRepository<T> Repositorey<T>() where T : BaseEntity
        {
            if (TableForRepos is null)
                TableForRepos = new Hashtable();

            var InstanceKey = typeof(T).Name;

            if (!TableForRepos.ContainsKey(InstanceKey))
            {
                var InstanseType = typeof(GenericRepository<>);

                var InstanceValue = Activator.CreateInstance(InstanseType.MakeGenericType(typeof(T)), _context);

                TableForRepos.Add(InstanceKey, InstanceValue);
            }
            return (IGenericRepository<T>)TableForRepos[InstanceKey];

        }
    }
}