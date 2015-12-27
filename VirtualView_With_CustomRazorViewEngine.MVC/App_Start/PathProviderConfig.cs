using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using VirtualView_With_CustomRazorViewEngine.MVC.PathProviders;

namespace VirtualView_With_CustomRazorViewEngine.MVC.App_Start
{
    public static class PathProviderConfig
    {
        public static void RegisterPathProvider()
        { 
            HostingEnvironment.RegisterVirtualPathProvider(new CustomVirtualPathProvider());
        }
    }
}