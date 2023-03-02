using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TopicsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ExecuteController : ControllerBase
{
    private string ConnectionString { get; set; } = "";

    private readonly ILogger<ExecuteController> _logger;

    public ExecuteController(ILogger<ExecuteController> logger)
    {
        _logger = logger;
        ConnectionString = ConfigHelper.GetConnectionString("Default");
    }

    [HttpGet("Fill")]

    public string GetTableEfficiencyByFill()
    {
        var reuslt = new Exceptions.Efficiency(() =>
        {
            string Query = "SELECT * FROM WMB01_0000";
            var dataTable = GetDataTable_Fill(Query);
            Console.WriteLine("共有 {0} 筆資料", dataTable.Rows.Count);
        }).getEfficiency();

        return JsonConvert.SerializeObject(reuslt);
    }

    [HttpGet("Load")]

    public string GetTableEfficiencyByLoad()
    {
        var reuslt = new Exceptions.Efficiency(() =>
        {
            string Query = "SELECT * FROM WMB01_0000";
            var dataTable = GetDataTable_Load(Query);
            Console.WriteLine("共有 {0} 筆資料", dataTable.Rows.Count);
        }).getEfficiency();

        return JsonConvert.SerializeObject(reuslt);
    }

    [HttpGet("IEnumrable")]

    public string GetTableEfficiencyByIEnumrable()
    {
        var reuslt = new Exceptions.Efficiency(() =>
        {
            string Query = "SELECT * FROM WMB01_0000";
            var lists = GetDataTable_IEnumrable(Query);
            Console.WriteLine("共有 {0} 筆資料", lists.Count());
        }).getEfficiency();

        return JsonConvert.SerializeObject(reuslt);
    }

    DataTable GetDataTable_Fill(string pSql)
    {
        if (string.IsNullOrEmpty(pSql)) { throw new ArgumentException("字串不能為 null 或空字串"); }

        DataTable dt = new DataTable();

        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            SqlDataAdapter Adapter = new(pSql, connection);
            Adapter.Fill(dt);
        }

        return dt;
    }


    DataTable GetDataTable_Load(string pSql)
    {
        if (string.IsNullOrEmpty(pSql)) { throw new ArgumentException("字串不能為 null 或空字串"); }

        var dt = new DataTable();

        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            using (var command = new SqlCommand(pSql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }
        return dt;
    }

    IEnumerable<object> GetDataTable_IEnumrable(string pSql)
    {
        if (string.IsNullOrEmpty(pSql)) { throw new ArgumentException("字串不能為 null 或空字串"); }

        var dt = new DataTable();

        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            using (var command = new SqlCommand(pSql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return new
                        {
                            StoCode = reader.GetString(0),
                            StoName = reader.GetString(1),
                            StoAddress = reader.GetString(2),
                            StoType = reader.GetString(3),
                            StoIP = reader.GetString(4),
                            IsUSE = reader.GetString(5),
                            IsServer = reader.GetString(6),
                            StoLift = reader.GetInt32(7),
                            StoFloor = reader.GetString(8),
                            StoPort = reader.GetString(9),
                        };
                    }
                }
            }
        }
    }

    //------------ Private -------------------//

}
