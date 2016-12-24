using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Lockbit/Unlockbits
using System.Drawing;
//Biblioteca que me permite trabalhar com imagens
using System.Drawing.Imaging;
//Marshal
using System.Runtime.InteropServices;

namespace Azzi_Sprite_Compiler_v2
{
    class SprDecompiler
    {
        protected byte[] signature = new byte[4];
        protected Bitmap[] SprBmp;
        protected UInt32[] offset;

        //Função deliciosa que eu achei na internet
        /* Função: CopyDataToBitmap
         * Objetivo: transforma um array de bytes em bmp
         * Autor: bledazemi
         * Link: http://www.tek-tips.com/viewthread.cfm?qid=1264492
         */
        public static Bitmap CopyDataToBitmap(byte[] data)
        {
            //Here create the Bitmap to the know height, width and format
            Bitmap bmp = new Bitmap(32, 32, PixelFormat.Format24bppRgb);

            //Create a BitmapData and Lock all pixels to be written 
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

            //Copy the data from the byte array into BitmapData.Scan0
            Marshal.Copy(data, 0, bmpData.Scan0, data.Length);

            //Unlock the pixels
            bmp.UnlockBits(bmpData);

            //Return the bitmap 
            return bmp;
        }

        public Bitmap[] getSprites()
        {
            return this.SprBmp;
        }

        public byte[] getSignature()
        {
            return signature;
        }
    }
}
