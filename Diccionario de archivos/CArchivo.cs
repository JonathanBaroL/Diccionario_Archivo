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

        public void insertaRegistro(CRegistro REG, string nameArch, CEntidad ENTIDAD)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(0, SeekOrigin.End);


                    escribe.Write(REG.Reg_dir);
                    int i = 0;
                    foreach(CAtributo ATR in ENTIDAD.Lista_Atrb)
                    {
                        switch (ATR.Tipo)
                        {
                            case 'I'://INT
                                int datoI = Convert.ToInt32(REG.Lista_Atributos[i]);
                                escribe.Write(datoI);
                                i++;
                                break;
                            case 'S'://String
                                string datoS = REG.Lista_Atributos[i].ToString();
                                escribe.Write(datoS);
                                i++;
                                break;
                        }
                    }
                    escribe.Write(REG.Reg_sig);
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
            //Console.Write(dir_apuntador + " Esta es la direccion del apuntador\n");
            if (dir_apuntador != -1)
            {
                try 
                {

                    FileStream file = new FileStream(nameArch, FileMode.Open, FileAccess.Read);
                    //Console.Write(dir_apuntador + " Esta es la direccion ya de busqueda\n");
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
                        //Console.Write(ATRI.Direccion + "\n");
                        //Console.Write(ATRI.Nombre + "\n");
                        //Console.Write(ATRI.Sig_Atributo + "\n");
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

        public void leeRegistros(long dir_apuntador, string nameArch, CEntidad ENTIDAD)
        {
            Console.Write(dir_apuntador + " Esta es la direccion del apuntador\n");
            if (dir_apuntador != -1)
            {
                try
                {

                    FileStream file = new FileStream(nameArch, FileMode.Open, FileAccess.Read);
                    //Console.Write(dir_apuntador + " Esta es la direccion ya de busqueda\n");
                    file.Seek(dir_apuntador, SeekOrigin.Current);
                    using (BinaryReader leer = new BinaryReader(file))
                    {

                        CRegistro REG = new CRegistro();

                        REG.Reg_dir = leer.ReadInt64();
                        foreach (CAtributo ATR in ENTIDAD.Lista_Atrb)
                        {
                            switch (ATR.Tipo)
                            {
                                case 'I'://INT
                                    int datoI = leer.ReadInt32();
                                    REG.Lista_Atributos.Add(datoI);
                                    break;
                                case 'S'://String
                                    string datoS = leer.ReadString(); ;
                                    REG.Lista_Atributos.Add(datoS);
                                    break;
                            }
                        }
                        REG.Reg_sig = leer.ReadInt64();



                        ENTIDAD.Lista_Registros.Add(REG);

                        leeRegistros(REG.Reg_sig, nameArch, ENTIDAD);

                    }
                    file.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show();
                }
            }
        }

        public void leeIndices(CAtributo atr, string nameArch, CEntidad ENTIDAD)
        {
            try
            {
                FileStream file = new FileStream(nameArch, FileMode.Open, FileAccess.Read);
                //MessageBox.Show("Direccion del indice " + atr.Dir_Indice.ToString() + " " + nameArch);
                file.Seek(atr.Dir_Indice, SeekOrigin.Current);
                using (BinaryReader leer = new BinaryReader(file))
                {
                    //MessageBox.Show("indice  " + atr.Indice.ToString() + " numero de inices   " + ENTIDAD.Lista_Indices.Count.ToString());

                    if (atr.Indice == 2)
                    {
                        if (atr.Tipo == 'S')
                        {
                            for (int i = 0; i < 27; i++)
                            {
                                CIndexP ind = new CIndexP();
                                long dat = leer.ReadInt64();
                                ind.DirIndice = dat;
                                char datoS = leer.ReadChar();
                                ind.Indice = datoS.ToString();
                                long dat1 = leer.ReadInt64();
                                ind.DirRegistros = dat1;
                                ENTIDAD.Lista_Indices.Add(ind);
                            }
                        }
                        if (atr.Tipo == 'I')
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                CIndexP ind = new CIndexP();
                                long dat = leer.ReadInt64();
                                ind.DirIndice = dat;
                                char datoS = leer.ReadChar();
                                ind.Indice = datoS.ToString();
                                dat = leer.ReadInt64();
                                ind.DirRegistros = dat;
                                ENTIDAD.Lista_Indices.Add(ind);
                            }
                        }
                    }
                    if (atr.Indice == 3)
                    {
                        for (int i = 0; i < 50; i++)
                        {
                            CIndexP ind = new CIndexP();
                            long dat = leer.ReadInt64();
                            ind.DirIndice = dat;
                            string datoS = leer.ReadString();
                            ind.Indice = datoS;
                            dat = leer.ReadInt64();
                            ind.DirRegistros = dat;
                            ENTIDAD.Lista_Indices.Add(ind);
                            //MessageBox.Show("numero de indices   " + ENTIDAD.Lista_Indices.Count.ToString() + " nombre del indice " + ind.Indice.ToString());
                        }
                    }
                }
                file.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        public void leeBloqueP(CEntidad ENTIDAD, string nameArch, int Tam)
        {
            foreach(CIndexP idxP in ENTIDAD.Lista_Indices)
            {
                if(idxP.DirRegistros != -1)
                {
                    try
                    {
                        FileStream file = new FileStream(nameArch, FileMode.Open, FileAccess.Read);
                        file.Seek(idxP.DirRegistros, SeekOrigin.Current);
                        using (BinaryReader leer = new BinaryReader(file))
                        {
                            for(int i = 0; i < 100; i++)
                            {
                                CIndexP ind = new CIndexP();
                                long dat = leer.ReadInt64();
                                ind.DirIndice = dat;
                                string datoS = "";
                                for (int j = 0; j < 30; j++)
                                {
                                    datoS += leer.ReadChar();
                                }
                               /* string cadena = "";
                                for(int k = 1; k < Tam; k++)
                                {
                                    cadena += datoS[k];
                                }*/
                                ind.Indice = datoS;
                                long dat1 = leer.ReadInt64();
                                ind.DirRegistros = dat1;
                                idxP.Lista_IndexP.Add(ind);
                            }
                        }
                        file.Close();
                    }
                    catch
                    {}
                }
            }
        }

        public void modifica_reg_sig(long apuntador, string nameArch, CEntidad ENTIDAD, CRegistro REG)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(Convert.ToInt32(apuntador), SeekOrigin.Current);
                    escribe.Write(REG.Reg_dir);
                    int i = 0;
                    foreach (CAtributo ATR in ENTIDAD.Lista_Atrb)
                    {
                        switch (ATR.Tipo)
                        {
                            case 'I'://INT
                                int datoI = Convert.ToInt32(REG.Lista_Atributos[i]);
                                escribe.Write(datoI);
                                i++;
                                break;
                            case 'S'://String
                                string datoS = REG.Lista_Atributos[i].ToString();
                                escribe.Write(datoS);
                                i++;
                                break;
                        }
                    }
                    escribe.Write(REG.Reg_sig);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public void modifica_Indice(long apuntador, string nameArch, CIndexP indice)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(Convert.ToInt32(apuntador), SeekOrigin.Current);
                    escribe.Write(indice.DirIndice);
                    escribe.Write(indice.Indice);
                    escribe.Write(indice.DirRegistros);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void modifica_IndiceP(long apuntador, string nameArch, CIndexP indice, char abc)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(Convert.ToInt32(apuntador), SeekOrigin.Current);
                    escribe.Write(indice.DirIndice);
                    escribe.Write(abc);
                    escribe.Write(indice.DirRegistros);
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

        public void creaRegistro(string nameArch, CRegistro REG, CEntidad ENTIDAD)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.OpenOrCreate)))
                {
                    escribe.Seek(0, SeekOrigin.Begin);
                    escribe.Write(REG.Reg_dir);
                    int i = 0;
                    foreach (CAtributo ATR in ENTIDAD.Lista_Atrb)
                    {
                        switch (ATR.Tipo)
                        {
                            case 'I'://INT
                                int datoI = Convert.ToInt32(REG.Lista_Atributos[i]);
                                escribe.Write(datoI);
                                i++;
                                break;
                            case 'S'://String
                                string datoS = REG.Lista_Atributos[i].ToString();
                                escribe.Write(datoS);
                                i++;
                                break;
                        }
                    }
                    escribe.Write(REG.Reg_sig);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void creaIdx(string nameArch)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.OpenOrCreate)))
                {
                    escribe.Seek(0, SeekOrigin.Begin);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void agregaidxP(string nameArch, CIndexP indice)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(0, SeekOrigin.End);
                    escribe.Write(indice.DirIndice);
                    escribe.Write(indice.Indice);
                    escribe.Write(indice.DirRegistros);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public void agregaidxPB(string nameArch, CIndexP indice, int Tam)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(0, SeekOrigin.End);
                    escribe.Write(indice.DirIndice);
                    for (int i = 0; i < 30; i++)
                    {
                        escribe.Write(indice.Indice[i]);
                    }
                    escribe.Write(indice.DirRegistros);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public void agregaidxPp(string nameArch, CIndexP indice, char abc)
        {
            try
            {
                using (BinaryWriter escribe = new BinaryWriter(new FileStream(nameArch, FileMode.Open)))
                {
                    escribe.Seek(0, SeekOrigin.End);
                    escribe.Write(indice.DirIndice);
                    escribe.Write(abc);
                    escribe.Write(indice.DirRegistros);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
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