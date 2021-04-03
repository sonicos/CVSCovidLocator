using System;
using System.Collections.Generic;
using System.Text;

namespace CVSCovidLocator
{
    class Config
    {
        public string endpoint { get; set; }
        public string apiKey { get; set; }
        public int minDelay { get; set; }
        public int maxDelay { get; set; }
        public List<string> searchZips { get; set; }

        
    }
}
