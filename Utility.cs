using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AzurLaneDatabase
{
    public class Utility
    {
        /*
        public static Stream GetEmbeddedResourceStream(string resourceName) {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }

        public static string[] GetEmbeddedResourceNames() {
            return Assembly.GetExecutingAssembly().GetManifestResourceNames();
        }*/

        public static bool bParse(int input) {
            if(input == 1) return true;
            return false;
        }
    }
}
