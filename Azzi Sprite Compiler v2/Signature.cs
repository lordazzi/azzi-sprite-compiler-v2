using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//trabalhando com arquivos externos
using System.IO;

namespace Azzi_Sprite_Compiler_v2
{
    class Signature
    {
        private byte[] signature = new byte[4];
        public void save(TibiaFolder folder, string newFolderName)
        {
            FileStream dat = new FileStream(folder.getPath() + "\\" + newFolderName + "\\signature.dat", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(dat);
            writer.Write(signature[0]);
            writer.Write(signature[1]);
            writer.Write(signature[2]);
            writer.Write(signature[3]);
            writer.Flush();
            writer.Close();
        }

        public void set(byte[] sig)
        {
            this.signature[0] = sig[0];
            this.signature[1] = sig[1];
            this.signature[2] = sig[2];
            this.signature[3] = sig[3];
        }

        public void fromFile(TibiaFolder folder)
        {
            BinaryReader signa = new BinaryReader(File.OpenRead(folder.getPath() + "\\signature.dat"));
            this.signature[0] = signa.ReadByte();
            this.signature[1] = signa.ReadByte();
            this.signature[2] = signa.ReadByte();
            this.signature[3] = signa.ReadByte();
        }

        public byte[] get()
        {
            return signature;
        }
    }
}
