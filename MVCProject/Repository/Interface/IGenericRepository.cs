using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Add(T item);
        void Remove(int id);
        void Update(T item);
        T FindByID(int id);
        IEnumerable<T> FindAll();

    }
}