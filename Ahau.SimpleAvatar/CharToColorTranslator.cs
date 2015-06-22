using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahau.SimpleAvatar
{
    public static class CharToColorTranslator
    {
        public static Color ToColor(this char symbol)
        {
            return ColorTranslator.FromOle(symbol * 1300);
        }
    }
}
