using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Parser.Heplers
{
    public static class LostFilmSerailListHelper
    {
        private const string KeywordStatus = "Статус:";
        private const string KeywordYearStart = "Год выхода:";
        private const string KeywordCanal = "Канал:";
        private const string KeywordGenre = "Жанр:";

        public static string GetKeywordStatus()
        {
            return KeywordStatus;
        }
        public static string GetKeywordYearStart()
        {
            return KeywordYearStart;
        }
        public static string GetKeywordCanal()
        {
            return KeywordCanal;
        }
        public static string GetKeywordGenre()
        {
            return KeywordGenre;
        }
    }
}
