CREATE PROCEDURE "DBA"."SP_BUSCAR_PERSONA"(
IN @IDENTIDAD VARCHAR (100),
IN @_TIPO INTEGER  
 /* [IN | OUT | INOUT] parameter_name parameter_type [DEFAULT default_value], ... */ )
/* RESULT( column_name column_type, ... ) */
BEGIN
IF @_TIPO = 0 THEN--EN CASO DE SER SOLO CON COD COOPERATIVISTA

SELECT  COOP_IDENTIDAD AS Identidad, coop_nombre as 'NOMBRE',coop_codigo as Codigo, 'COOP' as PERTENECE, coop_estado_Actual as 'ESTADO' FROM dba.cooperativistas where coop_codigo =@IDENTIDAD and COOP_ES_EMPRESA=0;

END IF;

IF @_TIPO = 1 THEN--EN CASO DE SOE POR IDENTIDAD

    if exists (select coop_identidad from dba.cooperativistas where coop_identidad =@IDENTIDAD) then
        select TOP 1  coop_nombre as 'NOMBRE',coop_identidad as 'IDENTIDAD', coop_codigo as 'CODIGO', 'COOP' as 'TIPO', coop_estado_Actual as 'ESTADO' from dba.cooperativistas where coop_identidad =@IDENTIDAD
        and COOP_ES_EMPRESA =0 and coop_identidad <> '' and coop_identidad <> '0' ORDER BY COOP_FECHA_AGR DESC

        else
        
        select def_identidad as 'IDENTIDAD', def_nombre as 'NOMBRE' ,DEF_SEC as 'CODIGO', 'DEF' as 'TIPO' , 0 as 'ESTADO'      
         from dba.Detalle_Firmas where def_identidad =@IDENTIDAD and def_es_representante =0
    end if;
END IF;
    
IF @_TIPO = 2 THEN --PARA EL LISTADO DE HUELLAS
 
    if exists (select coop_identidad from dba.cooperativistas where coop_identidad =@IDENTIDAD) then
        select coop_nombre as 'NOMBRE', HUE_IDENTIDAD AS 'IDENTIDAD', DEDO, HUE_OBSERVACION, FECHA_CREACION, HUELLA_SAMPLE, HUE_ID, HUE_CODIGO as 'CODIGO', HUE_TIPO_PER as 'TIPO', USR_AGREGO, coop_estado_Actual as 'ESTADO' 
        from   dba.HUELLAS_FIGERS 
        join dba.COOPERATIVISTAS on COOP_IDENTIDAD = hue_identidad
        where HUE_IDENTIDAD = @IDENTIDAD ORDER BY COOP_FECHA_AGR DESC
    else

        SELECT DISTINCT def_nombre AS 'NOMBRE', 
                        HUE_IDENTIDAD AS 'IDENTIDAD', 
                        DEDO, 
                        HUE_OBSERVACION, 
                        FECHA_CREACION, 
                        HUELLA_SAMPLE, 
                        HUE_ID, 
                        HUE_CODIGO AS 'CODIGO', 
                        HUE_TIPO_PER AS 'TIPO', 
                        USR_AGREGO, 
                        0 AS 'ESTADO'
        FROM dba.HUELLAS_FIGERS 
        left JOIN dba.Detalle_Firmas ON DEF_IDENTIDAD = IDENTIDAD
        WHERE IDENTIDAD = @IDENTIDAD;
--        select def_nombre as 'NOMBRE', HUE_IDENTIDAD AS 'IDENTIDAD', DEDO, HUE_OBSERVACION, FECHA_CREACION, HUELLA_SAMPLE, HUE_ID, HUE_CODIGO as 'CODIGO', HUE_TIPO_PER as 'TIPO', USR_AGREGO, 0 as 'ESTADO' 
--        from   dba.HUELLAS_FIGERS 
--        join dba.Detalle_Firmas on DEF_IDENTIDAD =IDENTIDAD
--        where def_sec = @IDENTIDAD
   

    end if;
END IF; 


END
--CALL DBA.SP_BUSCAR_PERSONA('147852369', 1)
--select * from dba.Detalle_Firmas where def_sec = '110008704';
