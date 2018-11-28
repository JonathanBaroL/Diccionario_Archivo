using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_de_archivos
{
    public class CDiccionario
    {
        private long cab;
        private CArchivo archivo;
        private List<CEntidad> lista_Ent;
        private List<CAtributo> lista_Atrb;
        private string name;

        public CDiccionario()
        {
            lista_Ent = new List<CEntidad>();
            lista_Atrb = new List<CAtributo>();
            archivo = new CArchivo();
            Cab = -1;
        }

        public void Inserta_Entidad(CEntidad EN)
        {
            string aux = EN.Nombre;
            EN.Nombre = rellenaString(aux);
            if (Cab > -1 && Lista_Ent.Count == 0)
            {
                lista_Ent.Add(EN);
                long arch = archivo.dimeTamArch(Name);
                Cab = arch;
            }
            else
            {
                if (Cab == -1 && Lista_Ent.Count == 0)
                {
                    lista_Ent.Add(EN);
                    Cab = 8;
                    archivo.modifica_Cab(Cab, Name);
                    //Console.Write(EN.Nombre);
                    archivo.insertaEntidad(EN, Name);
                }
                else
                {
                    int index = 0;
                    int band = 0;
                    foreach (CEntidad ent in lista_Ent)
                    {
                        Int32 val = string.Compare(EN.Nombre, ent.Nombre);
                        if (val < 0)
                        {
                            if (lista_Ent.Count < 2)
                            {
                                EN.Prt_ent_sig = lista_Ent[index].Ptr_entidad;
                                Cab = EN.Ptr_entidad;
                                archivo.modifica_Cab(Cab, Name);
                                index++;
                                lista_Ent.Insert(index - 1, EN);
                                archivo.insertaEntidad(EN, Name);
                                archivo.modifica_ent_sig(EN.Ptr_entidad, Name, EN);
                                band = 1;
                                //Console.Write("Caso 1");
                            }
                            else
                            {
                                if (index < 1)
                                {
                                    Cab = EN.Ptr_entidad;
                                    EN.Prt_ent_sig = lista_Ent[0].Ptr_entidad;
                      
                                    archivo.modifica_Cab(Cab, Name);
                                    archivo.insertaEntidad(EN, Name);
                                    archivo.modifica_ent_sig(EN.Ptr_entidad, Name, EN);
                                    lista_Ent.Insert(0, EN);
                                    band = 1;
                                   // Console.Write("Caso 2");
                                }
                                else
                                {
                                    EN.Prt_ent_sig = lista_Ent[index].Ptr_entidad;
                                    lista_Ent[index - 1].Prt_ent_sig = EN.Ptr_entidad;
                                    index++;
                                    lista_Ent.Insert(index - 1, EN);
                                    archivo.insertaEntidad(EN, Name);
                                    archivo.modifica_ent_sig(EN.Ptr_entidad, Name, EN);
                                    archivo.modifica_ent_sig(lista_Ent[index - 2].Ptr_entidad, Name, lista_Ent[index - 2]);
                                    band = 1;
                                    //Console.Write("Caso 3");
                                }
                            }
                            break;
                        }
                        index++;
                    }
                    if (band == 0)
                    {
                        int count = lista_Ent.Count();
                        lista_Ent[count - 1].Prt_ent_sig = EN.Ptr_entidad;
                        lista_Ent.Add(EN);
                        archivo.insertaEntidad(EN, Name);
                        archivo.modifica_ent_sig(EN.Ptr_entidad, Name, EN);
                        archivo.modifica_ent_sig(lista_Ent[count - 1].Ptr_entidad, Name, lista_Ent[count - 1]);
                        //Console.Write("Caso 4");
                    }

                }
            }

            
        }

        public void Inserta_Atributo(CAtributo AT, CEntidad EN)
        {
            if (EN.Ptr_atrib == -1)
            {
                EN.Ptr_atrib = dimeTamArch(Name);

                archivo.modifica_ent_sig(EN.Ptr_entidad, Name, EN);
                AT.Direccion = dimeTamArch(Name);
                AT.Sig_Atributo = -1;
                EN.Lista_Atrb.Add(AT);
                archivo.insertaAtributo(AT, Name);
                //Console.Write("CASO 1 \n");
            }
            else
            {
                AT.Direccion = dimeTamArch(Name);
                AT.Sig_Atributo = -1;
                int tam = EN.Lista_Atrb.Count();
                //Console.Write(tam + " \n");
                EN.Lista_Atrb[tam - 1].Sig_Atributo = AT.Direccion;  //Modificar el atributo anterio su direccion soguiente en el archivo para que haga el link
                Archivo.modifica_atri_sig(EN.Lista_Atrb[tam - 1].Direccion, Name, EN.Lista_Atrb[tam - 1]);
                Console.Write(EN.Lista_Atrb[tam - 1].Direccion + " \n");
                EN.Lista_Atrb.Add(AT);
                archivo.insertaAtributo(AT, Name);
                //Console.Write("CASO 2 \n");
            }
        }
        
        public void Inserta_Registro2(CRegistro REG, CEntidad EN, string Name_Reg, int modifica)
        {
            string aux = EN.Nombre;
            EN.Nombre = rellenaString(aux);

            string nombre = NombreReg(Name_Reg);

            if (EN.Ptr_datos > 0 && EN.Lista_Registros.Count == 0)
            {
                EN.Lista_Registros.Add(REG);
                EN.Ptr_datos = 0;
            }
            else
            {


                if (EN.Ptr_datos == 0 && EN.Lista_Registros.Count == 0)
                {
                    //MessageBox.Show("caso 0");
                    EN.Lista_Registros.Add(REG);
                    EN.Ptr_datos = 0;
                    REG.Reg_dir = 0;
                    REG.Reg_sig = -1;
                    archivo.creaRegistro(nombre, REG, EN);
                    archivo.modifica_ent_sig(EN.Ptr_entidad, name, EN);
                }
                
                /// 
                else
                {
                    ///metodo para saber como ordenar

                    int pos = 0;

                    for(int i2 = 0; i2 < EN.Lista_Atrb.Count; i2++)
                    {
                        if (EN.Lista_Atrb[i2].Indice == 1 || EN.Lista_Atrb[i2].Indice == 2 || EN.Lista_Atrb[i2].Indice == 3)
                        {
                            pos = i2;
                            break;
                        }
                    }


                    int index = 0;
                    int band = 0;
                    foreach (CRegistro reg in EN.Lista_Registros)
                    {
                        Int32 val = 0;
                        if (EN.Lista_Atrb[pos].Tipo == 'S')
                        {
                            val = string.Compare(REG.Lista_Atributos[pos].ToString(), reg.Lista_Atributos[pos].ToString());
                        }
                        if (EN.Lista_Atrb[pos].Tipo == 'I')
                        {
                            if (Convert.ToInt32(REG.Lista_Atributos[pos]) < Convert.ToInt32(reg.Lista_Atributos[pos]))
                            {
                                val = -1;
                            }
                            if (Convert.ToInt32(REG.Lista_Atributos[pos]) > Convert.ToInt32(reg.Lista_Atributos[pos]))
                            {
                                val = 1;
                            }
                        }
                        if (val < 0)
                        {
                            if (EN.Lista_Registros.Count < 2)
                            {
                                REG.Reg_sig = EN.Lista_Registros[index].Reg_dir;


                                EN.Ptr_datos = dimeTamArch(nombre);
                                


                                foreach(CEntidad ent in lista_Ent)
                                {
                                    if(EN.Nombre == ent.Nombre)
                                    {
                                        ent.Prt_ent_sig = EN.Prt_ent_sig;
                                        break;
                                    }
                                }

                                archivo.modifica_ent_sig(EN.Ptr_entidad, name, EN);
                                index++;
                                EN.Lista_Registros.Insert(index - 1, REG);

                                if (modifica == 1)
                                {
                                    archivo.modifica_reg_sig(REG.Reg_dir, nombre, EN, REG);
                                }
                                else
                                {
                                    archivo.insertaRegistro(REG, nombre, EN);
                                }



                                archivo.modifica_reg_sig(REG.Reg_dir, nombre, EN, REG);
                                band = 1;

                                //MessageBox.Show("caso 1");
                            }
                            else
                            {
                                if (index < 1)
                                {
                                    EN.Ptr_datos = REG.Reg_dir;
                                    REG.Reg_sig = EN.Lista_Registros[0].Reg_dir;

                                    archivo.modifica_ent_sig(EN.Ptr_entidad, Name, EN);

                                    if (modifica == 1)
                                    {
                                        archivo.modifica_reg_sig(REG.Reg_dir, nombre, EN, REG);
                                    }
                                    else
                                    {
                                        archivo.insertaRegistro(REG, nombre, EN);
                                    }

                                    archivo.modifica_reg_sig(REG.Reg_dir, nombre, EN, REG);
                                    EN.Lista_Registros.Insert(0, REG);
                                    band = 1;

                                    //MessageBox.Show("caso 2");
                                }
                                else
                                {
                                    REG.Reg_sig = EN.Lista_Registros[index].Reg_dir;
                                    EN.Lista_Registros[index - 1].Reg_sig = REG.Reg_dir;
                                    index++;

                                    if (modifica == 1)
                                    {
                                        EN.Lista_Registros.Insert(index - 1, REG);
                                    }
                                    else
                                    {
                                        EN.Lista_Registros.Insert(index - 1, REG);
                                        archivo.insertaRegistro(REG, nombre, EN);
                                    }



                                    archivo.modifica_reg_sig(REG.Reg_dir, nombre, EN, REG);
                                    archivo.modifica_reg_sig(EN.Lista_Registros[index - 2].Reg_dir, nombre, EN, EN.Lista_Registros[index - 2]);
                                    band = 1;

                                    //MessageBox.Show("caso 3");
                                }
                            }
                            break;
                        }
                        index++;
                    }
                    if (band == 0)
                    {
                        int count = EN.Lista_Registros.Count();

                        EN.Lista_Registros[count - 1].Reg_sig = REG.Reg_dir;
                        EN.Lista_Registros.Add(REG);

                        if (modifica == 1)
                        {
                            archivo.modifica_reg_sig(REG.Reg_dir, nombre, EN, REG);
                        }
                        else
                        {
                            archivo.insertaRegistro(REG, nombre, EN);
                            archivo.modifica_reg_sig(REG.Reg_dir, nombre, EN, REG);
                            archivo.modifica_reg_sig(EN.Lista_Registros[count - 1].Reg_dir, nombre, EN, EN.Lista_Registros[count - 1]);
                        }

                        //MessageBox.Show("caso 4");
                    }

                }
            }
        }

        public void Inserta_Indice(CEntidad ENT)
        {
            if (ENT.Lista_Registros.Count == 0)
            {
                string nombre = NombreIdx(ENT.Nombre);
                //MessageBox.Show("Nombre del Archivo .IDX  =  " + nombre);
                archivo.creaIdx(nombre);

                foreach (CAtributo atr in ENT.Lista_Atrb)
                {
                    if (atr.Indice == 2)
                    {
                        atr.Dir_Indice = dimeTamArch(nombre);
                        archivo.modifica_atri_sig(atr.Direccion, name, atr);

                        if (atr.Tipo == 'S')
                        {
                            char[] abc = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                            for (int i = 0; i < abc.Length; i++)
                            {
                                CIndexP ind = new CIndexP();
                                ind.DirIndice = dimeTamArch(nombre);
                                ind.Indice = abc[i].ToString();
                                long dat = -1;
                                ind.DirRegistros = dat;
                                ENT.Lista_Indices.Add(ind);
                                archivo.agregaidxPp(nombre, ind, abc[i]);
                            }
                        }
                        break;
                    }
                }
                foreach (CAtributo atr in ENT.Lista_Atrb)
                {
                    if (atr.Indice == 3)
                    {
                        atr.Dir_Indice = dimeTamArch(nombre);
                        archivo.modifica_atri_sig(atr.Direccion, name, atr);

                        for (int i = 0; i < 50; i++)
                        {
                            CIndexP ind = new CIndexP();
                            ind.DirIndice = dimeTamArch(nombre);
                            string cadena = stringVacio(atr.Tamaño);
                            string nom = cadena;
                            ind.Indice = nom;
                            long dat = -1;
                            ind.DirRegistros = dat;
                            ENT.Lista_Indices.Add(ind);
                            archivo.agregaidxPB(nombre, ind, atr.Tamaño);
                        }
                        break;
                    }
                }
            }
        }

        public void InsertaRegOnIdx(CEntidad ENT, CRegistro REG)
        {
            int pos = 0;
            string nombreIdx = NombreIdx(ENT.Nombre);
            for (int j = 0; j < ENT.Lista_Atrb.Count(); j++)
            {
                if (ENT.Lista_Atrb[j].Indice == 2)
                {
                    pos = j;
                    break;
                }
            }
            for (int i = 0; i < 27; i++)
            {
                string registro = REG.Lista_Atributos[pos].ToString();
                string mayuscula = registro[0].ToString().ToUpper();
                if (mayuscula == ENT.Lista_Indices[i].Indice)
                {
                    //MessageBox.Show(ENT.Lista_Indices[i].Indice.ToString() + "  " + ENT.Lista_Indices[i].DirRegistros.ToString());
                    if (ENT.Lista_Indices[i].DirRegistros == -1)
                    {
                        for(int j = 0; j < 100; j++)
                        {
                            CIndexP indice = new CIndexP();
                            long tam = dimeTamArch(nombreIdx);
                            indice.DirIndice = tam;
                            if(j == 0)
                            {
                                ENT.Lista_Indices[i].DirRegistros = tam;
                                char abc = ENT.Lista_Indices[i].Indice[0];
                                Archivo.modifica_IndiceP(ENT.Lista_Indices[i].DirIndice, nombreIdx, ENT.Lista_Indices[i], abc);
                            }
                            string cadena = stringVacio(ENT.Lista_Atrb[pos].Tamaño);
                            indice.Indice = cadena;
                            tam = -1;
                            indice.DirRegistros = tam;
                            ENT.Lista_Indices[i].Lista_IndexP.Add(indice);
                            Archivo.agregaidxPB(nombreIdx, indice, ENT.Lista_Atrb[pos].Tamaño);
                        }
                        //MessageBox.Show("Se creo correctamente el bloque del indice " + ENT.Lista_Indices[i].Indice);
                        InsertaRegOnIdx(ENT, REG);
                    }
                    else
                    {
                        //MessageBox.Show("Ya existe el bloque, ahora se agregara este elemento en el");
                        foreach(CIndexP indP in ENT.Lista_Indices[i].Lista_IndexP)
                        {
                            if (indP.Indice[0].ToString() == " ")
                            {
                                indP.Indice = registro;
                                indP.DirRegistros = REG.Reg_dir;
                                archivo.modifica_Indice(indP.DirIndice, nombreIdx, indP, ENT.Lista_Atrb[pos].Tamaño);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }

        public void InsertaRegOnIdxS(CEntidad ENT, CRegistro REG)
        {
            int pos = 0;
            string nombreIdx = NombreIdx(ENT.Nombre);
            for (int j = 0; j < ENT.Lista_Atrb.Count(); j++)
            {
                if (ENT.Lista_Atrb[j].Indice == 3)
                {
                    pos = j;
                    break;
                }
            }
            for (int i = 0; i < 50; i++)
            {
                string registro = REG.Lista_Atributos[pos].ToString();
                string compara = "";
                for(int k = 0; k < ENT.Lista_Atrb[pos].Tamaño - 1; k++)
                {
                    compara += ENT.Lista_Indices[i + 27].Indice[k];
                }
                if (registro == compara)
                {
                    //MessageBox.Show("Ya existe el bloque" + "   Tamaño del bloque = " + ENT.Lista_Indices[i + 27].Lista_IndexP.Count());
                    foreach(CIndexP idxS in ENT.Lista_Indices[i + 27].Lista_IndexP)
                    {
                        if (idxS.DirRegistros == -1)
                        {
                            //MessageBox.Show("Direcion del bloque " + ENT.Lista_Indices[i + 27].DirRegistros.ToString());
                            idxS.DirRegistros = REG.Reg_dir;
                            archivo.modifica_IndiceS(idxS.DirIndice, nombreIdx, idxS);
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    if (ENT.Lista_Indices[i + 27].Indice[0].ToString() == " ")
                    {
                        ENT.Lista_Indices[i + 27].Indice = registro;
                        ENT.Lista_Indices[i + 27].DirRegistros = dimeTamArch(nombreIdx);
                        archivo.modifica_Indice(ENT.Lista_Indices[i + 27].DirIndice, nombreIdx, ENT.Lista_Indices[i + 27], ENT.Lista_Atrb[pos].Tamaño);
                        for(int j = 0; j < 50; j++)
                        {
                            CIndexP indice = new CIndexP();
                            indice.DirIndice = dimeTamArch(nombreIdx);
                            indice.DirRegistros = -1;
                            ENT.Lista_Indices[i + 27].Lista_IndexP.Add(indice);
                            archivo.agregaidxS(nombreIdx, indice);
                        }
                        InsertaRegOnIdxS(ENT, REG);
                        break;
                    }
                }
            }
        }

        public void Elimina_Entidad(CEntidad ent_remove)
        {
            foreach(CEntidad ent in Lista_Ent)
            {
                if (cab == ent_remove.Ptr_entidad)
                {
                    Lista_Ent.Remove(ent_remove);
                    if (Lista_Ent.Count == 0)
                    {
                        long tamañoArch = archivo.dimeTamArch(Name);
                        if(tamañoArch > 0)
                        {
                            Cab = tamañoArch;
                            archivo.modifica_Cab(Cab, Name);
                        }
                        else
                        {
                            Cab = -1;
                            archivo.modifica_Cab(Cab, Name);
                        }
                    }
                    else
                    {
                        Cab = lista_Ent[0].Ptr_entidad;
                        archivo.modifica_Cab(Cab, Name);
                    }
                    break;
                    
                }
                if (ent.Prt_ent_sig == ent_remove.Ptr_entidad)
                {
                    ent.Prt_ent_sig = ent_remove.Prt_ent_sig;
                    archivo.modifica_ent_sig(ent.Ptr_entidad, Name, ent);
                    Lista_Ent.Remove(ent_remove);
                    break;
                }
            }
        }

        public void Elimina_Atributo(CAtributo atri_remove, CEntidad Ent)
        {
            if (Ent.Ptr_atrib == atri_remove.Direccion)
            {
                Ent.Ptr_atrib = atri_remove.Sig_Atributo;
                archivo.modifica_ent_sig(Ent.Ptr_entidad, Name, Ent);
                Ent.Lista_Atrb.Remove(atri_remove);
                Console.Write("Exito caso 1");
            }
            else
            {
                int position = Ent.Lista_Atrb.IndexOf(atri_remove);
                Ent.Lista_Atrb[position - 1].Sig_Atributo = atri_remove.Sig_Atributo;
                archivo.modifica_atri_sig(Ent.Lista_Atrb[position - 1].Direccion, Name, Ent.Lista_Atrb[position - 1]);
                Ent.Lista_Atrb.Remove(atri_remove);
                Console.Write("Exito caso 2");
            }
        }

        public void Elimina_Registro(CRegistro reg, CEntidad Ent)
        {
            string nombre = NombreReg(Ent.Nombre);
            Console.Write("Registro a eliminar... " + nombre + "\n");

             if (Ent.Ptr_datos == reg.Reg_dir)
             {
                Ent.Ptr_datos = reg.Reg_sig;
                archivo.modifica_ent_sig(Ent.Ptr_entidad, name, Ent);

                foreach(CRegistro auxreg in Ent.Lista_Registros)
                {
                    if (auxreg.Reg_dir == reg.Reg_dir)
                    {
                        Ent.Lista_Registros.Remove(auxreg);
                        break;
                    }
                }


                /// QUE PASA!!!

                Console.Write("Exito caso 1\n");

            }
            else
             {
                foreach (CRegistro auxreg in Ent.Lista_Registros)
                {
                    if (auxreg.Reg_dir == reg.Reg_dir)
                    {
                        int position = Ent.Lista_Registros.IndexOf(auxreg);
                        Ent.Lista_Registros[position - 1].Reg_sig = auxreg.Reg_sig;
                        archivo.modifica_reg_sig(Ent.Lista_Registros[position - 1].Reg_dir, nombre, Ent, Ent.Lista_Registros[position - 1]);
                        Ent.Lista_Registros.Remove(auxreg);
                        Console.Write("Exito caso 2\n");
                        break;
                    }
                }

            }

        }

        public void Modifica_Entidad(CEntidad ent_modificar, string modicacion)
        {
            CEntidad newE = new CEntidad();
            //newE.Nombre = modicacion;
            //Console.Write(modicacion);

            foreach (CEntidad ent in Lista_Ent)
            {
                if (ent_modificar.Nombre == ent.Nombre)
                {
                    newE = ent;
                    newE.Nombre = modicacion;
                    Elimina_Entidad(ent);
                    break;
                }
            }

            Inserta_Entidad(newE);
        }

        public void Modifica_Atributo(CEntidad Ent, CAtributo ATRI, CAtributo ATRI_MOD)
        {
            foreach(CAtributo Atr in Ent.Lista_Atrb)
            {
                if (Atr.Nombre == ATRI_MOD.Nombre)
                {
                    Atr.Nombre = ATRI.Nombre;
                    Atr.Tipo = ATRI.Tipo;
                    Atr.Tamaño = ATRI.Tamaño;
                    Atr.Indice = ATRI.Indice;
                    Archivo.modifica_atri_sig(Atr.Direccion, Name, Atr);
                    break;
                }
            }
        }

        public void Modifica_Registro(CEntidad Ent, CRegistro Reg, CRegistro Reg_mod)
        {
            /*foreach (CAtributo Atr in Ent.Lista_Atrb)
            {
                if (Atr.Nombre == ATRI_MOD.Nombre)
                {
                    Atr.Nombre = ATRI.Nombre;
                    Atr.Tipo = ATRI.Tipo;
                    Atr.Tamaño = ATRI.Tamaño;
                    Atr.Indice = ATRI.Indice;
                    Archivo.modifica_atri_sig(Atr.Direccion, Name, Atr);
                    break;
                }
            }

            int pos = Ent.Lista_Registros.IndexOf(Reg);

            Ent.Lista_Registros[pos].Reg_dir = Reg_mod.Reg_dir;
            foreach(CAtributo ATR in Ent.Lista_Atrb)
            {
                Reg.Lista_Atributos[i] = Reg
            }*/

        }

        public void Consulta_Registros(CEntidad EN)
        {
            string nombre = NombreReg(EN.Nombre);
            archivo.leeRegistros(EN.Ptr_datos, nombre, EN);
        }

        public long dimeTamArch(string NameA)
        {
            long dato = archivo.dimeTamArch(NameA);
            return dato;
        }

        public string rellenaString(string name)
        {
            int tamString = name.Length;
            for(int i = tamString; i < 30; i++)
            {
                name += " ";
            }

            //Console.Write(tamString + "\n");
           // Console.Write(name + "\n");
            return name;
        }

        public string rellenaStringTAM(string name, int tam)
        {
            int tamString = name.Length;
            for (int i = tamString; i < tam-1; i++)
            {
                name += " ";
            }
            //Console.Write(tamString + "\n");
            // Console.Write(name + "\n");
            return name;
        }

        public string stringVacio(int tam)
        {
            string cadena = "";
            for(int i = 0; i < tam; i++)
            {
                cadena += " ";
            }
            return cadena;
        }

        public void leeEntidad(long dic)
        {
            archivo.leeEntidad(dic, Name);
            Lista_Ent = archivo.Lista_Ent;
        }

        public string NombreReg(string name)
        {
            string Name_Reg = name;
            string nombre = " ";
            int k = 0;
            while (Name_Reg[k] != ' ')
            {
                nombre += Name_Reg[k];
                Console.Write(Name_Reg[k] + "\n");
                k++;
            }
            nombre += ".dat";
            return nombre;
        }

        public string NombreIdx(string name)
        {
            string Name_Reg = name;
            string nombre = " ";
            int k = 0;
            while (Name_Reg[k] != ' ')
            {
                nombre += Name_Reg[k];
                Console.Write(Name_Reg[k] + "\n");
                k++;
            }
            nombre += ".idx";
            return nombre;
        }

        internal CArchivo Archivo
        {
            get
            {
                return archivo;
            }

            set
            {
                archivo = value;
            }
        }

        internal List<CEntidad> Lista_Ent
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

        internal List<CAtributo> Lista_Atrb
        {
            get
            {
                return lista_Atrb;
            }

            set
            {
                lista_Atrb = value;
            }
        }

        public long Cab
        {
            get
            {
                return cab;
            }

            set
            {
                cab = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
