using MovieStudioAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStudioAPI.Classes
{
    public class CombinedData
    {
        public int movieId { get ; set ; }
        public string title { get ; set ; }
        public int averageWatchDurationS { get ; set ; }
        public int watches { get; set; }
        public int ReleaseYear { get ; set ; }

    }
}
