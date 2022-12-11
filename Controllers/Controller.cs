using Microsoft.AspNetCore.Mvc;

namespace apiSearch.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    [HttpGet(Name = "GetSearch")]
     public string Get(string search)
     {
        return PesquisaApp.PesquisaAsync(search).Result;
     }
}
