using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#pragma warning disable CS0105 // Using directive appeared previously in this namespace
using System.Text;
#pragma warning restore CS0105 // Using directive appeared previously in this namespace

namespace Diccionario_de_archivos
{
    public class CArchivo
    {
        private string nombre;
        private long dir_Fisica;
        private FileStream archivo;
        private List<CEntidad> lista_Ent;

        public CArchivo()
        {
            Lista_Ent = new List<CEntidad>();
        }


        public void insertaEntidad(CEntidad ent, string nameArch)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(0, SeekOrigin.End);
                    escribe.Write(ent.Nombre);
                    escribe.Write(ent.Prt_ent_sig);
                    escribe.Write(ent.Ptr_atrib);
                    escribe.Write(ent.Ptr_datos);
                    escribe.Write(ent.Ptr_entidad);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void insertaAtributo(CAtributo AT, string nameArch)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(0, SeekOrigin.End);
                    escribe.Write(AT.Direccion);
                    escribe.Write(AT.Nombre);
                    escribe.Write(AT.Tipo);
                    escribe.Write(AT.Tamaño);
                    escribe.Write(AT.Indice);
                    escribe.Write(AT.Dir_Indice);
                    escribe.Write(AT.Sig_Atributo);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void leeEntidad(long dir_apuntador, string nameArch)
        {
            if (dir_apuntador != -1)
            {
                try
                {

                    FileStream file = new FileStream(nameArch, FileMode.Open, FileAccess.Read);
                    file.Seek(dir_apuntador, SeekOrigin.Current);
                    using (BinaryReader leer = new BinaryReader(file))
                    {
                        CEntidad entidadAux = new CEntidad();

                        entidadAux.Nombre = leer.ReadString();
                        entidadAux.Prt_ent_sig = leer.ReadInt64();
                        entidadAux.Ptr_atrib = leer.ReadInt64();
                        entidadAux.Ptr_datos = leer.ReadInt64();
                        entidadAux.Ptr_entidad= leer.ReadInt64();

                        //Console.Write("vamos a buscar atributos \n");
                        if (entidadAux.Ptr_atrib != -1)
                        {
                            Console.Write(entidadAux.Ptr_atrib + " Si encontre atributos \n");
                            file.Close();
                            leeAtributos(entidadAux.Ptr_atrib, nameArch, entidadAux);
                        }
                        Lista_Ent.Add(entidadAux);
                        leeEntidad(entidadAux.Prt_ent_sig, nameArch);


                    }
                    file.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void leeAtributos(long dir_apuntador, string nameArch, CEntidad entidadAux)
        {
            Console.Write(dir_apuntador + " Esta es la direccion del apuntador\n");
            if (dir_apuntador != -1)
            {
                try
                {

                    FileStream file = new FileStream(nameArch, FileMode.Open, FileAccess.Read);
                    Console.Write(dir_apuntador + " Esta es la direccion ya de busqueda\n");
                    file.Seek(dir_apuntador, SeekOrigin.Current);
                    using (BinaryReader leer = new BinaryReader(file))
                    {
                        CAtributo ATRI = new CAtributo();

                        /*  private string nombre;
                          private long direccion;
                          private char tipo;
                          private int tamaño;
                          private int indice;
                          private long dir_Indice;
                          private long sig_Atributo;*/

                        ATRI.Direccion = leer.ReadInt64();
                        ATRI.Nombre = leer.ReadString();
                        ATRI.Tipo = leer.ReadChar();
                        ATRI.Tamaño = leer.ReadInt32();
                        ATRI.Indice = leer.ReadInt32();
                        ATRI.Dir_Indice = leer.ReadInt64();
                        ATRI.Sig_Atributo = leer.ReadInt64();

                        entidadAux.Lista_Atrb.Add(ATRI);
                        Console.Write(ATRI.Direccion + "\n");
                        Console.Write(ATRI.Nombre + "\n");
                        Console.Write(ATRI.Sig_Atributo + "\n");
                        leeAtributos(ATRI.Sig_Atributo, nameArch, entidadAux);
                        
                    }
                    file.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void modifica_ent_sig(long apuntador, string nameArch, CEntidad ent)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(Convert.ToInt32(apuntador), SeekOrigin.Current);
                    escribe.Write(ent.Nombre);
                    escribe.Write(ent.Prt_ent_sig);
                    escribe.Write(ent.Ptr_atrib);
                    escribe.Write(ent.Ptr_datos);
                    escribe.Write(ent.Ptr_entidad);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void modifica_atri_sig(long apuntador, string nameArch, CAtributo AT)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(Convert.ToInt32(apuntador), SeekOrigin.Current);
                    escribe.Write(AT.Direccion);
                    escribe.Write(AT.Nombre);
                    escribe.Write(AT.Tipo);
                    escribe.Write(AT.Tamaño);
                    escribe.Write(AT.Indice);
                    escribe.Write(AT.Dir_Indice);
                    escribe.Write(AT.Sig_Atributo);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void modifica_Cab(long dato, string nameArch)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(0, SeekOrigin.Current);
                    escribe.Write(dato);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public long dimeTamArch(string nameArch)
        {
            archivo = new FileStream(nameArch, FileMode.Open, FileAccess.Read);
            long t = archivo.Seek(0, SeekOrigin.End);
            archivo.Close();
            return t;
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public long Dir_Fisica
        {
            get
            {
                return dir_Fisica;
            }

            set
            {
                dir_Fisica = value;
            }
        }

        public List<CEntidad> Lista_Ent
        {
            get
            {
                return lista_Ent;
            }

            set
            {
                lista_Ent = value;
            }
        }
    }
}
