using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Enums
{
    public  enum ProductType
    {
        Physical = 1,
        Digital ,
        Service ,
        Subscription 
    } 
    
    public  enum ProductStatus
    {
        Draft =1,
        Active,
        Inactive,
        Discontinued
    }


}
