using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStudioAPI.Interfaces
{
   public interface IStats
    {
        public int movieId { get; set; }
        public int watchDurationMs { get; set; }
    }
}
