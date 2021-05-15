using CsvHelper;
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

        /// <summary>
        /// This will add a movie to the Database.txt file
        /// </summary>
        /// <param name="Data">The new movie info</param>
        /// <param name="path">Where the database file is stored</param>
        public void AddMovie(MovieData Data, string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("{0},{1},{2},{3},{4}", Data.MovieId, Data.Title, Data.Language, Data.Duration, Data.ReleaseYear);
            }
        }


        /// <summary>
        /// This will return all the movies with the related ID and only show 1 per country 
        /// </summary>
        /// <param name="id">Id of movie</param>
        /// <returns>Metadata array of all the movies found</returns>
        public Metadata[] FilterMovies(int id)
        {
            //Note: This reads the CVS file
            var reader = new StreamReader(Environment.CurrentDirectory + "/Docs/metadata.csv");
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //Note: I am doing a LINQ query to try and sort the data
                var reslts = csv.GetRecords<Metadata>()
                    .OrderBy(i => i.MovieId)
                    //Note: "i.Duration.Length == 8" is set to make sure we only get a valid time based on the spec of having HH:MM:SS
                    .Where(i => i.MovieId == id && i.Duration.Length == 8)
                    //Note: This part was kinda new for me as I was still getting back 2 of the same county 
                    .Distinct(new DistinctItemComparer())
                    .Select(i => i).OrderBy(o => o.Language).ToArray();

                return reslts;

            }
        }


        /// <summary>
        /// This is meant combine 2 CSV documents "metadata & stats" and return filtered information.
        /// </summary>
        /// <returns>An array of information</returns>
        public CombinedData[] MovieStats()
        {
            List<Metadata> metaDataFile = new List<Metadata>();
            List<Stats> statsFile = new List<Stats>();

            #region Getting Csv Data
            using (var reader = new StreamReader(Environment.CurrentDirectory + "/Docs/metadata.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                metaDataFile = csv.GetRecords<Metadata>().ToList();
            }

            using (var reader2 = new StreamReader(Environment.CurrentDirectory + "/Docs/stats.csv"))
            using (var csv2 = new CsvReader(reader2, CultureInfo.InvariantCulture))
            {
                statsFile = csv2.GetRecords<Stats>().ToList();
            }

            #endregion

            var watchesById = statsFile.GroupBy(x => x.movieId).ToArray();


            var formatedStatsData =
                (from t in statsFile
                 group t by t.movieId into moviegroup
                 select new CombinedData
                 {
                     movieId = moviegroup.Key,
                     watches = (int)watchesById.Where(x => x.Key == moviegroup.Key).SelectMany(x => x).Count(),
                     averageWatchDurationS = (int)moviegroup.Average(x => x.watchDurationMs)
                 }).ToArray();



            var Result = metaDataFile.Join(
                formatedStatsData,
                x => x.MovieId,
                y => y.movieId,
                (x, y) => new CombinedData()
                {
                    movieId = x.MovieId,
                    averageWatchDurationS = y.averageWatchDurationS,
                    watches = y.watches,
                    ReleaseYear = x.ReleaseYear,
                    title = x.Title
                });

            Result.OrderBy(x => x.watches).ThenBy(x => x.ReleaseYear).Distinct().ToArray();

            return Result.Distinct(new DistinctCombinedDataComparer()).ToArray();


        }


    }




    /// <summary>
    /// In this class what I am doing is checking if the language is the same and if there is multiple of the same Lagrange 
    /// then remove it, this was the way I manage to remove duplicate country
    /// </summary>
    class DistinctItemComparer : IEqualityComparer<Metadata>
    {
        public bool Equals([AllowNull] Metadata x, [AllowNull] Metadata y)
        {
            return
               x.Language == y.Language;
        }

        public int GetHashCode([DisallowNull] Metadata obj)
        {
            // Title had to be removed other wise it would check to see if the titles were different and because they were
            // it would allow it to add the movie, however this allowed for multiple movies with the same county to show

            return obj.MovieId.GetHashCode() ^
                //obj.Title.GetHashCode() ^
                obj.Language.GetHashCode() ^
                obj.Duration.GetHashCode() ^
                obj.ReleaseYear.GetHashCode();
        }
    }


    class DistinctCombinedDataComparer : IEqualityComparer<CombinedData>
    {
        public bool Equals([AllowNull] CombinedData x, [AllowNull] CombinedData y)
        {
            return
            x.movieId == y.movieId;
        }

        public int GetHashCode([DisallowNull] CombinedData obj)
        {
            return
                //obj.movieId.GetHashCode();
            obj.title.GetHashCode();
                //obj.averageWatchDurationS.GetHashCode() ^
                //obj.ReleaseYear.GetHashCode() ^
                //obj.watches.GetHashCode();

        }
    }

}
