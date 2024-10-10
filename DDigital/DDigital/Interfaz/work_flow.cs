using DDigital.Utilidades;
using DPUruNet;
using ProyectoDIGITALPERSONA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDigital.Interfaz
{
    public class work_flow
    {
        //SISC
        QUERIES sql;
        UTILIDADES UT;
        public INFO_SISTEMA INFORMACION_SYSTEM;
        public bool registro_huella(HUELLA _HUELLA)
        {
            ODBC_CONN bd = new ODBC_CONN();
            sql = new QUERIES();
            UT = new UTILIDADES();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object> {
                    {"@HUE_CODIGO",_HUELLA._HUE_CODIGO },
                    {"@HUE_IDENTIDAD", _HUELLA._HUE_IDENTIDAD },
                    {"@HUE_TIPO_PER", _HUELLA._HUE_TIPO_PER },

                    {"@HUELLA", _HUELLA._HUELLA},
                    {"@HUE_COMPANIA", _HUELLA._HUE_COMPANIA},
                    {"@HUE_OBSERVACION", _HUELLA._HUE_OBSERVACION},
                    {"@DEDO", _HUELLA._DEDO},
                    {"@HUELLA_SAMPLE", _HUELLA._HUELLA_SAMPLE},
                    {"@USR_AGREGO", _HUELLA._USR_AGREGO}

                };
            
                int rowAffected = bd.EjecutarParametrizada(sql.insert_in_figgers, parametros);
                if (rowAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw ;
            }
           

        }
       
        public bool verificacion_huella(Fmd fmd1, out HUELLA huella_)
        {
            UT = new UTILIDADES();
            bool verifica = false;
            ODBC_CONN bd = new ODBC_CONN();
            sql = new QUERIES();
            HUELLA dh = new HUELLA();
            DataTable resultado = bd.EjecutarConsultaSelect(sql.select_toda_huella); //trae todas las huellas
            using (DataTableReader reader = resultado.CreateDataReader())
            {
                while (reader.Read())
                {
                    MemoryStream MEMORIA = new MemoryStream((byte[])reader["HUELLA"]);
                   
                    string serial_huella = Encoding.UTF8.GetString(MEMORIA.ToArray());
                    Fmd fmd2 = Fmd.DeserializeXml(serial_huella); //grupo de fingers                     
                    CompareResult comparativa = Comparison.Compare(fmd1, 0, fmd2, 0);
                    if (comparativa.ResultCode != Constants.ResultCode.DP_SUCCESS)
                    {
                        verifica = false;
                    }
                    if (Convert.ToDouble(comparativa.Score.ToString()) == 0)
                    {

                     
                        string identidad = reader["HUE_IDENTIDAD"].ToString();
                        string CODIGO = reader["HUE_CODIGO"].ToString();
                        string tipo_per = reader["HUE_TIPO_PER"].ToString();

                        string dedo = reader["DEDO"].ToString();
                        string OBSERVACION = reader["HUE_OBSERVACION"].ToString();
                        string FECHA = reader["FECHA_CREACION"].ToString();
                        MemoryStream M_MEMORIA = new MemoryStream((byte[])reader["HUELLA_SAMPLE"]);
                        string hue_id = reader["HUE_ID"].ToString();


                        string usr = reader["USR_AGREGO"].ToString();

                        //data general persona
                       
                        //data de su huella
                        dh._HUE_TIPO_PER = tipo_per;
                        dh._HUE_CODIGO = CODIGO;
                        dh._HUE_IDENTIDAD = identidad;
                        dh._DEDO = UT.identificaDedo(dedo);
                        dh._HUE_OBSERVACION = OBSERVACION;
                        dh._FECHA_CREACION = DateTime.Parse(FECHA);
                        dh._HUE_ID = hue_id;
                        dh._USR_AGREGO = usr;
                        dh._HUELLA_SAMPLE = M_MEMORIA.ToArray();               
              
                        verifica = true;
                        break;
                    }
                }
            }
            huella_ = dh;
            return verifica;
        }

        public DATA_PERSONA InformacionVerificacion(string IDENTIDAD, int tipo)
        {
            DATA_PERSONA dp = new DATA_PERSONA();
            HUELLA dh = new HUELLA();
            ODBC_CONN con = new ODBC_CONN();
            sql = new QUERIES();
            UT = new UTILIDADES();

            try
            {
              
                Dictionary<string, object> parametros = new Dictionary<string, object> {
                            {"@identidad",IDENTIDAD },
                            {"@_tipo",  tipo}
                        };
                DataTable data_gen = con.EjecutarConsultaSelect(sql.SELECT_SP_BUSQUEDA, parametros);

                if (data_gen.Rows.Count > 0)
                {
                    using (DataTableReader reader = data_gen.CreateDataReader())
                    {
                        while (reader.Read())
                        {
                            if (tipo == 1) //SOLO DATA PERSONA
                            {
                                string nombre = reader["NOMBRE"].ToString();
                                string identidad = reader["IDENTIDAD"].ToString();
                                string CODIGO = reader["CODIGO"].ToString();
                                string tipo_per = reader["TIPO"].ToString();
                                //data general persona
                                dp.NOMBRE = nombre;
                                dp.TIPO = tipo_per;
                                dp.CODIGO = CODIGO;
                                dp.IDENTIDAD = identidad;
                                dp.ESTADO= reader["ESTADO"].ToString();
                            }
                            //else
                            //{

                            //    string nombre = reader["NOMBRE"].ToString();
                            //    string identidad = reader["IDENTIDAD"].ToString();
                            //    string CODIGO = reader["CODIGO"].ToString();
                            //    string tipo_per = reader["TIPO"].ToString();

                            //    string dedo = reader["DEDO"].ToString();
                            //    string OBSERVACION = reader["HUE_OBSERVACION"].ToString();
                            //    string FECHA = reader["FECHA_CREACION"].ToString();
                            //    MemoryStream MEMORIA = new MemoryStream((byte[])reader["HUELLA_SAMPLE"]);
                            //    string hue_id = reader["HUE_ID"].ToString();


                            //    string usr = reader["USR_AGREGO"].ToString();

                            //    //data general persona
                            //    dp.NOMBRE = nombre;
                            //    dp.TIPO = tipo_per;
                            //    dp.CODIGO = CODIGO;
                            //    dp.IDENTIDAD = identidad;
                            //    //data de su huella
                            //    dh._DEDO = UT.identificaDedo(dedo);
                            //    dh._HUE_OBSERVACION = OBSERVACION;
                            //    dh._FECHA_CREACION = DateTime.Parse(FECHA);
                            //    dh._HUE_ID = hue_id;
                            //    dh._USR_AGREGO = usr;
                            //    dh._HUELLA_SAMPLE = MEMORIA.ToArray();
                            //}

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return dp;
        }

        public DataTable Deshabilitar_Radios(string identidad)
        {
            ODBC_CONN bd = new ODBC_CONN();
            sql = new QUERIES();

            Dictionary<string, object> parametros = new Dictionary<string, object> {
                    {"@IDENTIDAD",identidad }
                };
            //string IDENTIDAD_ = "";
            DataTable resultado = bd.EjecutarConsultaSelect(sql.DEDOS_REGISTRADOS, parametros); //trae todas las huellas
            return resultado;
        }
        public List<string> Deshabilitar_Checks()
        {
          
            ODBC_CONN bd = new ODBC_CONN();
            sql = new QUERIES();
            List<string> Lcheks = new List<string>();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object> {
                {"@accion",2 },
                {"@PA_VALOR","" }
                };
                string valor_P_X_E = bd.primeraCol(sql.sp_selccion_mano, parametros);
                string[] split = valor_P_X_E.Split(';');
                string dedos = split[1];
                string[] arrayDedos = dedos.Split(':');
                arrayDedos = arrayDedos[1].Split(',');


                split = split[0].Split(':');
                Lcheks.Add(split[1]);
                foreach (var item in arrayDedos)
                {
                    Lcheks.Add(item);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return Lcheks;

        }
        public bool actualiza_mano(string pa_valor)
        {
            ODBC_CONN bd = new ODBC_CONN();
            sql = new QUERIES();
            UT = new UTILIDADES();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object> {
                       {"@accion", 1},
                       {"@PA_VALOR",pa_valor }
                };

                int rowAffected = bd.CountSelect(sql.sp_selccion_mano, parametros);
                if (rowAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable Lsitado_huellas (string identidad) {
            ODBC_CONN con1 = new ODBC_CONN();
            sql = new QUERIES();
            Dictionary<string, object> ListParam = new Dictionary<string, object>()
                {
                    {"@IDENTIDAD", identidad },                  
                    { "@_TIPO", 2 }
                };
            DataTable dt = new DataTable();
            dt = con1.EjecutarConsultaSelect(sql.SELECT_SP_BUSQUEDA, ListParam);
            return dt;
        }

        public int EliminarHuella(HISTO_HUELLAS itemBorra)
        {
            Security secu = new Security();
            INFORMACION_SYSTEM = secu.trae_info_sistema();
            ODBC_CONN db = new ODBC_CONN();
            sql = new QUERIES();
            
            itemBorra.HistoFechaAccion = DateTime.Now;
            itemBorra.HistoInfoAdicional = "ip:" + INFORMACION_SYSTEM.IP_MAQUINA + " userpc:" + INFORMACION_SYSTEM.USUARIO_MAQUINA + " namepc:" + INFORMACION_SYSTEM.MACHINE_NAME + " SOpc:" + INFORMACION_SYSTEM.VERION_OS_MAQUINA;
            itemBorra.HistoVActual = "(NO_DATA)";
            itemBorra.HistoVAnterior = "(NO_DATA)";
            itemBorra.HistoAccion = "D";
            itemBorra.HistoIdentificador = itemBorra.HistoId;
           // itemBorra.HistoCampo = "HUELLA";
            Dictionary<string, object> DelPar = new Dictionary<string, object>()
            {
                {"@id",itemBorra.HistoId}
            };
            Dictionary<string, object> historico = new Dictionary<string, object>()
            {
                
                {"@HISTO_TABLA",itemBorra.HistoTabla},
                {"@HISTO_IDENTIFICADO",itemBorra.HistoIdentificador},
                {"@HISTO_CAMPO",itemBorra.HistoCampo},              
                {"@HISTO_VANTERIOR",itemBorra.HistoVAnterior},
                {"@HISTO_VACTUAL",itemBorra.HistoVActual},
                {"@HISTO_ACCION",itemBorra.HistoAccion},
                {"@HISTO_USR_ACCION",itemBorra.HistoUsrAccion},      
                {"@HISTO_INFO_ADICIONAL",itemBorra.HistoInfoAdicional},
                {"@HISTO_OBSERVACION",itemBorra.HistoObservacion},
                //{"@HISTO_INFO_ADIC","ip:"+INFORMACION_SYSTEM.IP_MAQUINA+" userpc:"+INFORMACION_SYSTEM.USUARIO_MAQUINA+" namepc:"+INFORMACION_SYSTEM.MACHINE_NAME+" SOpc:"+INFORMACION_SYSTEM.VERION_OS_MAQUINA}
                //esto puede cambiar 
            };       
            int borrado = db.EjecutarParametrizada(sql.EliminarHuella, DelPar);
            if (borrado > 0)
            {
                ODBC_CONN db1 = new ODBC_CONN();
                db1.EjecutarParametrizada(sql.InsertarHistorialHuella, historico);         
            }



            return borrado;


        }

        public int Verifica_firmas(HUELLA huella, CREDENCIALES _CRED, int tipo_veri)
        {
            ODBC_CONN CN;
            int estado =0;
            sql = new QUERIES();
            Dictionary<string, object> parametros_ = new Dictionary<string, object>()
            {
                  {"_TIPO", tipo_veri},
                  {"CUENTA_AHO", _CRED.cta},
                  {"IDENTIDAD", huella._HUE_IDENTIDAD},
                  {"COOP_CODIGO", huella._HUE_CODIGO},
                  {"COMPAÑIA", _CRED.cia},
                  {"FILIAL", _CRED.fil}
            };
            if (huella._HUE_IDENTIDAD != null)
            {
                CN = new ODBC_CONN();
                estado = CN.CountSelect(sql.Verificacion_transaccion, parametros_);
            }
            return estado;



        }
        public DataTable Listado_mancomunadas(CREDENCIALES _CRED, int tipo_veri, out int sin_huellas)
        {
            ODBC_CONN CN;           
            sql = new QUERIES();       
            DataTable dt = new DataTable();
            Dictionary<string, object> parametros_ = new Dictionary<string, object>()
            {
                  {"_TIPO", tipo_veri},
                  {"CUENTA_AHO", _CRED.cta},
                  {"IDENTIDAD", _CRED.identidad},
                  {"COOP_CODIGO", _CRED.codigo},
                  {"COMPAÑIA", _CRED.cia},
                  {"FILIAL", _CRED.fil}
            };
            if (_CRED.identidad != null)
            {
                CN = new ODBC_CONN();
                dt = CN.EjecutarConsultaSelect(sql.Verificacion_transaccion, parametros_);
            }

            int conta = 0;
            foreach (DataRow item in dt.Rows)
            {
                int eshuel = NO_Existe_huella(item.ItemArray[3].ToString());
                if (eshuel==0)
                {
                    conta++;    
                }
            }


            sin_huellas = conta;

            return dt;



        }


        public int NO_Existe_huella(string identidad)
        {
            ODBC_CONN CN;
            sql = new QUERIES();
            Dictionary<string, object> parametro_huella = new Dictionary<string, object>()
            {
                  {"IDENTIDAD", identidad}
            };
            CN = new ODBC_CONN();
            return CN.CountSelect(sql.existe_en_huellas, parametro_huella);
        }
        public int Verifica_mancomuna(CREDENCIALES _CRED, string identidad, int tipo_veri)
        {
            ODBC_CONN CN;
            int estado = 0;
            sql = new QUERIES();
            Dictionary<string, object> parametros_ = new Dictionary<string, object>()
            {
                  {"_TIPO", tipo_veri},
                  {"CUENTA_AHO", _CRED.cta},
                  {"IDENTIDAD", identidad},
                  {"COOP_CODIGO", ""},
                  {"COMPAÑIA", _CRED.cia},
                  {"FILIAL", _CRED.fil}
            };
            
                CN = new ODBC_CONN();
                estado = CN.CountSelect(sql.Verificacion_transaccion, parametros_);
            
            return estado;
        }
        public int Es_Mancomunada(CREDENCIALES cred)
        {        
            sql = new QUERIES();
            ODBC_CONN conn = new ODBC_CONN();
            Dictionary<string, object> pa = new Dictionary<string, object> {
                {"cta", cred.cta },
                {"cia", cred.cia },
                {"fil", cred.fil}
            };
            int conteo = conn.CountSelect(sql.Es_mancomunada, pa);
            return conteo;
        }

        public string trae_secuencial()
        {
            ODBC_CONN cn = new ODBC_CONN();
            sql = new QUERIES();

            try
            {               
                return cn.primeraCol(sql.secuencial,null);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        //roles y permisos
        public DataTable listaUsuarios() { 
            sql = new QUERIES();
            ODBC_CONN bd = new ODBC_CONN();
            DataTable dt = new DataTable();
            try
            {
                dt = bd.EjecutarConsultaSelect(sql.ListaUsuarios);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
            return dt;
        }
        public DataTable ListaRoles() { 
            DataTable dataTable = new DataTable();
            ODBC_CONN bd = new ODBC_CONN();
            sql = new QUERIES();

            try
            {
                dataTable = bd.EjecutarConsultaSelect(sql.ListaRoles);
            }
            catch (Exception ex)      
            {
                throw new Exception(ex.Message);
               

            }

            return dataTable;
        }
        public int roles_Admin(ROLES ROL, int tipo)
        {
            ODBC_CONN cn = new ODBC_CONN();
            sql = new QUERIES();
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>
                     {
                        {"@TIPO",tipo },{"@ID_ROLHUE",ROL.ID_ROLHUE }, {"@NOMBRE_ROLHUE",ROL.NOMBRE_ROLHUE },{"@PERMISOS_ROLHUE", ROL.PERMISOS_ROLHUE},{"@USR_AGREGO_ROLHUE", ROL.USR_AGREGO_ROLHUE}
                     };
               return cn.CountSelect(sql.SP_USR_PERMISOS_HUE, parametros);
              
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public int registraAcceso(Token_Payload pl, ROLES rol, int tipo)
        {
            sql = new QUERIES();
            Security secu = new Security();
            ODBC_CONN CN = new ODBC_CONN();
            string token = secu.GenerarToken("toUsuario", pl);

                Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                   {"@tipo",tipo }, {"@ID_ROLUSR", rol.ID_ROLHUE },{"@NOMBRE_USR", pl.NOMBRE_USR},{"@TOKEN_ID", token},{"@USR_CREADO_USR", rol.USR_AGREGO_ROLHUE}
                };

                return  CN.CountSelect(sql.SP_USR_PERMISOS_HUE, parametros);


        }

        public DataTable informacion_acceso (string usuario) 
        {
            sql = new QUERIES();
            ODBC_CONN cn = new ODBC_CONN();
            Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                    {"@USR", usuario}//DESDE EL DRID
                };
            DataTable dt = cn.EjecutarConsultaSelect(sql.JOIN_USR_ROL, parametros);
            return dt;

        }
        public int Quitar_Accso_rol(string usuario)
        { 
            sql = new QUERIES();
            ODBC_CONN cn = new ODBC_CONN();
            Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                   {"@nombre_usr",  usuario}

                };
            return cn.EjecutarParametrizada(sql.quitar_rol, parametros);
        }

        public int comprobarExistenciObjetos(string nombre_objeto)
        {
            sql = new QUERIES();
            ODBC_CONN cn = new ODBC_CONN();
            Dictionary<string, object> parametros = new Dictionary<string, object>
                {
                   {"@nombreObjeto",  nombre_objeto}

                };
            if (nombre_objeto == "comprobar_aditamentos")
            {
                return cn.CountSelect(sql.FuntioncomprobarObjeto, parametros);
            }
            else
            {  
                return cn.CountSelect(sql.comprobarObjeto, parametros);
            }        

        }


    }


   
}
