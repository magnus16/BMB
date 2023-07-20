using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMB.Entities.Models
{
    public static class Enums
    {
        public enum GenreType
        {
            Scifi = 0,
            Action = 1,
            Drama = 2
        }

        public static string GetGenreText(GenreType type)
        {
            switch (type)
            {
                case GenreType.Drama: return "Drama";
                case GenreType.Action: return "Action";
                case GenreType.Scifi: return "Sci-Fi";
                default: return "";
            }
        }

    }
}
