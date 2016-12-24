using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//me permite chamar uma janela que escolha uma arquivo ou uma pasta
using System.Windows.Forms;

namespace Azzi_Sprite_Compiler_v2
{
    class TibiaFile
    {
        private string path = "";
        private string file = "";
        private string filename = "";
        public bool success;

        public TibiaFile(string title)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Tibia Sprites|*.spr";
            ofd.Title = title;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string pathfile = ofd.FileName.ToString();
                string[] retorno = Str.explode("\\", pathfile), caminho = new string[retorno.Length - 1];
                for (int i = 0; i < retorno.Length - 1; i++)
                {
                    caminho[i] = retorno[i];
                }

                this.file = pathfile;
                this.path = Str.implode("\\", caminho);
                this.filename = retorno[retorno.Length - 1];
                this.success = true;
            }
        }

        public string getPath()
        {
            return this.path;
        }

        public string getFile()
        {
            return this.file;
        }

        public string getFileName()
        {
            return this.filename;
        }
    }
}
