using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDigital.Utilidades
{
    public class QUERIES
    {
       public readonly string select_toda_huella = "SELECT * FROM dba.HUELLAS_FIGERS";
       public readonly string insert_in_figgers = "INSERT INTO DBA.HUELLAS_figers (HUE_CODIGO, HUE_IDENTIDAD,HUE_TIPO_PER, HUELLA, HUE_COMPANIA,  HUE_OBSERVACION, DEDO, HUELLA_SAMPLE,USR_AGREGO) VALUES (?, ?, ?, ?, ?,?,?,?,?)";
       public readonly string SELECT_SP_BUSQUEDA = "CALL DBA.SP_BUSCAR_PERSONA(?,?)";
       public readonly string DEDOS_REGISTRADOS = "select dedo from dba.HUELLAS_FIGERS where HUE_IDENTIDAD = ?";
       public readonly string sp_selccion_mano = "call dba.sp_select_mano(?,?)";
       public string EliminarHuella = $"DELETE FROM DBA.HUELLAS_FIGERS WHERE HUE_ID =?";
       public string InsertarHistorialHuella = "INSERT INTO DBA.HISTO_HUELLAS (HISTO_TABLA,HISTO_CAMPO,HISTO_VANTERIOR,HISTO_VACTUAL,HISTO_ACCION,HISTO_USR_ACCION,HISTO_INFO_ADICIONAL,HISTO_OBSERVACION) VALUES(?,?,?,?,?,?,?,?)";
       public readonly string Verificacion_transaccion = "call dba.VERIFICA_TRANSACCION_HUELLA(?,?,?,?,?,?)";
       public readonly string Es_mancomunada = "select count(*) from dba.FIRMAS_X_CUENTA where FXC_MANCOMUNADA ='1' and FXC_CTA_AHO =? and  fxc_filial =? and fxc_compania =?";

        public readonly string secuencial = "SELECT LEFT((SELECT MAX (OFI_UNI_SEQ) AS SEQ FROM DBA.F_Oficinas where OFI_CODIGO =1), LENGTH((SELECT MAX (OFI_UNI_SEQ) AS SEQ FROM DBA.F_Oficinas where OFI_CODIGO =1)) - 4) AS Resultado";
        //ROLES Y PERMISOS
        public readonly string ListaUsuarios = "SELECT USU_CODIGO AS 'USUARIO', USU_NOMBRE AS 'NOMBRE USUARIO' FROM DBA.Usuarios where usuario <> 'HID' and  USU_ACTIVO =1";
        public readonly string ListaRoles = "SELECT ID_ROLHUE,nombre_rolhue, permisos_rolhue FROM DBA.ROL_HUELLA";
        public readonly string JOIN_USR_ROL = "SELECT ID_ROLUSR,NOMBRE_USR, nombre_rolhue, permisos_rolhue,TOKEN_ID FROM DBA.USUARIOS_HUELLAS join dba.rol_huella on ID_ROLUSR = id_rolhue where nombre_usr =?";
        public readonly string SP_USR_PERMISOS_HUE = "CALL DBA.SP_USRS_HUELLAS(?,?,?, ?, ?)";
        public readonly string quitar_rol = "DELETE FROM DBA.USUARIOS_HUELLAS WHERE NOMBRE_USR=?";
        public readonly string comprobarObjeto = "SELECT DBA.comprobar_aditamentos(?)";
        public readonly string FuntioncomprobarObjeto = "select count(*) from sys.SYSPROCEDURE where proc_name = 'comprobar_aditamentos'";



    }
}
