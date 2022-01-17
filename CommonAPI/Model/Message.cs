using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonAPI.Model
{
    public class Message
    {

        

        public class Root
        {
            public From from { get; set; }
            public To to { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
            public bool show_noreply_warning { get; set; }
        }


        public class From
        {
            public string name { get; set; }
        }

        public class To
        {
            public string name { get; set; }
            public string address { get; set; }
        }

    }
}
