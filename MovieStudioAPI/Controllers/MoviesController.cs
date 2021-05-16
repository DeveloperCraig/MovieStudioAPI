using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStudioAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStudioAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {


        // GET: MoviesController/stats
        [HttpGet("stats")]
        public ActionResult <CombinedData[]> stats()
        {
            try
            {
                var testingClass = new MovieQuery();
                return testingClass.MovieStats();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

       
    }
}
