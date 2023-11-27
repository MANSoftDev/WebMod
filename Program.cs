using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace WebMod
{
    class Program
    {
        static void Main(string[] args)
        {
            SPWebApplication app = SPWebApplication.Lookup(new Uri("http://sp2010:2000"));
            Modifcations.MakeMods(app);
        }

    }
}
