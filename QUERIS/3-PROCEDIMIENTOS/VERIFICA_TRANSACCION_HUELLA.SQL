CREATE PROCEDURE "DBA"."VERIFICA_TRANSACCION_HUELLA"(
    IN @_TIPO integer,
    IN @CUENTA_AHO VARCHAR(100),
    IN @IDENTIDAD VARCHAR (100),     
    IN @COOP_CODIGO VARCHAR(100),
    IN @CIA integer ,
    IN @FILIAL integer ,
    out @verificado integer 
    
     /* [IN | OUT | INOUT] parameter_name parameter_type [DEFAULT default_value], ... */ )
    /* RESULT( column_name column_type, ... ) */
    BEGIN
         declare @conteoMan int;  
         set @conteoMan= (select count(*) from dba.FIRMAS_X_CUENTA where FXC_MANCOMUNADA ='1' and FXC_CTA_AHO =@CUENTA_AHO and FXC_COMPANIA = @CIA); 
--     DECLARE @RowsAffected INT;
--     set @RowsAffected =@@ROWCOUNT;
    IF @_TIPO = 0 THEN --VERIFICA PARA CUENTAS PROPIAS
        IF @conteoMan <=1 then
                 IF EXISTS  (select * from dba.cooperativistas where coop_codigo =@COOP_CODIGO and coop_identidad =@IDENTIDAD) then
                    if exists (select * from dba.ahorros where aho_codigo_coo =@COOP_CODIGO and aho_codigo_cta =@CUENTA_AHO  AND AHO_COMPANIA =@CIA) then
                          set  @verificado =1;
                    else
                        set  @verificado =0;
                    end if;
                 else
                    set  @verificado =0;
                 end if;  
                else
                 set  @verificado = -999;
            end if;  
 select @verificado    
    ENDIF ;

    IF @_TIPO = 1 THEN --para verificar a terceros
          IF @conteoMan <=1 then
            IF EXISTS  (select * from dba.FIRMAS_X_CUENTA where FXC_IDENTIDAD =@IDENTIDAD and FXC_CTA_AHO =@CUENTA_AHO and FXC_COMPANIA = @CIA) then
                set  @verificado =1;
             else
                set  @verificado =0;
             end if;  
         else
             set  @verificado = -999;
        end if;
    select @verificado 
    END IF;

    IF @_TIPO= 2 THEN --para verificar mancomunadas. Pasará aqui uno por uno
    if @conteoMan > 1 then
      IF EXISTS  (select * from dba.FIRMAS_X_CUENTA where FXC_IDENTIDAD =@IDENTIDAD and FXC_CTA_AHO =@CUENTA_AHO and FXC_MANCOMUNADA =1 and FXC_COMPANIA = @CIA) then
            set  @verificado =1;
         else
            set  @verificado =0;
      end if;        
    else
       set  @verificado = -999; --aqui un codigo que avisa que esta queriendo tocar una cta mancomunada
    end if;
    select @verificado;   
    END IF; 

    IF @_TIPO= 3 THEN --enlista los mancomunados 
            SELECT 
              FXC.FXC_TIPO_PERSONA as Tipo,
              fxc.FXC_SEQ as Secuencia,
              CASE 
                WHEN FXC.FXC_TIPO_PERSONA = 'COOP' THEN COOP.COOP_NOMBRE
                WHEN FXC.FXC_TIPO_PERSONA = 'DEF' THEN DF.DEF_NOMBRE
              END AS NOMBRE,
              CASE 
                WHEN FXC.FXC_TIPO_PERSONA = 'COOP' THEN COOP.COOP_IDENTIDAD
                WHEN FXC.FXC_TIPO_PERSONA = 'DEF' THEN DF.DEF_IDENTIDAD
              END AS IDENTIDAD, 
            FXC.FXC_FECHA_AGREGO as 'Fecha Agrego',       
            isnull(firma, 'N/A', firma) as Firma
            FROM 
              "DBA"."FIRMAS_X_CUENTA" FXC
            LEFT JOIN DBA.COOPERATIVISTAS COOP ON FXC.FXC_TIPO_PERSONA = 'COOP' AND COOP.COOP_IDENTIDAD = FXC.FXC_IDENTIDAD
            LEFT JOIN DBA.Detalle_Firmas DF ON FXC.FXC_TIPO_PERSONA = 'DEF' AND DF.DEF_SEC = FXC.FXC_IDEN_DEF
            LEFT JOIN (
                SELECT 'Firma 1' as firma, Codigo_Cta_Aho as CTA, FIRMA1_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
                WHERE Codigo_Cta_Aho = @CUENTA_AHO 
                UNION
                SELECT 'Firma 2' as firma, Codigo_Cta_Aho as CTA, FIRMA2_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
                WHERE Codigo_Cta_Aho = @CUENTA_AHO
                UNION 
                SELECT 'Firma 3' as firma, Codigo_Cta_Aho as CTA, FIRMA3_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
                WHERE Codigo_Cta_Aho = @CUENTA_AHO
                UNION 
                SELECT 'Firma 4' as firma, Codigo_Cta_Aho as CTA, FIRMA4_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
                WHERE Codigo_Cta_Aho = @CUENTA_AHO
            ) AS FF ON FF.CTA = FXC.FXC_CTA_AHO AND FF.IDENTIDAD = FXC.FXC_IDENTIDAD 
            
            WHERE FXC.FXC_CTA_AHO = @CUENTA_AHO and firma <>'N/A' AND FXC_MANCOMUNADA =1 and FXC_COMPANIA = @CIA AND FXC_FILIAL=@FILIAL order by firma asc;
    END IF; 
end 
    
