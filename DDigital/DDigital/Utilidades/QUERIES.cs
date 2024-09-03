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
    }
}
