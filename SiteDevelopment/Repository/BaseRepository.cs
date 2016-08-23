using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteDevelopment.Repository
{
    public abstract class BaseRepository
    {
        protected string ConnectionString;

        protected BaseRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}