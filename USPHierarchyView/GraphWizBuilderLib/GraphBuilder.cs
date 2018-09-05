using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphVizBuilderLib
{
    class GraphBuilder
    {
        GraphGeneration _wrapper;
        public GraphBuilder()
        {
            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);
            _wrapper = new GraphGeneration(getStartProcessQuery, getProcessStartInfoQuery, registerLayoutPluginCommand);
        }

        public string BuildGraph(IEnumerable<Link> hierarchyCalls)
        {
            return hierarchyCalls.Aggregate(string.Empty, (accum, next) => accum += $"{next.ReferencingObject}  ->  {next.ReferencedObject} ;");
        }

        public void CreateFileGraph(string fileName, string graph)
        {
            byte[] output = _wrapper.GenerateGraph($"digraph {{ {graph} }}", Enums.GraphReturnType.Png);
            File.WriteAllBytes(fileName, output);
        }

    }
}
