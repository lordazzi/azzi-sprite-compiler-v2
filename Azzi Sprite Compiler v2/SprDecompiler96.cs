using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Usando o bitmap
using System.Drawing;
// me permite trabalhar com arquivos
using System.IO;

namespace Azzi_Sprite_Compiler_v2
{
    class SprDecompiler96 : SprDecompiler
    {
        private UInt32 spritesAmount;

        public SprDecompiler96(TibiaFile spr)
        {
            if (File.Exists(spr.getFile()))
            {
                //lendo assinatura
                BinaryReader readspr = new BinaryReader(File.OpenRead(spr.getFile()));
                this.signature[0] = readspr.ReadByte();
                this.signature[1] = readspr.ReadByte();
                this.signature[2] = readspr.ReadByte();
                this.signature[3] = readspr.ReadByte();

                spritesAmount = readspr.ReadUInt32();//quantidade de sprites
                this.SprBmp = new Bitmap[spritesAmount + 1];//array de sprites
                this.offset = new UInt32[spritesAmount];//array de offsets

                for (int i = 1; i < spritesAmount; i++)
                {
                    this.offset[i] = readspr.ReadUInt32();
                }

                for (int i = 1; i < spritesAmount; i++)
                {
                    UInt16 length;
                    UInt32 offset_final;
                    Int32 px = 0;
                    byte[] pixel_array = new byte[3072], transparent = new byte[3];

                    if (this.offset[i] == 0)
                    {
                        this.SprBmp[i] = Program.nullimage;
                    }

                    else
                    {
                        readspr.BaseStream.Seek(this.offset[i], SeekOrigin.Begin);
                        //cor transparente, normalmente 255 0 255
                        transparent[0] = readspr.ReadByte();
                        transparent[1] = readspr.ReadByte();
                        transparent[2] = readspr.ReadByte();

                        length = readspr.ReadUInt16();
                        offset_final = this.offset[i] + length;
                        this.SprBmp[i] = new Bitmap(32, 32);
                        while (readspr.BaseStream.Position < offset_final && px != 3072)
                        {
                            UInt16 transparent_px = readspr.ReadUInt16();
                            UInt16 normal_px = readspr.ReadUInt16();

                            for (int j = 0; j < transparent_px; j++)
                            {
                                if (px != 3072)
                                {
                                    pixel_array[px] = 255; //blue
                                    px++;
                                    pixel_array[px] = 0; //green
                                    px++;
                                    pixel_array[px] = 255; //red
                                    px++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            for (int j = 0; j < normal_px; j++)
                            {
                                if (px != 3072)
                                {
                                    byte red = readspr.ReadByte(), green = readspr.ReadByte(), blue = readspr.ReadByte();
                                    pixel_array[px] = blue; //blue
                                    px++;
                                    pixel_array[px] = green; //green
                                    px++;
                                    pixel_array[px] = red; //red
                                    px++;
                                }

                                else
                                {
                                    break;
                                }
                            }
                        }

                        for (int j = px; j < 3072; j++)
                        {
                            pixel_array[j] = 255; //blue
                            j++;
                            pixel_array[j] = 0; //green
                            j++;
                            pixel_array[j] = 255; //red
                        }

                        this.SprBmp[i] = CopyDataToBitmap(pixel_array);
                    }

                }
            }
        }
    }
}
