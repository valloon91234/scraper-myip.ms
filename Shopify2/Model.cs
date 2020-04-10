using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopify2
{
    class Model
    {
        public int Id { get; set; }
        public String Domain { get; set; }
        public String IP { get; set; }
        public String Company { get; set; }
        public String Location { get; set; }
        public String City { get; set; }
        public int Rating { get; set; }
        public String Email { get; set; }
        public String Error { get; set; }

        public override String ToString()
        {
            return Domain + "\t" + IP + "\t" + Company + "\t" + Location + "\t" + City + "\t" + Rating + "\t" + Email;
        }
    }
}
