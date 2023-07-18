using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloBlake.Web.Controllers
{
    public class PageComponents : Controller
    {
        private readonly List<string> nodeTypeAliases;

        public PageComponents()
        {
            nodeTypeAliases = new List<string> { "blockFolder", "utilityFolder", "contentFolder", "altLink", "xmlSitemap" };
        }

        public bool IsNodeTypeAlias(string contentTypeAlias)
        {
            return nodeTypeAliases.Contains(contentTypeAlias, StringComparer.OrdinalIgnoreCase);
        }
    }
}
