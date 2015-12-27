using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualView_With_CustomRazorViewEngine.MVC.App_Start
{
    public static class ViewEngineConfig
    {
        public static void RegisterViewEngine()
        {
            //Register View Engine
            ViewEngines.Engines.Insert(0, new CustomRazorViewEngine());
        }
    }
     
    /// <summary>
    /// Custom Razor View Engine
    /// </summary>
    class CustomRazorViewEngine : RazorViewEngine
    {
        /// <summary>
        /// Get custom file extension
        /// </summary>
        private readonly string _fileExtension = ".cshtml";
        private string _absolutePath = string.Empty;
        /// <summary>
        /// Get virtual path from "Web.Config"
        /// </summary>
        private readonly string _virtualPath;

        /// <summary>
        ///   
        /// </summary>
        public CustomRazorViewEngine()
        {
            this.ViewLocationFormats = new string[]
            {
                //{0} - Culture Name, {1} - Controller, {2} - Page, {3} Extension (aspx/ascx)
                "~/Views/{0}/{1}"
            };

            _virtualPath = ConfigurationManager.AppSettings["m:VirtualPath"].ToString().ToLower() ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="viewName"></param>
        /// <param name="masterName"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            return FindView(controllerContext, viewName, masterName, useCache, _fileExtension);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="viewName"></param>
        /// <param name="masterName"></param>
        /// <param name="useCache"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        private ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache,string extension)
        {
             _absolutePath = controllerContext.RequestContext.HttpContext.Request.Url.AbsolutePath.ToLower();
            if (_absolutePath.StartsWith(_virtualPath))
            {
                return new ViewEngineResult(CreateView(controllerContext, _absolutePath + extension, masterName), this);
            }
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
    }

}