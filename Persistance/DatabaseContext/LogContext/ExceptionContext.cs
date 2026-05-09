using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistance.DatabaseContext.LogContext
{

    public class ExceptionContext : DbContext
    {
        public ExceptionContext(DbContextOptions<ExceptionContext> options) : base(options)
        {
        }


        public DbSet<ExceptionLog> ExceptionLog { get; set; }
        //public DbSet<SerilogTbl> SerilogTbl { get; set; }


    }
}
