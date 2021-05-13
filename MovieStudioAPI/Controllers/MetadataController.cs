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
        public Metadata[] Get(int id)
        {
            try
            {
                var t = new MovieQuery();
                Metadata[] metadatas = t.FilterList(id);

                if (metadatas.Length <= 0)
                {
                    throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.NotFound);
                }
                else
                {
                    metadatas.OrderBy(o => o.Language);
                    return metadatas;
                }


            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // POST <MetadataController>
        [HttpPost]
        public void Post([FromBody] MovieData value)
        {
            try
            {
                var t = new MovieQuery();
                t.AddMovie(value, Environment.CurrentDirectory + "/Docs/Database.txt" );
            }
            catch (Exception e)
            {

                throw e;
            }
            

        }

        // PUT <MetadataController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE <MetadataController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
