using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azzi_Sprite_Compiler_v2
{
    class Str
    {
        public static string[] explode(string delimiter, string explodeme)
        {
            return explodeme.Split(new string[] { delimiter }, StringSplitOptions.None);
        }

        public static string implode(string glue, string[] implodeme)
        {
            return string.Join(glue, implodeme);
        }
    }
}
