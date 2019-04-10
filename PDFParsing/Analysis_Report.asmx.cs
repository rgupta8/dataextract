using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace PDFParsing
{
    /// <summary>
    /// Summary description for Analysis_Report
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Analysis_Report : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(EnableSession = true, Description = "Returns All PARSER DATA")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void Get_PARSER_REPORT(string PARSER_ID)
        {
            PDFParseDataContext dbcontext = new PDFParseDataContext();
            var data = dbcontext.GetReport(Convert.ToInt32(PARSER_ID));
            //DataTable dt = LINQResultToDataTable(data);
            //SerializeAndWriteDataTableASJson(dt);
        }

        //public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        //{
        //    DataTable dt = new DataTable();
        //    PropertyInfo[] columns = null;
        //    if (Linqlist == null) return dt;
        //    foreach (T Record in Linqlist)
        //    {
        //        if (columns == null)
        //        {
        //            columns = ((Type)Record.GetType()).GetProperties();
        //            foreach (PropertyInfo GetProperty in columns)
        //            {
        //                Type colType = GetProperty.PropertyType;

        //                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
        //                == typeof(Nullable<>)))
        //                {
        //                    colType = colType.GetGenericArguments()[0];
        //                }

        //                dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
        //            }
        //        }
        //        DataRow dr = dt.NewRow();
        //        foreach (PropertyInfo pinfo in columns)
        //        {
        //            dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
        //            (Record, null);
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}

        public void SerializeAndWriteDataTableASJson(DataTable dt)
        {
            string tablename = dt.TableName;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            Context.Response.Clear();
            serializer.MaxJsonLength = Int32.MaxValue;
            Context.Response.ContentType = "application/json";
            string content = serializer.Serialize(rows);
            //string total = ",{\"total\":" + dt.Rows.Count.ToString() + "}";
            Context.Response.Write(content.Insert(content.Length - 1, ""));
        }
    }
}
