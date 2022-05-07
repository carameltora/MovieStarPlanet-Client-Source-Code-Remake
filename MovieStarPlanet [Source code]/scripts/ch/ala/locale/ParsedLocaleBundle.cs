using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ch.ala.locale
{
    internal class ParsedLocaleBundle
    {
        public string localeChain;
        public string bundleName;
        public Dictionary<string, string> parsedData;

        public void ParsedLocaleBundleVoid(string param1 = null, string param2 = null)
        {
            if(param1 != null)
            {
                localeChain = param1;
            }
            if (param2 != null)
            {
                bundleName = param2;
            }
        }
    }
}
