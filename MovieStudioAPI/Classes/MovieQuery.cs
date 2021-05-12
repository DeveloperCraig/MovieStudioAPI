using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStudioAPI.Classes
{
    public class MovieQuery
    {
        public void AddMovie(MovieData Data, string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("{0},{1},{2},{3},{4}", Data.MovieId, Data.Title, Data.Language, Data.Duration, Data.ReleaseYear);
            }
        }
    }
}
