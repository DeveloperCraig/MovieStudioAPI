﻿using CsvHelper;
using MovieStudioAPI.Classes;
using MovieStudioAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStudioAPI.Testing
{
    public class TestingClass
    {
        public CombinedData[] MovieStats()
        {
            Stats[] stats = new Stats[] { };
            List<Metadata> ts1 = new List<Metadata>();
            List<Stats> ts2 = new List<Stats>();

            using (var reader = new StreamReader(Environment.CurrentDirectory + "/Docs/metadata.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                ts1 = csv.GetRecords<Metadata>().ToList();
            }

            using (var reader2 = new StreamReader(Environment.CurrentDirectory + "/Docs/stats.csv"))
            using (var csv2 = new CsvReader(reader2, CultureInfo.InvariantCulture))
            {
                ts2 = csv2.GetRecords<Stats>().ToList();
            }

            //var ts3 = from t1 in ts1
            //          join t2 in ts2 on t1.MovieId equals t2.movieId
            //          select new CombinedData() { MovieId = t1.MovieId, watchDurationMs = t2.watchDurationMs, Duration = t1.Duration, Language = t1.Language, ReleaseYear = t1.ReleaseYear, Title = t1.Title };


            var aver =
                (from t in ts2
                 group t by t.movieId into moviegroup
                 select new Stats
                 {
                     movieId = moviegroup.Key,
                     watchDurationMs = (int)moviegroup.Average(x => x.watchDurationMs)
                 }).ToArray();



            var ts3 = ts1.Join(
                aver,
                x => x.MovieId,
                y => y.movieId,
                (x, y) => new CombinedData()
                {
                    MovieId = x.MovieId,
                    watchDurationMs = y.watchDurationMs,
                    ReleaseYear = x.ReleaseYear,
                    Title = x.Title
                });


            return ts3.ToArray();


        }
    }
}
