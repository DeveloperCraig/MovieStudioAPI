using Microsoft.AspNetCore.Mvc;
using MovieStudioAPI.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieStudioAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        // GET <MetadataController>/5
        [HttpGet("{id}")]
        public ActionResult <Metadata[]> Get(int id)
        {
            try
            {
                var movieQuery = new MovieQuery();
                Metadata[] metadatas = movieQuery.FilterMovies(id);

                if (metadatas.Length <= 0)
                {
                    //Note: if it comes back with no results then show 404
                    return NotFound();
                }
                else
                {
                    metadatas.OrderBy(o => o.Language);
                    return metadatas;
                }
            }
         
            catch (Exception e)
            {

                throw e;
            }
           
        }

        // POST <MetadataController>
        [HttpPost]
        public void Post([FromBody] MovieData value)
        {
            try
            {
                var movieQuery = new MovieQuery();
                movieQuery.AddMovie(value, Environment.CurrentDirectory + "/Docs/Database.txt" );
            }
            catch (Exception e)
            {

                throw e;
            }
            

        }
    }
}
