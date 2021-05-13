using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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

        public Metadata[] FilterList(int id)
        {
            var reader = new StreamReader(Environment.CurrentDirectory + "/Docs/metadata.csv");
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //var records = csv.GetRecords<Metadata>();

                var reslts = csv.GetRecords<Metadata>()
                    .OrderBy(i => i.MovieId)
                    .Where(i => i.MovieId == id && i.Duration.Length == 8)
                    .Distinct(new DistinctItemComparer())
                    .Select(i => i).OrderBy(o => o.Language).ToArray();

                return reslts;

            }
        }
    }

    class DistinctItemComparer : IEqualityComparer<Metadata>
    {
        public bool Equals([AllowNull] Metadata x, [AllowNull] Metadata y )
        {
            return
               x.Language == y.Language;
        }

        public int GetHashCode([DisallowNull] Metadata obj)
        {
            return obj.MovieId.GetHashCode() ^
                //obj.Title.GetHashCode() ^
                obj.Language.GetHashCode() ^
                obj.Duration.GetHashCode() ^
                obj.ReleaseYear.GetHashCode();
        }
    }

}
