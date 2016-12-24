using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//permite abrir pastas
using System.Windows.Forms;
//me permite trabalhar com arquivos externos
using System.IO;
using System.Diagnostics;

namespace Azzi_Sprite_Compiler_v2
{
    class TibiaFolder
    {
        private string path = "";
        public bool success;

        public TibiaFolder(string title)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = title;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.path = fbd.SelectedPath.ToString();
                    this.success = true;
                }

                else
                {
                    this.success = false;
                }
            }

            catch
            {
                this.success = false;
            }
        }

        public UInt32 getSprites()
        {
            FileInfo[] di = new DirectoryInfo(this.getPath()).GetFiles("*.bmp");
            return Conversor.Int32_to_UInt32(di.Length);
        }

        public byte[] getSignature()
        {
            byte[] signature = new byte[4];
            BinaryReader signa = new BinaryReader(File.OpenRead(this.getPath() + "\\signature.dat"));
            signature[0] = signa.ReadByte();
            signature[1] = signa.ReadByte();
            signature[2] = signa.ReadByte();
            signature[3] = signa.ReadByte();
            return signature;
        }

        public string getPath()
        {
            return this.path;
        }

        public string generateFileName()
        {
            string name = "Azzi";
            int i = 2;
            while (File.Exists(this.path + "/" + name + ".spr"))
            {
                name = "Azzi " + i.ToString();
                i++;
            }
            return this.path + "/" + name + ".spr";
        }
    }
}
