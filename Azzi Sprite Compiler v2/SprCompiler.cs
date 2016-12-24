using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//trabalhando com imagens
using System.Drawing;

namespace Azzi_Sprite_Compiler_v2
{
    class SprCompiler
    {
        //função responsável por compilar as imagens individualmente
        public byte[] CompileImg(Image sprite)
        {
            byte[] compiled = new byte[0], coloredgroup = new byte[0], add = new byte[3], coloreds, transps;
            //Quantos pixels coloridos? Quantos transparentes?
            UInt16 colored = 0, transp = 0;
            //Declarando os pixels
            Color pixel, nextPx;
            Bitmap mysprite = new Bitmap(sprite);
            //Essa variavel será sempre a mesma
            byte[] transparent_color;
            transparent_color = new byte[3];
            transparent_color[0] = 255;
            transparent_color[1] = 0;
            transparent_color[2] = 255;

            for (int y = 0; y < mysprite.Height; y++)
            {
                for (int x = 0; x < mysprite.Width; x++)
                {

                    pixel = mysprite.GetPixel(x, y);
                    if (isMagenta(pixel))
                    {
                        transp++;
                    }

                    else
                    {
                        //Adicionando o novo pixel
                        add[0] = pixel.R;
                        add[1] = pixel.G;
                        add[2] = pixel.B;

                        //Avisando que mais um pixel colorido foi encontrado
                        colored++;

                        //Próximo pixel
                        nextPx = nextPixel(mysprite, x, y);

                        coloredgroup = Arrays.groupByteArray(coloredgroup, add);
                        //se o proximo pixel for magenta OU se esse for o ultimo e for colorido
                        if (x == 31 && y == 31 && isMagenta(pixel) == false || isMagenta(nextPx) == true)
                        {
                            //Salvando tudo
                            transps = Conversor.UInt16_to_Byte(transp);
                            coloreds = Conversor.UInt16_to_Byte(colored);
                            compiled = Arrays.groupByteArray(compiled, transps);
                            compiled = Arrays.groupByteArray(compiled, coloreds);
                            compiled = Arrays.groupByteArray(compiled, coloredgroup);

                            //Reiniciando
                            coloredgroup = new byte[0];
                            transp = 0;
                            colored = 0;
                        }
                    }
                }
            }

            compiled = Arrays.groupByteArray(Conversor.UInt16_to_Byte(Conversor.Int32_to_UInt16(compiled.Length)), compiled);
            compiled = Arrays.groupByteArray(transparent_color, compiled);
            return compiled;
        }

        public static bool isMagenta(Color px)
        {
            if (px.R == 255 && px.G == 0 && px.B == 255)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static Color nextPixel(Bitmap myImage, int myX, int myY)
        {
            myX++;
            //Ultimo pixel X
            if (myX == myImage.Width)
            {
                myY++;
                myX = 0;
                if (myY == myImage.Height)
                {
                    myY = 0;
                }
                return myImage.GetPixel(myX, myY);
            }

            else
            {
                return myImage.GetPixel(myX, myY);
            }
        }
    }
}