using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP
{
    public class CustomerCodeCompare : IEqualityComparer<CustomerModel>
    {
        public bool Equals(CustomerModel x, CustomerModel y)
        {
            var isEqual = false;
            if (x.CustomerCode == y.CustomerCode)
            {
                isEqual = true;
            }
            return isEqual;
        }

        public int GetHashCode(CustomerModel obj)
        {
            return base.GetHashCode();
        }
    }
}
