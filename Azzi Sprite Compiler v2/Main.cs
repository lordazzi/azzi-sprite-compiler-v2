using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
// Me permite abrir um link da internet
using System.Diagnostics;
//Me permite trabalhar com pastas
using System.IO;
//trabalhando com thread
using System.Threading;

namespace Azzi_Sprite_Compiler_v2
{
    public partial class Main : Form
    {
        #region variaveis de decompilação
        //Variaveis de extração
        TibiaFile extrair_quem; //variavel responsável por guardar a informação de qual arquivo será extraido
        TibiaFolder extrair_para_onde; //variavel responsável por guardar informações a respeito da pasta selecionada pelo usuário
        SprDecompiler70 sd70; //classe decompiladora de sprites na versão 7.0 até 9.54
        SprDecompiler96 sd96; //classe decompiladora de sprites da versão 9.6 para frente
        Signature assinatura = new Signature(); //assinatura que identifica a versão do cliente
        Bitmap[] sprites_to_export; //array que guardará as sprites a seres salvas no computador
        Thread sprites_exporter; //thread que vai executar a função de salvar as sprites
        string export_folder_name; //posição exata onde as sprites serão extraidas
        private delegate void setConsoleLog(string log); //não faço idéia do que é essa porcaria, maldito C# fica mandando eu declarar essas coisas sem sentido
        private delegate void lastConsoleLog(string log);
        #endregion
        #region variaveis de compilação
        //Variaveis de compilação
        TibiaFolder compilar_onde, compilar_para_onde; //compilar o que e para onde?
        Thread sprite_compiler70, sprite_compiler96; //Thread que contém as funções de compilação
        SprCompiler70 sc70 = new SprCompiler70(); //Responsável por compilar entre as versões 7.0 e 9.54
        SprCompiler96 sc96 = new SprCompiler96(); //Responsável por compilar acima das versões 9.6
        UInt32[] offset; //Variável responsável por receber o cálculo do deslocamento das sprites dentro do arquivo
        byte[] compiledSprite; //Array de arrays de bytes, esses bytes são as sprites compiladas
        byte[] readSignature; //Lendo a assinatura do client
        BinaryWriter compiling, compiled; //objetos responsáveis por escrever os bytes das sprites e manipular a memória estratégicamente \O/ uhu
        BinaryReader get_bin_file_from_virtual_memory; // objeto responsável por ler o arquivo binário que foi criado dentro da memória rígida, depois da leitura as informações são jogadas dentro do arquivo final
        #endregion

        public Main()
        {
            InitializeComponent();
        }

        #region threads
        //salvando sprites no computador
        #region Salvando as sprites depois da decompilação
        private void saveSprites()
        {
            setLog("[0%] 0 sprites from " + sprites_to_export.Length.ToString());
            for (int i = 1; i < sprites_to_export.Length; i++)
            {
                sprites_to_export[i].Save(export_folder_name + "\\s" + i.ToString() + ".bmp");
                lastLog("[" + percent(i, sprites_to_export.Length).ToString() + "%] " + i.ToString() + " sprites from " + sprites_to_export.Length.ToString());
            }
            setLog("Operation complete...");
        }
        #endregion
        #region Compilando para a versão 7.0
        //Thread responsável pela compilação de sprites para as versões entre 7.0 e 9.54
        private void compileTo70()
        {
            //Declarando variaveis
            bool wrongVersion = false, cantReadSignature = false;
            UInt16 quantidade_de_sprites = 0;
            Image spr;

            //lendo assinatura
            try
            {
                setLog("Reading signature...");
                assinatura.fromFile(compilar_onde);
                readSignature = assinatura.get();
            }

            catch
            {
                cantReadSignature = true;
            }

            try
            {
                setLog("Reading sprite number...");
                quantidade_de_sprites = Conversor.UInt32_to_UInt16(compilar_onde.getSprites());
            }
            catch
            {
                wrongVersion = true;
            }

            if (wrongVersion == true)
            {
                setLog("Unable to read all the sprites");
                setLog("The maximum supported sprites for a compilation between versions 7.0 and 9.54 is 65535 images.");
                setLog("Operation aborted...");
            }

            else if (cantReadSignature == true)
            {
                setLog("Error while trying to read signature.");
                setLog("Operation aborted...");
            }

            else
            {
                //organizando as variaveis para começar a leitura
                setLog("[0%] 0 sprites from " + quantidade_de_sprites.ToString());
                compiling = new BinaryWriter(new FileStream(compilar_para_onde.getPath() + "\\virtual_memory.bin", FileMode.Create));
                offset = new UInt32[quantidade_de_sprites];
                offset[0] = Conversor.Int32_to_UInt32((quantidade_de_sprites * 4) + 6);
                compiledSprite = new byte[quantidade_de_sprites + 1];

                //lendo as sprites aqui e criando as offsets
                for (int i = 1; i <= quantidade_de_sprites; i++)
                {
                    lastLog("[" + percent(i, quantidade_de_sprites).ToString() + "%] " + i.ToString() + " sprites from " + quantidade_de_sprites.ToString());
                    spr = Image.FromFile(compilar_onde.getPath() + "\\s" + i.ToString() + ".bmp");
                    compiledSprite = sc70.CompileImg(spr);
                    if (i != quantidade_de_sprites)
                    {
                        offset[i] = Conversor.UInt32_add(offset[i - 1], compiledSprite.Length);
                    }
                    compiling.Write(compiledSprite);
                }
                compiling.Flush();
                compiling.Close();
                compiling = null;

                //criando o arquivo .spr
                setLog("Concluding compilation...");
                string file_name = compilar_para_onde.generateFileName();
                compiled = new BinaryWriter(new FileStream(file_name, FileMode.Create));
                compiled.Write(readSignature);
                compiled.Write(Conversor.UInt16_to_Byte(quantidade_de_sprites));
                for (int i = 0; i < quantidade_de_sprites; i++)
                {
                    compiled.Write(Conversor.UInt32_to_Byte(offset[i]));
                }
                get_bin_file_from_virtual_memory = new BinaryReader(File.OpenRead(compilar_para_onde.getPath() + "\\virtual_memory.bin"));
                get_bin_file_from_virtual_memory.BaseStream.Position = 0;
                compiled.Write(get_bin_file_from_virtual_memory.ReadBytes(Conversor.Int64_to_Int32(get_bin_file_from_virtual_memory.BaseStream.Length)));
                
                //Encerrando
                get_bin_file_from_virtual_memory.Close();
                get_bin_file_from_virtual_memory = null;
                compiled.Flush();
                compiled.Close();
                compiled = null;
                File.Delete(compilar_para_onde.getPath() + "\\virtual_memory.bin");
                setLog("File compiled to: " + file_name);
                setLog("Compilation complete!");
            }
        }
        #endregion
        #region Compilando para a versão 9.61
        //Thread responsável pela compilação de sprites para a versão 9.6 para cima
        private void compileTo96()
        {
            //declarando variaiveis
            bool cantReadSignature = false;
            Image spr;
            UInt32 quantidade_de_sprites = 0;

            //lendo assinatura
            try
            {
                assinatura.fromFile(compilar_onde);
                readSignature = assinatura.get();
            }
            catch
            {
                cantReadSignature = true;
            }

            if (cantReadSignature == true)
            {
                setLog("Error while trying to read signature.");
                setLog("Operation aborted...");
            }

            else
            {
                //organizando as variaveis para começar a leitura
                UInt32 lastOffset; //variavel responsável por guardar a posição do ultimo offset
                setLog("Reading sprite number...");
                compiling = new BinaryWriter(new FileStream(compilar_para_onde.getPath() + "\\virtual_memory.bin", FileMode.Create));
                quantidade_de_sprites = compilar_onde.getSprites();
                setLog("[0%] 0 sprites from " + quantidade_de_sprites.ToString());
                offset = new UInt32[quantidade_de_sprites];
                offset[0] = (quantidade_de_sprites * 4) + 8;
                lastOffset = offset[0];

                //lendo as sprites aqui e criando as offsets
                for (int i = 1; i <= quantidade_de_sprites; i++)
                {
                    lastLog("[" + percent(i, Conversor.UInt32_to_Int32(quantidade_de_sprites)).ToString() + "%] " + i.ToString() + " sprites from " + quantidade_de_sprites.ToString());
                    spr = Image.FromFile(compilar_onde.getPath() + "\\s" + i.ToString() + ".bmp");
                    compiledSprite = sc96.CompileImg(spr);
                    if (compiledSprite.Length == 5 && i != 1 && i != quantidade_de_sprites)
                    {
                        offset[i - 1] = 0;
                        compiledSprite = new byte[0];
                        offset[i] = lastOffset;
                    }

                    else if (i != quantidade_de_sprites)
                    {
                        lastOffset = Conversor.UInt32_add(lastOffset, compiledSprite.Length);
                        offset[i] = lastOffset;
                    }
                    compiling.Write(compiledSprite);
                    compiling.Flush();
                }
                compiling.Close();
                compiling = null;

                //criando o arquivo .spr
                setLog("Concluding compilation...");
                string file_name = compilar_para_onde.generateFileName();
                compiled = new BinaryWriter(new FileStream(file_name, FileMode.Create));
                compiled.Write(readSignature);
                compiled.Write(Conversor.UInt32_to_Byte(quantidade_de_sprites));
                for (int i = 0; i < quantidade_de_sprites; i++)
                {
                    compiled.Write(Conversor.UInt32_to_Byte(offset[i]));
                }
                get_bin_file_from_virtual_memory = new BinaryReader(File.OpenRead(compilar_para_onde.getPath() + "\\virtual_memory.bin"));
                get_bin_file_from_virtual_memory.BaseStream.Position = 0;
                compiled.Write(get_bin_file_from_virtual_memory.ReadBytes(Conversor.Int64_to_Int32(get_bin_file_from_virtual_memory.BaseStream.Length)));
                
                //Encerrando
                get_bin_file_from_virtual_memory.Close();
                get_bin_file_from_virtual_memory = null;
                compiled.Flush();
                compiled.Close();
                compiled = null;
                File.Delete(compilar_para_onde.getPath() + "\\virtual_memory.bin");

                setLog("File compiled to: " + file_name);
                setLog("Compilation complete!");
            }
        }
        #endregion
        #endregion
        #region funções internas
        /* funções privadas */
        private double getVersion()
        {
            if (selectversion.SelectedIndex == 0)
            {
                return 9.6;
            }

            else if (selectversion.SelectedIndex == 1)
            {
                return 7.0;
            }

            else if (selectversion.SelectedIndex == 2)
            {
                return 3.0;
            }

            else
            {
                return 0;
            }
        }

        private void setLog(string log)
        {
            if (this.consolelog.InvokeRequired)
            {
                setConsoleLog scl = new setConsoleLog(setLog);
                Invoke(scl, new object[] { log });
            }
            else
            {
                this.consolelog.Text += Environment.NewLine + log;
            }
        }

        private void lastLog(string log)
        {
            if (this.consolelog.InvokeRequired)
            {
                lastConsoleLog lcl = new lastConsoleLog(lastLog);
                Invoke(lcl, new object[] { log });
            }

            else
            {

                string[] logs = Str.explode(Environment.NewLine, this.consolelog.Text);
                logs[logs.Length - 1] = log;
                this.consolelog.Text = Str.implode(Environment.NewLine, logs);
            }
        }

        private int percent(int piece, int total)
        {
            return piece / (total / 100);
        }
        #endregion
        #region botões mais bestinhas
        /* metodos da classe */
        private void About_Click(object sender, EventArgs e)
        {
            Sobre sobre = new Sobre();
            sobre.Show();
        }

        private void Donate_Click(object sender, EventArgs e)
        {
            Process.Start("javascript:alert('ops')");
        }
        #endregion
        #region botão de extração de sprites
        private void Extract_Click(object sender, EventArgs e)
        {
            if (getVersion() == 3.0)
            {
                #region lendo para a versão 3.0 até 6.5
                setLog("Can't work with this version.");
                #endregion
            }

            else
            {
                extrair_quem = new TibiaFile("Which file you want to decompile?");
                if (extrair_quem.success == true)
                {
                    extrair_para_onde = new TibiaFolder("Where do you want the folder be created?");
                    if (extrair_para_onde.success == true)
                    {
                        #region lendo para a versão 7.0 até 9.54
                        if (getVersion() == 7.0)
                        {
                            setLog("Reading sprite file.");
                            try
                            {
                                sd70 = new SprDecompiler70(extrair_quem);
                                setLog("Exporting...");
                                setLog("The logs blinking while extracting means that my program is the best...");
                                sprites_to_export = sd70.getSprites();
                                assinatura.set(sd70.getSignature());
                            }

                            catch
                            {
                                try
                                {
                                    setLog("Can't read the file...");
                                    selectversion.SelectedIndex = 1;
                                    setLog("Trying to decompile under the pattern from version 9.6");
                                    setLog("Reading sprite file.");
                                    sd96 = new SprDecompiler96(extrair_quem);
                                    setLog("Exporting...");
                                    sprites_to_export = sd96.getSprites();
                                    assinatura.set(sd96.getSignature());
                                }

                                catch
                                {
                                    setLog("Can't read the file... Operation aborted");
                                }
                            }
                        }
                        #endregion
                        #region lendo para versão 9.6
                        else if (getVersion() == 9.6)
                        {
                            setLog("Reading sprite file.");
                            //try
                           // {
                                sd96 = new SprDecompiler96(extrair_quem);
                                setLog("Exporting...");
                                sprites_to_export = sd96.getSprites();
                                assinatura.set(sd96.getSignature());
                            /*}

                            catch
                            {
                                try
                                {
                                    setLog("Can't read the file...");
                                    selectversion.SelectedIndex = 0;
                                    setLog("Trying to decompile under the pattern from version 7.2 to 9.54");
                                    setLog("Reading sprite file.");
                                    sd70 = new SprDecompiler70(extrair_quem);
                                    assinatura.set(sd70.getSignature());
                                    setLog("Exporting...");
                                    sprites_to_export = sd70.getSprites();
                                }

                                catch
                                {
                                    setLog("Can't read the file... Operation aborted");
                                }
                            */
                        }
                        #endregion
                        #region criando a pasta onde as sprites serão armazenadas e depois iniciando o thread
                        if (sprites_to_export != null)
                        {
                            string nova_pasta = "Sprites";
                            int k = 2;

                            while (Directory.Exists(extrair_para_onde.getPath() + "\\" + nova_pasta))
                            {
                                nova_pasta = "Sprites " + k.ToString(); k++;
                            }
                            export_folder_name = extrair_para_onde.getPath() + "\\" + nova_pasta;
                            Directory.CreateDirectory(export_folder_name);
                            assinatura.save(extrair_para_onde, nova_pasta);
                        }

                        this.sprites_exporter = new Thread(new ThreadStart(this.saveSprites));
                        this.sprites_exporter.Start();
                        #endregion

                    }//seleção da pasta onde as imagens vão ficar
                }//seleção do arquivo a ser extraido
            }
        }
        #endregion
        #region botão de compilação de sprites
        private void Compile_Click(object sender, EventArgs e)
        {
            if (getVersion() == 3.0)
            {
            }

            else
            {
                compilar_onde = new TibiaFolder("What folder the sprites are?");
                if (compilar_onde.success == true)
                {
                    compilar_para_onde = new TibiaFolder("Choose where you want the file be saved?");
                    if (compilar_para_onde.success == true)
                    {
                        #region compilando as versões entre 7.0 até 9.54
                        if (getVersion() == 7.0)
                        {
                            sprite_compiler70 = new Thread(compileTo70);
                            sprite_compiler70.Start();
                        }
                        #endregion
                        #region compilando da versão 9.6 para cima
                        else if (getVersion() == 9.6)
                        {
                            sprite_compiler96 = new Thread(compileTo96);
                            sprite_compiler96.Start();
                        }
                        #endregion
                    }
                }
            }
        }
        #endregion
    }
}
