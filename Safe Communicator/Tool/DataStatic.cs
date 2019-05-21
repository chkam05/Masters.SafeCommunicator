using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Tool {

    public static class DataStatic {
        
        #region Calendar Names
        // ##########################################################################################
        /// <summary> Tablica z pełnymi nazwami dni tygodnia. </summary>
        public static readonly string[] DayOfWeek = {
            "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"
        };

        // ------------------------------------------------------------------------------------------
        /// <summary> Tablica z krótkimi nazwami dni tygodnia. </summary>
        public static readonly  string[] ShortDayOfWeek = {
            "pon", "wto", "śro", "czw", "pią", "sob", "nie"
        };

        // ------------------------------------------------------------------------------------------
        /// <summary> Tablica z pełnymi nazwami miesięcy. </summary>
        public static readonly string[] MonthName = {
            "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec",
            "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
        };

        // ------------------------------------------------------------------------------------------
        /// <summary> Tablica z krótkimi nazwami miesięcy. </summary>
        public static readonly string[] ShortMonthName = {
            "sty", "lut", "mar", "kwi", "maj", "cze", "lip", "sie", "wrz", "paz", "lis", "gru"
        };

        #endregion Calendar Names
        // ##########################################################################################

    }

}
