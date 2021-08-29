using System.Data;

namespace Courses.Data.Context
{
    public class DapperContext
    {
        private readonly IDbConnection _dapperConnection;

        public DapperContext(IDbConnection dapperConnnection)
        {
            _dapperConnection = dapperConnnection;
        }

        public IDbConnection DapperConnection
        {
            get
            {
                return _dapperConnection;
            }
        }
    }
}
