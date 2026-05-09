using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Exceptions
{
    public class DataBaseExcption : DbException
    {
        public int ErrorCode { get; set; }
        public DataBaseExcption()
        {

        }
        public DataBaseExcption(string message) : base(message) { }

        public DataBaseExcption(string message, int errorCode) : base(message, errorCode) { }

        public DataBaseExcption(string message, Exception innerException) : base(message, innerException) { }


        public override string ToString()
        {
            return base.ToString();
        }



    }
}
