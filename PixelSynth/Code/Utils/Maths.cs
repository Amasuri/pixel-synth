using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code.Utils
{
    static public class Maths
    {
        static public int Map(int old_value, int old_bottom, int old_top, int new_bottom, int new_top)
        {
            return (int)(((float)old_value - (float)old_bottom) / ((float)old_top - (float)old_bottom) * ((float)new_top - (float)new_bottom) + (float)new_bottom);
        }
    }
}
