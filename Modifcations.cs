using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;

namespace WebMod
{
    public class Modifcations
    {
        private static SPWebConfigModification ChildNode = new SPWebConfigModification
        {
            Path = "configuration/appSettings",
            Name = string.Format("add [@key='myAttribute'] [@value='{0}']", 1),
            Sequence = 1,
            Owner = "CodeProject",
            Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode,
            Value = string.Format("<add key='myAttribute' value='{0}' />", 1)
        };

        private static SPWebConfigModification ChildNode2 = new SPWebConfigModification
        {
            Path = "configuration/appSettings",
            Name = string.Format("add [@key='myAttribute'] [@value='{0}']", 1),
            Sequence = 2,
            Owner = "CodeProject",
            Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode,
            Value = string.Format("<add key='myAttribute2' value='{0}' />", 2)
        };

        private static SPWebConfigModification Attribute = new SPWebConfigModification
        {
            Path = "configuration/appSettings/add[@key='myAttribute']",
            Name = "value",
            Sequence = 0,
            Owner = "CodeProject",
            Type = SPWebConfigModification.SPWebConfigModificationType.EnsureAttribute,
            Value = "42"
        };

        private static SPWebConfigModification Section = new SPWebConfigModification
        {
            Path = "configuration",
            Name = "mySection",
            Owner = "CodeProject",
            Type = SPWebConfigModification.SPWebConfigModificationType.EnsureSection
        };

        public static void MakeMods(SPWebApplication webApp)
        {
            //EnsureChildNode(webApp);
            //EnsureChildNodeWithSequence(webApp);
            //EnsureAttribute(webApp);
            //EnsureSection(webApp);
            RemoveMods(webApp);
        }

        private static void EnsureChildNode(SPWebApplication webApp)
        {
            webApp.WebConfigModifications.Add(ChildNode);
            webApp.Farm.Services.GetValue<SPWebService>().ApplyWebConfigModifications();
        }

        private static void EnsureChildNodeWithSequence(SPWebApplication webApp)
        {
            webApp.WebConfigModifications.Add(ChildNode);
            // Only this one will be added since the sequence is higher than
            // the previous modification
            webApp.WebConfigModifications.Add(ChildNode2);
            webApp.Farm.Services.GetValue<SPWebService>().ApplyWebConfigModifications();
        }
        
        private static void EnsureAttribute(SPWebApplication webApp)
        {
            // Make sure node has been added
            webApp.WebConfigModifications.Add(ChildNode);
            // Modify attribute
            webApp.WebConfigModifications.Add(Attribute);
            webApp.Farm.Services.GetValue<SPWebService>().ApplyWebConfigModifications();
        }

        private static void EnsureSection(SPWebApplication webApp)
        {
            webApp.WebConfigModifications.Add(Section);
            webApp.Farm.Services.GetValue<SPWebService>().ApplyWebConfigModifications();
        }

        private static void RemoveMods(SPWebApplication webApp)
        {
            List<SPWebConfigModification> mods = webApp.WebConfigModifications
                .Where(m => m.Owner == "CodeProject")
                .ToList();

            foreach(SPWebConfigModification mod in mods)
            {
                webApp.WebConfigModifications.Remove(mod);
            }

            webApp.Farm.Services.GetValue<SPWebService>().ApplyWebConfigModifications();
        }
    }
}
