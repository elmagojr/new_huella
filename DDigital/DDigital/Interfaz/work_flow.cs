using DDigital.Utilidades;
using DPUruNet;
using ProyectoDIGITALPERSONA;
using System;
using System.Collections.Generic;
using System.Data;
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
        public bool verificacion_huella(Fmd fmd1)
        {
            bool verifica = false;
            ODBC_CONN bd = new ODBC_CONN();
            sql = new QUERIES();
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
                        verifica = true;
                        break;
                    }
                }
            }
            return verifica;
        }

        public (DATA_PERSONA Persona, HUELLA hue_data) InformacionVerificacion(string IDENTIDAD, int tipo)
        {

            DATA_PERSONA dp = new DATA_PERSONA();
            HUELLA dh = new HUELLA();
            ODBC_CONN con = new ODBC_CONN();
            sql = new QUERIES();
            UT = new UTILIDADES();
            Dictionary<string, object> parametros = new Dictionary<string, object> {
                            {"@identidad",IDENTIDAD },
                            {"@_tipo",  tipo}
                        };
            DataTable data_gen = con.EjecutarConsultaSelect(sql.SELECT_SP_BUSQUEDA, parametros);

            if (data_gen.Rows.Count>0)
            {
                using (DataTableReader reader = data_gen.CreateDataReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader["NOMBRE"].ToString();
                        string identidad = reader["IDENTIDAD"].ToString();
                        string dedo = reader["DEDO"].ToString();
                        string OBSERVACION = reader["HUE_OBSERVACION"].ToString();
                        string FECHA = reader["FECHA_CREACION"].ToString();
                        MemoryStream MEMORIA = new MemoryStream((byte[])reader["HUELLA_SAMPLE"]);
                        string hue_id = reader["HUE_ID"].ToString();
                        string CODIGO = reader["CODIGO"].ToString();
                        string tipo_per = reader["HUE_TIPO_PER"].ToString();
                        string usr = reader["USR_AGREGO"].ToString();

                        //data general persona
                        dp.NOMBRE = nombre;
                        dp.TIPO = tipo_per;
                        dp.CODIGO = CODIGO;
                        dp.IDENTIDAD = identidad;
                        //data de su huella
                        dh._DEDO = UT.identificaDedo(dedo);
                        dh._HUE_OBSERVACION = OBSERVACION;
                        dh._FECHA_CREACION = DateTime.Parse(FECHA);
                        dh._HUE_ID = hue_id;
                        dh._USR_AGREGO = usr;
                        dh._HUELLA_SAMPLE = MEMORIA.ToArray();

                    }
                }
            }

            return (dp, dh);
        }
    }


   
}
