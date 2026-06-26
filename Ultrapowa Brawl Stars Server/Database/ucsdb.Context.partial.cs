using System.Data.Common;
using System.Data.Entity;

namespace UCS.Database
{
    public partial class ucsdbEntities
    {
        public ucsdbEntities(DbConnection connection)
            : base(connection, true)
        {
        }

        public ucsdbEntities(string connectionString, bool useConnectionStringAsIs)
            : base(useConnectionStringAsIs ? connectionString : "name=" + connectionString)
        {
        }
    }
}
