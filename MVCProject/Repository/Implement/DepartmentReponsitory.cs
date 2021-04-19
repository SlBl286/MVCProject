using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCProject.Repository;
using MVCProject.Models;
namespace MVCProject.Repository
{
    public class DepartmentReponsitory : IGenericRepository<PhongBan>, IDepartmentReponsitory
    {
        public void Add(PhongBan item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhongBan> FindAll()
        {
            throw new NotImplementedException();
        }

        public PhongBan FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(PhongBan item)
        {
            throw new NotImplementedException();
        }
    }
}