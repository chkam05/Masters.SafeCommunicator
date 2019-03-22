using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Tool {

    // ####################################################################################################
    //  xxxx     xxx    xxxxx    xxx        xxxx   xxxxx    xxx    xxxxx   xxxxx    xxx 
    //   x  x   x   x     x     x   x      x         x     x   x     x       x     x   x
    //   x  x   xxxxx     x     xxxxx       xxx      x     xxxxx     x       x     x    
    //   x  x   x   x     x     x   x          x     x     x   x     x       x     x   x
    //  xxxx    x   x     x     x   x      xxxx      x     x   x     x     xxxxx    xxx 
    // ####################################################################################################
    public static class DataStatic {
        
        public static readonly string[] DayOfWeek = {
            "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"
        };

        // ------------------------------------------------------------------------------------------
        public static readonly  string[] ShortDayOfWeek = {
            "pon", "wto", "śro", "czw", "pią", "sob", "nie"
        };

        // ------------------------------------------------------------------------------------------
        public static readonly string[] MonthName = {
            "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec",
            "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
        };

        // ------------------------------------------------------------------------------------------
        public static readonly string[] ShortMonthName = {
            "sty", "lut", "mar", "kwi", "maj", "cze", "lip", "sie", "wrz", "paz", "lis", "gru"
        };

    }

    // ####################################################################################################
}
