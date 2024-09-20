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
        public string _HUE_IDENTIDAD { get; set; }
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
    public class HISTO_HUELLAS
    {
        public string _HUE_ID { get; set; }
        public string _HUE_CODIGO { get; set; }
        public object _HUE_IDENTIDAD { get; set; }
        public string _TIPO_PER { get; set; }
        public DateTime _HUE_FECHA_AGREGO { get; set; }
        public DateTime _HUE_FECHA_ELIMINA { get; set; }
        public string _HUE_MOTIVO_ELIMINA { get; set; }
        public string _HUE_USR_ELIMINA { get; set; }
        public string _HUE_USR_AGREGO { get; set; }
    }

    public class DATA_PERSONA
    {
        public string IDENTIDAD { get; set; }
        public string CODIGO { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO { get; set; }
        public string ESTADO { get; set; }

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


    public class DATA_INTERSEPTOR
    {
        public string NOMBRE_VERIFICA { get; set; }
        public string HUE_CODIGO { get; set; }
        public string HUE_IDENTIDAD { get; set; }
        public string TIPO_PER { get; set; }
        public string FLAG { get; set; }
        public string USR_VERIFICO { get; set; }


    }


    public class ROLES
    {
        public string ID_ROLHUE { get; set; }
        public string NOMBRE_ROLHUE { get; set; }
        public object PERMISOS_ROLHUE { get; set; }
        public string USR_AGREGO_ROLHUE { get; set; }
        public DateTime FECHA_CREA_ROLHUE { get; set; }
        public DateTime FECHA_MODI_ROLHUE { get; set; }
        public string USR_MODI_ROLHUE { get; set; }
    }


    public class Token_Payload
    {
        public string ListaPermisos { get; set; }
        public string EsPermitido { get; set; }
        public string NOMBRE_USR { get; set; }
        public string ROL { get; set; }
    }
    public class PERMISOS
    {
        public string Tiene_acceso { get; set; }
        public string nombre_rol {  get; set; }
        public bool RegistrarHuellas { get; set; }
        public bool VerificarHuellas { get; set; }
        public bool VerificarMancomuna { get; set; }
        public bool SeleccionarMano { get; set; }
        public bool SeleccionarDedos { get; set; }
        public bool VerHuellas { get; set; }
        public bool EliminarHuellas { get; set; }
        public bool AdmonUsuarios { get; set; }
        public bool QuitarAccesoUsr { get; set; }
        public bool AdmonRoles { get; set; }
    }


    public class INFO_SISTEMA
    {
        public string NOMBRE_MAQUINA { get; set; }
        public string IP_MAQUINA { get; set; }
        public string SEQUENCIAL_SISTEMA { get; set; }
        public string USUARIO_MAQUINA { get; set; }
        public string VERION_OS_MAQUINA { get; set; }
        public string DOMINIO_MAQUINA { get; set; }
        public string MACHINE_NAME { get; set; }


    }

}
