using DevExpress.Images;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseMedicineInputSystem.Helpers
{
    public class FilePathHelper
    {
        public static string GetFullPath(string name)
        {
            var type = Type.GetType("DevExpress.DemoData.Helpers.DataFilesHelper, " + AssemblyInfo.SRAssemblyDemoDataCore + ", Version=" + AssemblyInfo.Version + ", Culture=neutral, PublicKeyToken=" + AssemblyInfo.PublicKeyToken);
            var method = type.GetMethod("FindFile", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            var propValue = "Data";
            return (string)method.Invoke(null, new object[] { name, propValue });
        }
        public static Uri GetDXImageUri(string path)
        {
            return AssemblyHelper.GetResourceUri(typeof(DXImages).Assembly, string.Format("Office2013/{0}.png", path));
        }
        public static Uri GetAppImageUri(string path, UriKind uriKind = UriKind.Relative)
        {
            return new Uri(string.Format("/ChineseMedicineInputSystem;component/Images/{0}.png", path), uriKind);
        }
    }
}
