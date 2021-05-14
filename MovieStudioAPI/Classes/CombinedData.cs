﻿using MovieStudioAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStudioAPI.Classes
{
    public class CombinedData : IMetadata
    {
        public int MovieId { get ; set ; }
        public string Title { get ; set ; }
        public string Language { get ; set ; }
        public string Duration { get ; set ; }
        public int ReleaseYear { get ; set ; }
        public int watchDurationMs { get ; set ; }
    }
}
