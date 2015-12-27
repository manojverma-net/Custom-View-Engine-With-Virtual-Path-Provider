using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using VirtualView_With_CustomRazorViewEngine.MVC.VirtualFiles;

namespace VirtualView_With_CustomRazorViewEngine.MVC.PathProviders
{
    public class CustomVirtualPathProvider : VirtualPathProvider
    {
        /// <summary>
        /// Get Virtual Path from "Web.Config"
        /// </summary>
        private static readonly string _virtualPath = ConfigurationManager.AppSettings["m:VirtualPath"].ToString().ToLower();

        /// <summary>
        /// Check File existance
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override bool FileExists(string virtualPath)
        {
            if (IsPathVirtual(virtualPath))
            {
                //Check database has content for request
                return !String.IsNullOrEmpty("html content from database");
            }

            return base.FileExists(virtualPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsPathVirtual(virtualPath))
            { 
                string content = GetFileContent();
                return new DatabaseVirtualFile(virtualPath, System.Text.Encoding.ASCII.GetBytes(content));
            }
            return base.GetFile(virtualPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="virtualPathDependencies"></param>
        /// <param name="utcStart"></param>
        /// <returns></returns>
        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsPathVirtual(virtualPath))
            {
                return null;
            }
            return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        /// <summary>
        /// Method will return new GUID for every request, So caching will not work.
        /// To enable caching retun null insted of GUID
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="virtualPathDependencies"></param>
        /// <returns></returns>
        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            if (IsPathVirtual(virtualPath))
            {
                return Guid.NewGuid().ToString();
            }
            return base.GetFileHash(virtualPath, virtualPathDependencies);
        }

        /// <summary>
        /// Check Is Virtual Path 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        private static bool IsPathVirtual(string virtualPath)
        {
            return virtualPath.StartsWith(_virtualPath.ToLower());
        }

        /// <summary>
        /// Get File content from database
        /// </summary>
        /// <returns></returns>
        private static string GetFileContent()
        {
            return "<div>" + DateTime.UtcNow.ToLongTimeString() + " Label : @Html.TextBox(\"thanks\")</div><br/> Demo App by Manoj Verma";
        }

    }
}