using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Controllers
{
    public class Chart
    {
        public string type { get; set; }
        public int[] data { get; set; }
        public Chart(string type, int[] data)
        {
            this.type = type;
            this.data = data;
        }
    }
}
