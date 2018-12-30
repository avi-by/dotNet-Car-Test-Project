using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   public class FactoryBL
    {
        public static IBL GetBL(string typeBL)
        {
            switch (typeBL)
            {
                case "myBL":
                    return MyBL.Instance;
            }
            return null;
        }
    }
}
