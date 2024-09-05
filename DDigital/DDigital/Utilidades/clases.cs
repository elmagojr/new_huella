using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDigital.Utilidades
{
    internal class clases
    {
    }
    public class MANO
    {
        public string QUE_MANO { get; set; }
        public string QUE_DEDO { get; set; }

    }
    public class HUELLA
    {
        public object _HUE_ID { get; set; }
        public object _HUE_CODIGO { get; set; }
        public object _HUE_IDENTIDAD { get; set; }
        public object _HUE_TIPO_PER { get; set; }
        public byte[] _HUELLA { get; set; }
        public DateTime _FECHA_CREACION { get; set; }
        public string _HUE_COMPANIA { get; set; }
        public string _HUE_OBSERVACION { get; set; }
        public object _FLAG { get; set; }
        public string _DEDO { get; set; }
        public byte[] _HUELLA_SAMPLE { get; set; }
        public string _USR_AGREGO { get; set; }

    }

    public class DATA_PERSONA
    {
        public string IDENTIDAD { get; set; }
        public string CODIGO { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO { get; set; }  

    }

    public class CREDENCIALES
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string identidad { get; set; }
        public string fromAction { get; set; }
        public string cta { get; set; }
        public string cia { get; set; }
        public string fil { get; set; }
        //subidentificador
        public string usr_logged { get; set; } //ESTO PARA INICIAR SESION
    }
}
