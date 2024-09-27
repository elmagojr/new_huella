CREATE TABLE "DBA"."FIRMAS_X_CUENTA" (
	"FXC_SEQ" INTEGER NOT NULL UNIQUE,
	"FXC_IDENTIDAD" VARCHAR(50) NULL,
	"FXC_CTA_AHO" VARCHAR(50) NULL,
	"FXC_TIPO_PERSONA" VARCHAR(10) NULL,
	"FXC_MANCOMUNADA" INTEGER NULL DEFAULT 0,
	"FXC_AGREGO" VARCHAR(50) NULL,
	"FXC_FECHA_AGREGO" "datetime" NULL DEFAULT CURRENT TIMESTAMP,
	"FXC_MOD" VARCHAR(50) NULL,
	"FXC_FECHA_MOD" "datetime" NULL,
	"FXC_IDEN_DEF" VARCHAR(50) NULL,
	"FXC_COMPANIA" INTEGER NULL,
	"FXC_FILIAL" INTEGER NULL,
	"FXC_PARENTEZCO" VARCHAR(50) NULL,
	PRIMARY KEY ( "FXC_SEQ" ASC )
) IN "system";
COMMENT ON TABLE "DBA"."FIRMAS_X_CUENTA" IS 'Tabla que almacena personas que estan relacionadas a una cuenta de ahorro.';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_SEQ" IS 'Indentificador unico';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_IDENTIDAD" IS 'Numero de indentidad de la persona relacionada a la cuenta';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_CTA_AHO" IS 'Numero de cuenta de ahorro';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_TIPO_PERSONA" IS 'Tipo de la persona, COOP = Cooperativista / DEF = Firma autorizada';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_MANCOMUNADA" IS 'Campo que identifica si la persona registrada mancomuna la cuenta de ahorro';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_AGREGO" IS 'Usuario que agrego';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_FECHA_AGREGO" IS 'fecha en que se agrego';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_MOD" IS 'Usuario que modifico';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_FECHA_MOD" IS 'Fecha en que se modifico';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_IDEN_DEF" IS 'Identificacion de registro si se trata de una Firma autorizada';
COMMENT ON COLUMN "DBA"."FIRMAS_X_CUENTA"."FXC_PARENTEZCO" IS 'QUE PARENTEZCO TIENE LA PERSONA DE ESTA FIRMA CON EL DUEÑO DE LA CUENTA';


CREATE TRIGGER "after_actauliza_identidad_firmas" AFTER UPDATE OF "FXC_IDENTIDAD"
ORDER 2 ON "DBA"."FIRMAS_X_CUENTA"
 REFERENCING OLD AS old_name NEW AS new_name 
FOR EACH ROW /* WHEN( search_condition ) */
BEGIN
DECLARE @contenido INTEGER ;
DECLARE @firma varchar (100);

--old_name.FXC_IDENTIDAD
--old_name.FXC_CTA_AHO

WITH  temporal AS(    
SELECT 'F1' as firma, Codigo_Cta_Aho as CTA, FIRMA1_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
    WHERE Codigo_Cta_Aho = old_name.FXC_CTA_AHO 
    UNION
    SELECT 'F2' as firma, Codigo_Cta_Aho as CTA, FIRMA2_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
    WHERE Codigo_Cta_Aho = old_name.FXC_CTA_AHO
    UNION 
    SELECT 'F3' as firma, Codigo_Cta_Aho as CTA, FIRMA3_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
    WHERE Codigo_Cta_Aho = old_name.FXC_CTA_AHO
    UNION 
    SELECT 'F4' as firma, Codigo_Cta_Aho as CTA, FIRMA4_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
    WHERE Codigo_Cta_Aho = old_name.FXC_CTA_AHO
)


  SELECT COUNT(*) INTO @contenido FROM temporal where identidad =old_name.FXC_IDENTIDAD;
if  @contenido > 0 then
WITH  temporal AS(    
    SELECT 'F1' as firma, Codigo_Cta_Aho as CTA, FIRMA1_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
    WHERE Codigo_Cta_Aho = old_name.FXC_CTA_AHO 
    UNION
    SELECT 'F2' as firma, Codigo_Cta_Aho as CTA, FIRMA2_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
    WHERE Codigo_Cta_Aho = old_name.FXC_CTA_AHO
    UNION 
    SELECT 'F3' as firma, Codigo_Cta_Aho as CTA, FIRMA3_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
    WHERE Codigo_Cta_Aho = old_name.FXC_CTA_AHO
    UNION 
    SELECT 'F4' as firma, Codigo_Cta_Aho as CTA, FIRMA4_AHO_IDEN AS IDENTIDAD FROM  dba.Firmas_Ahorro 
    WHERE Codigo_Cta_Aho = old_name.FXC_CTA_AHO
)
SELECT firma into @firma from temporal where identidad =old_name.FXC_IDENTIDAD;
    case   @firma 
    when 'F1' THEN
        update dba.Firmas_Ahorro set FIRMA1_AHO_IDEN = new_name.FXC_IDENTIDAD where FIRMA1_AHO_IDEN =old_name.FXC_IDENTIDAD;

    WHEN 'F2' THEN 
        update dba.Firmas_Ahorro set FIRMA2_AHO_IDEN = new_name.FXC_IDENTIDAD where FIRMA2_AHO_IDEN =old_name.FXC_IDENTIDAD;
    WHEN 'F3' THEN 
        update dba.Firmas_Ahorro set FIRMA3_AHO_IDEN = new_name.FXC_IDENTIDAD where FIRMA3_AHO_IDEN = old_name.FXC_IDENTIDAD;
    WHEN 'F4' THEN 
        update dba.Firmas_Ahorro set FIRMA4_AHO_IDEN = new_name.FXC_IDENTIDAD where FIRMA4_AHO_IDEN = old_name.FXC_IDENTIDAD;
    END CASE;
end if;
	/* Type the trigger statements here */
END;
COMMENT ON TRIGGER "DBA"."FIRMAS_X_CUENTA"."after_actauliza_identidad_firmas" IS 'actauliza la identidad en la tabla Firmas Ahorros';



CREATE TRIGGER "tgr_secuencial_fxc" BEFORE INSERT
ORDER 1 ON "DBA"."FIRMAS_X_CUENTA"
REFERENCING NEW AS new_name 
FOR EACH ROW  WHEN( current user <> 'DBA' and current user <> 'USR_SPS' ) 
BEGIN
  declare numero decimal(15);
  declare filial smallint;
  declare compania smallint;
  select USU_FILIAL,USU_COMPANIA into filial,compania from DBA.Usuarios where USU_CODIGO = current user;
  select func_llaves('FIRMAS_X_CUENTA.FXC_SEQ',filial) into numero from SYS.DUMMY;
  set new_name.FXC_SEQ=numero;
END;
