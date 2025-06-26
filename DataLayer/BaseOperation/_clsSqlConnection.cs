using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace DataLayer.BaseOperation
{
     static class _clsSqlConnection
    {
        internal static string DynamicConnection(string ServerName, string DataBaseName) => ConfigurationManager.AppSettings["dynamic"];
        //{ return $@"Server=.;Database={DataBaseName};User ID=sa;Password=Qq123098qQ"; }

        internal static string connection => //"Server=.;Database=Project;User ID=sa;Password=Qq123098qQ";
                                             ConfigurationManager.ConnectionStrings["Connection"].ConnectionString; 
    }
}
