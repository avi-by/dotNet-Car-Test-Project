using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct Address
    {
        public string street;
        public int houseNumber;
        public string city;

        public Address(string street, int houseNumber, string city)
        {
            this.street = street;
            this.houseNumber = houseNumber;
            this.city = city;
        }
    }
}
