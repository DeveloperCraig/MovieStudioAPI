using MovieStudioAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStudioAPI.Classes
{
    public class Stats : IStats
    {
        public int movieId { get; set; }
        public int watchDurationMs { get; set; }

    }
}
