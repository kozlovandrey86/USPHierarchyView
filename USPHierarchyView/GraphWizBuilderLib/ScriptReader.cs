using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GraphVizBuilderLib
{
    class ScriptBuilder
    {
        public string GetDependsOnScript(string uspName, string path=null)
        {
            var _path = path ?? Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\DependsOn.sql");
            return GetSUPText(uspName, _path);
        }
        public string GetOnDependsScript(string uspName, string path = null)
        {
            var _path = path ?? Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\OnDependes.sql");
            return GetSUPText(uspName, _path);
        }

        private string GetSUPText(string uspName, string path)
        {
            var recursionOnDependesQueryTemplate = File.ReadAllText(path);
            return recursionOnDependesQueryTemplate.Replace("{uspname}", uspName);
        }
    }
}
