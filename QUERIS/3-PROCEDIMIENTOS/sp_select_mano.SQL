CREATE PROCEDURE "DBA"."sp_select_mano"(
in accion_ int,
in cadena_ varchar(200)
 /* [IN | OUT | INOUT] parameter_name parameter_type [DEFAULT default_value], ... */ )
/* RESULT( column_name column_type, ... ) */
BEGIN
    DECLARE @RowsAffected INT;

  if accion_ = 1 then
    if EXISTS  (select * from dba.PARAMETROS_X_EMPRESA where PA_ABREV_PARAMETRO = 'DEDO_MANO_ACTIVO') THEN 
    UPDATE  dba.PARAMETROS_X_EMPRESA set PA_VALOR =cadena_ where PA_ABREV_PARAMETRO = 'DEDO_MANO_ACTIVO';
        SET @RowsAffected = @@ROWCOUNT;
  
     IF @RowsAffected > 0 THEN        
         COMMIT;
        select @RowsAffected;
     ELSE   
         ROLLBACK;
     END IF;
    ELSE 
    RAISERROR 17000 'Parametro de Seleccion de manos no invocado. Favor contacte al personal de soporte';
    end if;    
    ELSEIF   accion_ = 2 THEN 
    select PA_VALOR from dba.PARAMETROS_X_EMPRESA where PA_ABREV_PARAMETRO = 'DEDO_MANO_ACTIVO';
   end if; 
END;
COMMENT ON PROCEDURE "DBA"."sp_select_mano" IS 'PROCEDIMIENTO PARA GESTIONAR PARAMETRO POR EMPRESA DEDO_MANO_ACTIVO, QUE GUARDA LOS DEDOS PERMITIDOS PARA USAR';
