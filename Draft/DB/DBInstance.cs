using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draft.DB
{
    public static class DBInstance
    {
        static Draft1Entities connection;
        static object objectLock = new object();
        public static Draft1Entities Get()
        {
            lock (objectLock)
            {
                if (connection == null)
                    connection = new Draft1Entities();
                return connection;
            }
        }
    }
}
