using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Services
{
    public class GoogleLanguages
    {
        public List<GoogleLanguage> text { get; set; }
    }

    public class GoogleLanguage
    {
        public string language { get; set; }
        public string code { get; set; }
    }
}