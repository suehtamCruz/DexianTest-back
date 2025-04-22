using Microsoft.AspNetCore.Mvc;

namespace DexianTest_back.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentsController
    {
        [HttpGet()]
        public string GetAllStudents()
        {
            return "Hello World";
        }
    }
}
