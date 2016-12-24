using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//trabalhando com imagens
using System.Drawing;

namespace Azzi_Sprite_Compiler_v2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        //Agrupando array de bytes
        public static byte[] groupByteArray(byte[] myarray, byte[] add)
        {
            byte[] retorna = new byte[myarray.Length + add.Length];
            for (int i = 0; i < myarray.Length; i++)
            {
                retorna[i] = myarray[i];
            }

            int j = 0;
            for (int i = myarray.Length; i < retorna.Length; i++)
            {
                retorna[i] = add[j];
                j++;
            }

            return retorna;
        }


        public static Bitmap nullimage;
        
        [STAThread]
        static void Main()
        {
            byte[] magenta = new byte[3072];
            for (int j = 0; j < 3072; j++)
            {
                magenta[j] = 255; j++;
                magenta[j] = 0; j++;
                magenta[j] = 255;
            }
            nullimage = SprDecompiler.CopyDataToBitmap(magenta);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
