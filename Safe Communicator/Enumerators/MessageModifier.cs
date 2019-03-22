using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safe_Communicator.Enumerators {

    // ####################################################################################################
    //  x   x   xxxxx    xxxx    xxxx    xxx     xxxx   xxxxx
    //  xx xx   x       x       x       x   x   x       x    
    //  x x x   xxxx     xxx     xxx    xxxxx   x  xx   xxxx 
    //  x   x   x           x       x   x   x   x   x   x    
    //  x   x   xxxxx   xxxx    xxxx    x   x    xxxx   xxxxx
    //
    //  x   x    xxx    xxxx    xxxxx   xxxxx   xxxxx   xxxxx   xxxx 
    //  xx xx   x   x    x  x     x     x         x     x       x   x
    //  x x x   x   x    x  x     x     xxxx      x     xxxx    xxxx 
    //  x   x   x   x    x  x     x     x         x     x       x   x
    //  x   x    xxx    xxxx    xxxxx   x       xxxxx   xxxxx   x   x
    // ####################################################################################################
    public enum MessageModifier {
        INCOMING,
        INFORMATION,
        OUTGOING,
        SERVER
    }

    // ####################################################################################################
}
