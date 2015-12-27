using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace VirtualView_With_CustomRazorViewEngine.MVC.VirtualFiles
{ 
    public class DatabaseVirtualFile : VirtualFile
    {
        private byte[] data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="data"></param>
        public DatabaseVirtualFile(string virtualPath, byte[] data)
            : base(virtualPath)
        {
            this.data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override System.IO.Stream Open()
        {
            return new MemoryStream(data, false);
        }
    }
}