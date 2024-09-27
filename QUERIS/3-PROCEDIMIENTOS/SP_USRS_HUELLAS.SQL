CREATE PROCEDURE "DBA"."SP_USRS_HUELLAS"( 
IN @TIPO INTEGER,
IN @ID_ROLHUE VARCHAR(50),
IN @NOMBRE_ROLHUE VARCHAR(100),
IN @PERMISOS_ROLHUE VARCHAR(1024),
IN @USR_ROL VARCHAR(100),
OUT @@ESTADO INTEGER 
)
/* RESULT( column_name column_type, ... ) */
BEGIN
DECLARE @RowsAffected INT;
declare conteo int;
IF @TIPO = 0 THEN --nuevo rol
begin
    set conteo =(SELECT count(*) FROM DBA.ROL_HUELLA WHERE NOMBRE_ROLHUE = @NOMBRE_ROLHUE);
    if conteo >0 then
       
        SET @@ESTADO = -999; //ya existe

    else
    begin--
            INSERT INTO DBA.ROL_HUELLA (NOMBRE_ROLHUE,PERMISOS_ROLHUE,USR_AGREGO_ROLHUE) VALUES(@NOMBRE_ROLHUE,@PERMISOS_ROLHUE,@USR_ROL);
            SET @RowsAffected = @@ROWCOUNT;
            IF @RowsAffected > 0 THEN
                COMMIT;
                SET @@ESTADO = 1; -- registrado 
            ELSE
                ROLLBACK;
                SET @@ESTADO = -1; -- fallo
            END IF;
    end
end if;
select @@estado;
end;
END IF;
IF @TIPO =1 THEN --actualizar rol
  UPDATE DBA.ROL_HUELLA set NOMBRE_ROLHUE=@NOMBRE_ROLHUE,PERMISOS_ROLHUE=@PERMISOS_ROLHUE,FECHA_MODI_ROLHUE=CURRENT DATE,USR_MODI_ROLHUE=@USR_ROL WHERE ID_ROLHUE=@ID_ROLHUE;
     SET @RowsAffected = @@ROWCOUNT;
                IF @RowsAffected > 0 THEN
                    COMMIT;
                    SET @@ESTADO = 1; 
                ELSE
                    ROLLBACK;
                    SET @@ESTADO = -1; 
                END IF;
select @@estado;
END IF;
IF @TIPO =2 THEN//ELEMINA ROLL
DELETE FROM DBA.ROL_HUELLA WHERE ID_ROLHUE=@ID_ROLHUE;
 SET @RowsAffected = @@ROWCOUNT;
                IF @RowsAffected > 0 THEN
                    DELETE FROM DBA.USUARIOS_HUELLAS WHERE ID_ROLUSR=@ID_ROLHUE;
                    COMMIT;
                    SET @@ESTADO = 1; 
                ELSE
                    ROLLBACK;
                    SET @@ESTADO = -1; 
                END IF;
 select @@estado;

END IF;
//DE LOS PERMISOS PARA USUARIOS
IF @TIPO =3 THEN//CREA USUSARIO HUELLA
  set conteo =(SELECT count(*) FROM DBA.USUARIOS_HUELLAS WHERE NOMBRE_USR = @NOMBRE_ROLHUE);
    if conteo >0 then
       
        SET @@ESTADO = -999; //ya existe 

    else
        INSERT INTO DBA.USUARIOS_HUELLAS (ID_ROLUSR,NOMBRE_USR,TOKEN_ID,USR_CREADO_USR) VALUES(@ID_ROLHUE,@NOMBRE_ROLHUE,@PERMISOS_ROLHUE,@USR_ROL);
        SET @RowsAffected = @@ROWCOUNT;
                IF @RowsAffected > 0 THEN
                    COMMIT;
                    SET @@ESTADO = 1; 
                ELSE
                    ROLLBACK;
                    SET @@ESTADO = -1; 
                END IF;
    end if;
select @@ESTADO;
END IF;
IF @TIPO =4 THEN//ACTUALIZA USUARIOS 
UPDATE  DBA.USUARIOS_HUELLAS  SET  ID_ROLUSR =@ID_ROLHUE, FECHA_MODIFICADO_USR =CURRENT DATE, TOKEN_ID =@PERMISOS_ROLHUE, USR_MODI_USR =@USR_ROL WHERE  NOMBRE_USR=@NOMBRE_ROLHUE;
 SET @RowsAffected = @@ROWCOUNT;
                IF @RowsAffected > 0 THEN
                    COMMIT;
                    SET @@ESTADO = 1; 
                ELSE
                    ROLLBACK;
                    SET @@ESTADO = -1;
                END IF;
 select @@ESTADO;
END IF;


END;
COMMENT ON PROCEDURE "DBA"."SP_USRS_HUELLAS" IS 'SP PARA EL MANEJO DEL MODULO DE SEGURIDAD DEL ADDON DE HUELLAS (PERMISOS Y ROLES)';
