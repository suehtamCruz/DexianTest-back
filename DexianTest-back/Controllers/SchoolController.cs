using Microsoft.AspNetCore.Mvc;

namespace DexianTest_back.Controllers
{
    [ApiController]
    [Route("school")]
    public class SchoolController
    {
        [HttpGet()]
        public string GetAllSchools()
        {
            return "Hello World";
        }


    }
}
