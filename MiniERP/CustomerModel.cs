using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP
{
    public class CustomerModel
    {
        public string CustomerCode { get; set; }
        public string Customer { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

    }
}


//SELECT TOP(1000) [CustomerCode]
//      ,[Customer]
//      ,[Telephone]
//      ,[Address]
//      ,[Email]
//      ,[LastChangeTime]
//FROM[MiniERP].[dbo].[Customer]
