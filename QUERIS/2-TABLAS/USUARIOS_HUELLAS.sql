CREATE TABLE "DBA"."USUARIOS_HUELLAS" (
	"ID_USR" INTEGER NOT NULL DEFAULT AUTOINCREMENT,
	"ID_ROLUSR" INTEGER NOT NULL,
	"NOMBRE_USR" VARCHAR(50) NOT NULL,
	"FECHA_CREADO_USR" "datetime" NULL DEFAULT CURRENT TIMESTAMP,
	"FECHA_MODIFICADO_USR" "datetime" NULL,
	"TOKEN_ID" VARCHAR(512) NULL,
	"USR_CREADO_USR" VARCHAR(50) NULL,
	"USR_MODI_USR" VARCHAR(50) NULL,
	PRIMARY KEY ( "ID_USR" ASC )
) IN "system";
COMMENT ON TABLE "DBA"."USUARIOS_HUELLAS" IS 'USUARIOS PARA LA HUELLA';
COMMENT ON COLUMN "DBA"."USUARIOS_HUELLAS"."ID_ROLUSR" IS 'rol del usr asignado a este usuario ';
COMMENT ON COLUMN "DBA"."USUARIOS_HUELLAS"."NOMBRE_USR" IS 'nombre del usuario ';
COMMENT ON COLUMN "DBA"."USUARIOS_HUELLAS"."TOKEN_ID" IS 'token que valida que los permisos y el acceso al add on';


CREATE TRIGGER "TRG_DELETE_USR" BEFORE DELETE
ORDER 1 ON "DBA"."USUARIOS_HUELLAS"
 REFERENCING OLD AS old_name 
FOR EACH ROW /* WHEN( search_condition ) */
BEGIN
        declare Campo VARCHAR(100);
        declare Query VARCHAR(1000);
        declare Valor_Anterior VARCHAR(800);
        declare Valor_Nuevo VARCHAR(800);
        declare ExisteCampo integer;
        
        select count( column_name) INTO ExisteCampo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'USUARIOS_HUELLAS'); 
        
        if(ExisteCampo>0) then
            begin
                declare Inserta_Bitacora dynamic scroll cursor for
                select column_name INTO Campo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'USUARIOS_HUELLAS');
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
                IF (Campo in ('ID_USR','NOMBRE_USR')) THEN 
                        INSERT INTO "DBA"."HISTO_HUELLAS" ("HISTO_TABLA","HISTO_IDENTIFICADOR","HISTO_CAMPO","HISTO_VANTERIOR","HISTO_VACTUAL","HISTO_ACCION","HISTO_USR_ACCION","HISTO_FECHA_ACCION","HISTO_INFO_ADICIONAL","HISTO_OBSERVACION")
                        VALUES('USUARIOS_HUELLAS',old_name.ID_USR,Campo,Valor_Anterior,'(NO_DATA)','D',current user,CURRENT TIMESTAMP ,(SELECT "DBA"."func_trae_info_sys"(3)),'Motivo: Gestion de  USUARIOS HUELLA, Quitar rol');
                    end if;
                 fetch next Inserta_Bitacora into Campo;
                   end loop;
                   close Inserta_Bitacora;      
                end;
         
            
        end if; 
END;
COMMENT ON TRIGGER "DBA"."USUARIOS_HUELLAS"."TRG_DELETE_USR" IS 'PARA QUITAR ROL DE UN USUARIO DE HUELLA';


CREATE TRIGGER "TRG_UPDATE_USR" AFTER UPDATE
ORDER 1 ON "DBA"."USUARIOS_HUELLAS"
 REFERENCING OLD AS old_name NEW AS new_name 
FOR EACH ROW /* WHEN( search_condition ) */
BEGIN
    declare Campo VARCHAR(100);
    declare Query VARCHAR(1000);
    declare Valor_Anterior VARCHAR(800);
    declare Valor_Nuevo VARCHAR(800);
    declare ExisteCampo integer;
    declare observacion VARCHAR(800);
    
    select count(column_name) INTO ExisteCampo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'USUARIOS_HUELLAS'); 
        
    if(ExisteCampo>0) then
        begin
            declare Inserta_Bitacora dynamic scroll cursor for
            select column_name INTO Campo from sys.SYSCOLUMN where table_id=(SELECT table_id FROM SYS.SYSTABLE WHERE table_name = 'USUARIOS_HUELLAS');
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
            IF (Campo in ('ID_ROLUSR','NOMBRE_USR','ID_USR', 'TOKEN_ID')) THEN 
                    if  Campo = 'ID_ROLUSR' THEN   
                           set observacion ='Se cambio de rol';                                     
                    ELSEIF Campo = 'TOKEN_ID'   THEN 
                            set observacion ='Se cambio la informacion de acceso'; 
                            SET Valor_Anterior = '(NO_DATA)'; 
                            SET Valor_Nuevo = '(NO_DATA)';  
                    ELSE
                             set observacion =''; 
                    end if;
                
    
                    INSERT INTO "DBA"."HISTO_HUELLAS" ("HISTO_TABLA","HISTO_IDENTIFICADOR","HISTO_CAMPO","HISTO_VANTERIOR","HISTO_VACTUAL","HISTO_ACCION","HISTO_USR_ACCION","HISTO_FECHA_ACCION","HISTO_INFO_ADICIONAL","HISTO_OBSERVACION")
                    VALUES('USUARIOS_HUELLAS',old_name.ID_USR,Campo,Valor_Anterior,Valor_Nuevo,'U',current user,CURRENT TIMESTAMP ,(SELECT "DBA"."func_trae_info_sys"(3)),observacion);
            end if;
        END IF;
             fetch next Inserta_Bitacora into Campo;
               end loop;
               close Inserta_Bitacora;      
            end;
     
        
    end if; 
END;
COMMENT ON TRIGGER "DBA"."USUARIOS_HUELLAS"."TRG_UPDATE_USR" IS 'par update  usr';

