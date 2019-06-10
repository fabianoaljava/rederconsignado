using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLibrary
{
    public class Regras
    {
         public static bool PermiteImportacaoCarga(string status)
        {

            if (status == "A") return true; else return false;
               
        }
    }

}
