using Dapper.FastCrud;
using Microsoft.Extensions.Configuration;
using MVCProject.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Repository
{
    public class StaffRepository : IGenericRepository<NhanVien>,IStaffRepository
    {
        
        private string connectionString;
        public StaffRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            OrmConfiguration.DefaultDialect = SqlDialect.PostgreSql;
        }
        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(NhanVien item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NhanVien> FindAll()
        {
            using(IDbConnection conn = Connection){
                return conn.Find<NhanVien>(statement => statement.OrderBy($"{nameof(NhanVien.MaNhanVien):C} ASC"));
            }
        }

        public NhanVien FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(NhanVien item)
        {
            throw new NotImplementedException();
        }
    }
}