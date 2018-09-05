
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphVizBuilderLib;

namespace USPHierarchyView
{

    class Program
    {
        static void Main(string[] args)
        {
            var graphBuilder = new GraphBuilderProvider();
            graphBuilder.BuildDependsOnGraph("procedure_name");
            graphBuilder.BuildOnDependsGraph("procedure_name");
        }
    }
}
