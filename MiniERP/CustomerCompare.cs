using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP
{
    public class CustomerCompare : IEqualityComparer<CustomerModel>
    {
        public bool Equals(CustomerModel x, CustomerModel y)
        {
            var isEqual = false;
            if(x.Customer == y.Customer &&
                x.CustomerCode == y.CustomerCode &&
                x.Address == y.Address &&
                x.Email == y.Email &&
                x.Telephone == y.Telephone)
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
