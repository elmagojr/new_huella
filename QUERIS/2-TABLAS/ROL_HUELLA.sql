CREATE TABLE "DBA"."ROL_HUELLA" (
	"ID_ROLHUE" INTEGER NOT NULL DEFAULT AUTOINCREMENT,
	"NOMBRE_ROLHUE" VARCHAR(50) NOT NULL UNIQUE,
	"PERMISOS_ROLHUE" VARCHAR(512) NULL,
	"USR_AGREGO_ROLHUE" VARCHAR(50) NULL,
	"FECHA_CREA_ROLHUE" "datetime" NULL DEFAULT CURRENT TIMESTAMP,
	"FECHA_MODI_ROLHUE" "datetime" NULL,
	"USR_MODI_ROLHUE" VARCHAR(50) NULL,
	PRIMARY KEY ( "ID_ROLHUE" ASC )
) IN "system";
COMMENT ON TABLE "DBA"."ROL_HUELLA" IS 'PERMISOS PARA LA HUELLA DIGITAL';
COMMENT ON COLUMN "DBA"."ROL_HUELLA"."NOMBRE_ROLHUE" IS 'nombre del rol';
COMMENT ON COLUMN "DBA"."ROL_HUELLA"."PERMISOS_ROLHUE" IS 'permisos asingandos a este rol mediante token';


CREATE TRIGGER "TGR_INSERTED" before INSERT
ORDER 1 ON "DBA"."ROL_HUELLA"
 REFERENCING NEW AS new_name 
FOR EACH ROW /* WHEN( search_condition ) */
BEGIN
	declare Campo VARCHAR(100);
declare Query VARCHAR(1000);
declare Valor_Anterior VARCHAR(800);
declare Valor_Nuevo VARCHAR(800);
declare ExisteCampo integer;
declare observacion VARCHAR(800);

select count(column_name) INTO ExisteCampo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'ROL_HUELLA'); 

if(ExisteCampo>0) then
    begin
        declare Inserta_Bitacora dynamic scroll cursor for
        select column_name INTO Campo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'ROL_HUELLA');
        open Inserta_Bitacora;
        fetch next Inserta_Bitacora into Campo;
    
        if(@@sqlstatus = 2) then
          print "No se encontró ningun Registro";
          close Inserta_Bitacora;
          return
        end if;
    
        while(@@sqlstatus = 0) loop
        
      SET Query = 'SELECT new_name.' + Trim(Campo) + ' INTO Valor_Nuevo FROM DUMMY';
        EXECUTE IMMEDIATE Query;

        IF (Campo in ('ID_ROLHUE','NOMBRE_ROLHUE','PERMISOS_ROLHUE')) THEN 
                if  Campo = 'PERMISOS_ROLHUE' THEN   
                       set observacion ='Nuevo rol';                 
                      set Valor_Nuevo = '(NEW_DATA)';
                ELSE    
                        set observacion =''; 
                end if;            

                INSERT INTO "DBA"."HISTO_HUELLAS" ("HISTO_TABLA","HISTO_IDENTIFICADOR","HISTO_CAMPO","HISTO_VANTERIOR","HISTO_VACTUAL","HISTO_ACCION","HISTO_USR_ACCION","HISTO_FECHA_ACCION","HISTO_INFO_ADICIONAL","HISTO_OBSERVACION")
                VALUES('ROL_HUELLA',new_name.ID_ROLHUE, Campo,NULL,Valor_Nuevo,'I',current user,CURRENT TIMESTAMP ,(SELECT "DBA"."func_trae_info_sys"(3)),observacion);
        end if;

         fetch next Inserta_Bitacora into Campo;
           end loop;
           close Inserta_Bitacora;      
        end;
 
    
end if; 
END;
COMMENT ON TRIGGER "DBA"."ROL_HUELLA"."TGR_INSERTED" IS 'PARA EL INSERT';


CREATE TRIGGER "TGR_ONDELETE" BEFORE DELETE
ORDER 1 ON "DBA"."ROL_HUELLA"
 REFERENCING OLD AS old_name 
FOR EACH ROW /* WHEN( search_condition ) */
BEGIN

declare Campo VARCHAR(100);
declare Query VARCHAR(1000);
declare Valor_Anterior VARCHAR(800);
declare Valor_Nuevo VARCHAR(800);
declare ExisteCampo integer;

select count( column_name) INTO ExisteCampo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'ROL_HUELLA'); 

if(ExisteCampo>0) then
    begin
        declare Inserta_Bitacora dynamic scroll cursor for
        select column_name INTO Campo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'ROL_HUELLA');
        open Inserta_Bitacora;
        fetch next Inserta_Bitacora into Campo;
    
        if(@@sqlstatus = 2) then
          print "No se encontró ningun Registro";
          close Inserta_Bitacora;
          return
        end if;
    
        while(@@sqlstatus = 0) loop
        
        SET Query = 'SELECT old_name.' + Trim(Campo) +' INTO Valor_Anterior FROM DUMMY';
        EXECUTE IMMEDIATE Query;
        IF (Campo in ('ID_ROLHUE','NOMBRE_ROLHUE')) THEN 
                IF Campo ='ID_ROLHUE' THEN  
                    DELETE FROM DBA.USUARIOS_HUELLAS WHERE ID_ROLUSR=Valor_Anterior;
                END IF;
                INSERT INTO "DBA"."HISTO_HUELLAS" ("HISTO_TABLA","HISTO_IDENTIFICADOR","HISTO_CAMPO","HISTO_VANTERIOR","HISTO_VACTUAL","HISTO_ACCION","HISTO_USR_ACCION","HISTO_FECHA_ACCION","HISTO_INFO_ADICIONAL","HISTO_OBSERVACION")
                VALUES('ROL_HUELLA',old_name.ID_ROLHUE,Campo,Valor_Anterior,NULL,'D',current user,CURRENT TIMESTAMP ,(SELECT "DBA"."func_trae_info_sys"(3)),'Motivo: Gestion de roles, Eliminacion de rol');
            end if;
         fetch next Inserta_Bitacora into Campo;
           end loop;
           close Inserta_Bitacora;      
        end;
 
    
end if; 

END;
COMMENT ON TRIGGER "DBA"."ROL_HUELLA"."TGR_ONDELETE" IS 'AL ELIMNAR UN ROL';


CREATE TRIGGER "TRG_ONUPDATE" AFTER UPDATE 
ORDER 1 ON "DBA"."ROL_HUELLA"
REFERENCING OLD AS old_name NEW AS new_name
FOR EACH ROW /* WHEN( search_condition ) */
BEGIN

declare Campo VARCHAR(100);
declare Query VARCHAR(1000);
declare Valor_Anterior VARCHAR(800);
declare Valor_Nuevo VARCHAR(800);
declare ExisteCampo integer;
declare observacion VARCHAR(800);

select count(column_name) INTO ExisteCampo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'ROL_HUELLA'); 

if(ExisteCampo>0) then
    begin
        declare Inserta_Bitacora dynamic scroll cursor for
        select column_name INTO Campo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'ROL_HUELLA');
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
        IF (Campo in ('ID_ROLHUE','NOMBRE_ROLHUE','PERMISOS_ROLHUE')) THEN 
                if  Campo = 'PERMISOS_ROLHUE' THEN   
                       set observacion ='Se modificaron los permisos del rol';
                       SET Valor_Anterior = '(NO_DATA)'; 
                       SET Valor_Nuevo = '(NO_DATA)';  
                ELSE    
                        set observacion =''; 
                end if;
            

                INSERT INTO "DBA"."HISTO_HUELLAS" ("HISTO_TABLA","HISTO_CAMPO","HISTO_VANTERIOR","HISTO_VACTUAL","HISTO_ACCION","HISTO_USR_ACCION","HISTO_FECHA_ACCION","HISTO_INFO_ADICIONAL","HISTO_OBSERVACION")
                VALUES('ROL_HUELLA',Campo,Valor_Anterior,Valor_Nuevo,'U',current user,CURRENT TIMESTAMP ,(SELECT "DBA"."func_trae_info_sys"(3)),observacion);
        end if;
    END IF;
         fetch next Inserta_Bitacora into Campo;
           end loop;
           close Inserta_Bitacora;      
        end;
 
    
end if; 
	/* Type the trigger statements here */
END;
COMMENT ON TRIGGER "DBA"."ROL_HUELLA"."TRG_ONUPDATE" IS 'PARA UPDATE';
