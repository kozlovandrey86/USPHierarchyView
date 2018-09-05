using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphVizBuilderLib
{
    public class GraphBuilderProvider
    {
        ScriptBuilder _scriptBuilder;
        DBProvider _dbProvider;
        GraphBuilder _graphBuider;
        string _connString;
        public GraphBuilderProvider()
        {
            _scriptBuilder = new ScriptBuilder();
            _dbProvider = new DBProvider();
            _graphBuider = new GraphBuilder();

            _connString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

        }
        public void BuildDependsOnGraph(string uspName)
        {
            var query = _scriptBuilder.GetDependsOnScript(uspName);
            string graph = GetStringGraph(query);
            _graphBuider.CreateFileGraph($"{uspName} depends on.png", graph);
        }
        public void BuildOnDependsGraph(string uspName)
        {
            var query = _scriptBuilder.GetOnDependsScript(uspName);
            string graph = GetStringGraph(query);
            _graphBuider.CreateFileGraph($"on depends {uspName}.png", graph);
        }

        private string GetStringGraph(string query)
        {
            IEnumerable<Link> hierarhy = _dbProvider.ExecQuery<Link>(query);
            return _graphBuider.BuildGraph(hierarhy);
        }
    }
}
