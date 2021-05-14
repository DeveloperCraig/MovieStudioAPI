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
            //var movieQuery = new MovieQuery();
            //Stats[] statsSatas = movieQuery.MovieStats();

            //return statsSatas;

            try
            {
                var testingClass = new Testing.TestingClass();
                return testingClass.MovieStats();

            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
