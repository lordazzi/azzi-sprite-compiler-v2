using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azzi_Sprite_Compiler_v2
{
    class Arrays
    {
        private string[] indexagem, values;

        public string Item(string index)
        {
            int i;
            string retorno = null;
            for (i = 0; i < this.indexagem.Length; i++)
            {
                if (this.indexagem[i] == index) {
                    retorno = values[i];
                    break;
                }
            }
            return retorno;
        }

        public void Item(string index, string value)
        {
            int i;
            bool find = false;
            for (i = 0; i < this.indexagem.Length; i++)
            {
                if (this.indexagem[i] == index)
                {
                    find = true;
                    break;
                }
            }

            if (find == true)
            {
                this.indexagem[i] = index;
                this.values[i] = value;
            }

            else
            {
                this.indexagem = Arrays.Add(indexagem, index);
                this.values = Arrays.Add(values, value);
            }
        }

        public static string[] Add(string[] strs, string str)
        {
            string[] nova = new string[strs.Length + 1];
            for (int i = 0; i < strs.Length; i++ )
            {
                nova[i] = strs[i];
            }
            nova[strs.Length] = str;
            return nova;
        }

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
    }
}