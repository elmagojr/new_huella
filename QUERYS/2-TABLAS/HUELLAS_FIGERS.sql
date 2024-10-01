CREATE TABLE "DBA"."HUELLAS_FIGERS" (
	"HUE_ID" INTEGER NOT NULL DEFAULT AUTOINCREMENT,
	"HUE_CODIGO" VARCHAR(50) NULL,
	"HUE_IDENTIDAD" VARCHAR(50) NOT NULL,
	"HUE_TIPO_PER" VARCHAR(50) NOT NULL,
	"HUELLA" LONG BINARY NOT NULL UNIQUE,
	"FECHA_CREACION" "datetime" NULL DEFAULT CURRENT TIMESTAMP,
	"HUE_COMPANIA" INTEGER NULL,
	"HUE_FILIAL" INTEGER NULL,
	"HUE_OBSERVACION" VARCHAR(200) NOT NULL,
	"FLAG" INTEGER NULL DEFAULT 0,
	"DEDO" VARCHAR(20) NULL,
	"HUELLA_SAMPLE" LONG BINARY NULL,
	"USR_AGREGO" VARCHAR(100) NULL,
	PRIMARY KEY ( "HUE_ID" ASC, "HUELLA" ASC, "HUE_IDENTIDAD" ASC )
) IN "system";
COMMENT ON TABLE "DBA"."HUELLAS_FIGERS" IS 'TABLA DONDE SE REGISTRAN LAS HUELLAS DIGITALES DE LAS PERSONAS';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."HUE_ID" IS 'identificador unico';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."HUE_CODIGO" IS 'si HUE_TIPO_PER  es  Coop, guarda el codigo afiliado; si es DEF el secuencial del def con que se registro la huella ';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."HUE_IDENTIDAD" IS 'idenditidad de la persona';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."HUE_TIPO_PER" IS 'tipo de persona COOP para cooperativista y DEF para los registrados en la tabla detalles_firma';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."HUELLA" IS 'guarda la cadena hexadeciamal de la huella';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."HUE_OBSERVACION" IS 'observacion a la hora de registrar la huella';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."FLAG" IS 'al hacer una verificacion de esta huella se coloca en 1, el addon tomara el valor en 1 para validar la huella';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."DEDO" IS 'identificador de que dedo es: mano derecha DD y DI la izquierda (1 pulgar 5 meñique, en suscecion)  ';
COMMENT ON COLUMN "DBA"."HUELLAS_FIGERS"."HUELLA_SAMPLE" IS 'guarda la imagen de la huella en jpeg en resolucion 80x80 pixeles';


CREATE TRIGGER "trg_tablas_parame" AFTER UPDATE
ORDER 1 ON "DBA"."HUELLAS_FIGERS"
REFERENCING OLD AS old_name NEW AS new_name
FOR EACH ROW /* WHEN( search_condition ) */
BEGIN
        declare Campo VARCHAR(100);
        declare Query VARCHAR(1000);
        declare Valor_Anterior VARCHAR(255);
        declare Valor_Nuevo VARCHAR(255);
        declare ExisteCampo integer;
        
        select count(column_name) INTO ExisteCampo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'HUELLAS_FIGERS'); 
       -- SELECT COUNT(*) INTO ExisteCampo FROM DBA.TABLAS_PARAMETRIZADAS WHERE TP_COMPANIA = new_name.HUE_COMPANIA AND TP_TABLA = 'HUELLAS_FIGERS' AND TP_ESTADO=1; 
        
        if(ExisteCampo>0) then
        begin
            declare Inserta_Bitacora dynamic scroll cursor for
            select column_name INTO Campo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'HUELLAS_FIGERS');
            --SELECT TP_CAMPO INTO Campo FROM DBA.TABLAS_PARAMETRIZADAS WHERE TP_COMPANIA = new_name.HUE_COMPANIA AND TP_TABLA = 'HUELLAS_FIGERS' AND TP_ESTADO=1;  
            open Inserta_Bitacora;
            fetch next Inserta_Bitacora into Campo;
        
            if(@@sqlstatus = 2) then
              print "No se encontró ningun Registro";
              close Inserta_Bitacora;
              return
            end if;
        
            while(@@sqlstatus = 0) loop
          
            SET Query = 'SELECT old_name.' + Trim(Campo) +',new_name.' + Trim(Campo) + ' INTO Valor_Anterior,Valor_Nuevo FROM DUMMY';
            EXECUTE IMMEDIATE Query;
        
            IF(Trim(Valor_Anterior) <> Trim(Valor_Nuevo)) THEN
               insert into DBA.HISTO_TABLAS ("HT_TABLA","HT_CAMPO_PK","HT_VALOR_PK","HT_CAMPO_AFECTADO","HT_VALOR_ANTERIOR","HT_VALOR_NUEVO") 
               values('HUELLAS_FIGERS', 'HUE_ID', new_name.HUE_ID,Campo,Valor_Anterior,Valor_Nuevo) 
            END IF;
        
               fetch next Inserta_Bitacora into Campo;
               end loop;
               close Inserta_Bitacora;      
            end;
         
            
        end if; 
END;
