using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//trabalhando com arquivos externos
using System.IO;

namespace Azzi_Sprite_Compiler_v2
{
    class Configuration
    {
        private Arrays configs = new Arrays();
        private Arrays tmp_configs = new Arrays();
        public Configuration()
        {
            if (File.Exists("configutarion.log"))
            {
                //lendo arquivo de configuações
                string texto = "", lendo = "";
                TextReader reader = File.OpenText("configutarion.log");

                while ((lendo = reader.ReadLine()) != null)
                {
                    texto += lendo;
                }
                lendo = null;
                reader.Close();

                //setando as configurações
                string[] exploded, exploded_do_exploded;
                exploded = Str.explode(Environment.NewLine, texto);
                for (int j = 0; j < exploded.Length; j++ )
                {
                    exploded_do_exploded = Str.explode(" => ", exploded[j]);
                    configs.Item(exploded_do_exploded[0], exploded_do_exploded[1]);
                }
            }
        }

        public void setConfig()
        {

        }

        public string getLogin()
        {
            return configs.Item("login");
        }

        public string getPassword()
        {
            return configs.Item("password");
        }

        public double getVersion()
        {
            return double.Parse(configs.Item("version"));
        }
    }
}