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
       public string InsertarHistorialHuella = "INSERT INTO DBA.HISTO_HUELLAS (HISTO_HUE_ID,HISTO_HUE_CODIGO,HISTO_HUE_IDENTIDAD,HISTO_TIPO_PER,HISTO_HUE_FECHA_AGREGO,HISTO_HUE_FECHA_ELIMINA,HISTO_HUE_MOTIVO_ELIMINA,HISTO_HUE_USR_ELIMINA,HISTO_HUE_USR_AGREGO) VALUES (?,?,?,?,?,?,?,?,?)";
       public readonly string Verificacion_transaccion = "call dba.VERIFICA_TRANSACCION_HUELLA(?,?,?,?,?,?)";
       public readonly string Es_mancomunada = "select count(*) from dba.FIRMAS_X_CUENTA where FXC_MANCOMUNADA ='1' and FXC_CTA_AHO =? and  fxc_filial =? and fxc_compania =?";

    }
}
