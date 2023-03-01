using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TopicsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ExecuteController : ControllerBase
{
    private readonly ILogger<ExecuteController> _logger;

    public ExecuteController(ILogger<ExecuteController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "Get")]

    public string GetDataTable()
    {
        var reuslt = new Exception.Efficiency(() =>
        {
            return "Hello World";
        }).getEfficiency();

        return JsonConvert.SerializeObject(reuslt);
    }





}
