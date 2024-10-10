
CREATE FUNCTION "DBA"."func_trae_info_sys"( in accion INTEGER )
RETURNS VARCHAR(200)
DETERMINISTIC
BEGIN
 DECLARE "data_sys" VARCHAR(200);
    
 DECLARE resultado LONG VARCHAR;
 DECLARE  archivo varchar(200);
 DECLARE exe_info varchar(200);
 DECLARE EXISTE INTEGER ;
    

 set archivo ='C:\SISC\Addons\DDigital\info.txt';
 set exe_info ='C:\SISC\Addons\DDigital\INFO_SYS.exe';
 SET EXISTE = (select count(*) from sys.SYSTABLE where table_name ='temporal_info_sys');
   
if accion = 0 THEN      
            CALL xp_cmdshell('C:\SISC\Addons\DDigital\INFO_SYS.exe','no_output' );    
             if EXISTE = 0 then 
                    BEGIN 
                        create global temporary TABLE  temporal_info_sys(
                        usr varchar(50),
                        informacion VARCHAR (500) 
                        ) on commit preserve rows;
                    END 
             end IF; 
 SET EXISTE = (select count(*) from temporal_info_sys where usr = current user);
        if EXISTE =0 THEN 
             insert into temporal_info_sys (usr,informacion) VALUES  (current user,(SELECT CAST(xp_read_file('C:\SISC\Addons\info.txt') AS VARCHAR)));         
        else 
             UPDATE temporal_info_sys set informacion =(SELECT CAST(xp_read_file('C:\SISC\Addons\info.txt') AS VARCHAR)) where usr = current user;
        end IF ;             
                 
 call xp_cmdshell('DEL "C:\\SISC\\Addons\\info.txt"','no_output');
 set data_sys = (SELECT informacion FROM  temporal_info_sys where usr = current user); 
end if; 

if accion = 1 then
    set data_sys = (SELECT informacion FROM  temporal_info_sys where usr = current user); 
end if;

if accion = 3 THEN

           CALL xp_cmdshell('C:\SISC\Addons\DDigital\INFO_SYS.exe','no_output' );             
           set data_sys =(SELECT CAST(xp_read_file('C:\SISC\Addons\info.txt') AS VARCHAR)); 
           call xp_cmdshell('DEL "C:\\SISC\\Addons\\info.txt"','no_output');
end if; 

	/* Type the function statements here */
	RETURN "data_sys";
END;
COMMENT ON PROCEDURE "DBA"."func_trae_info_sys" IS 'esta funcion devuelve un una cadena con informacion del sistema';






CREATE PROCEDURE "DBA"."sp_firmas_digitales"(
    in p_tipo varchar(50),
    in p_codigo_cta varchar(100),
    in p_compania varchar(50),
    in obs VARCHAR(200),
    in p_firma long binary
)
BEGIN
 DECLARE @RowsAffected INT;
if p_tipo = 0 then --en caso de que sea la primera firma
    if not exists  (select * from dba.firmas_ahorro where codigo_cta_aho = p_codigo_cta) then
        insert into dba.firmas_ahorro (codigo_cta_aho,Codigo_COMPANIA,Firma1_aho) values (p_codigo_cta, p_compania, p_firma);
    else 
     raiserror 17000 'Esta cuenta ya tiene firma registrada';
    end if;    
end if;

        IF p_tipo = 1 THEN
            if not exists  (select * from dba.firmas_ahorro where codigo_cta_aho = p_codigo_cta) then
                insert into dba.firmas_ahorro (codigo_cta_aho,Codigo_COMPANIA,Firma1_aho) values (p_codigo_cta, p_compania, p_firma);
            else 
                UPDATE dba.firmas_ahorro 
                SET Firma1_aho = p_firma, observacion = obs 
                WHERE codigo_cta_aho = p_codigo_cta;
            end if;          
        ELSEIF p_tipo = 2 THEN
            if not exists  (select * from dba.firmas_ahorro where codigo_cta_aho = p_codigo_cta) then
                insert into dba.firmas_ahorro (codigo_cta_aho,Codigo_COMPANIA,Firma1_aho,Firma2_aho) values (p_codigo_cta, p_compania,'', p_firma);
            else 
                UPDATE dba.firmas_ahorro 
                SET Firma2_aho = p_firma, observacion = obs 
                WHERE codigo_cta_aho = p_codigo_cta;
            end if;
        ELSEIF p_tipo = 3 THEN
            if not exists  (select * from dba.firmas_ahorro where codigo_cta_aho = p_codigo_cta) then
                insert into dba.firmas_ahorro (codigo_cta_aho,Codigo_COMPANIA,Firma1_aho,Firma3_aho) values (p_codigo_cta, p_compania,'', p_firma);
            else 
                UPDATE dba.firmas_ahorro 
                SET Firma3_aho = p_firma, observacion = obs 
                WHERE codigo_cta_aho = p_codigo_cta;
            end if;
        ELSEIF p_tipo = 4 THEN
            if not exists  (select * from dba.firmas_ahorro where codigo_cta_aho = p_codigo_cta) then
                insert into dba.firmas_ahorro (codigo_cta_aho,Codigo_COMPANIA,Firma1_aho,Firma4_aho) values (p_codigo_cta, p_compania,'', p_firma);
            else 
                UPDATE dba.firmas_ahorro 
                SET Firma4_aho = p_firma, observacion = obs 
                WHERE codigo_cta_aho = p_codigo_cta;
            end if;
        END IF;

        IF p_tipo = 0 THEN
                 
            INSERT INTO dba.firmas_ahorro (codigo_cta_aho, Codigo_COMPANIA,CODIGO_FILIAL, Firma1_aho, observacion) 
            VALUES (p_codigo_cta, p_compania,1 ,p_firma, obs);
     set @RowsAffected =@@ROWCOUNT;
    

        END IF;

END;



CREATE FUNCTION "DBA"."comprobar_aditamentos"( in @string_nombre_objeto varchar(512) )
RETURNS NUMERIC(1,0)
DETERMINISTIC
BEGIN
	DECLARE "comprovado" NUMERIC(1,0);
	IF EXISTS (select * from sys.SYSTABLE where table_name =@string_nombre_objeto ) THEN 
       set comprovado = 1;
    elseIF EXISTS (select * from sys.SYSUSER where user_name =@string_nombre_objeto ) THEN
        set comprovado = 1;
    elseif EXISTS (select * from sys.SYSPROCEDURE where proc_name =@string_nombre_objeto ) THEN
     set comprovado = 1;
    elseif EXISTS (select *  from sys.SYSTRIGGERS where trigname =@string_nombre_objeto ) THEN
        set comprovado = 1;
    elseif EXISTS (select * from dba.PARAMETROS_X_EMPRESA where PA_ABREV_PARAMETRO =@string_nombre_objeto ) THEN
        set comprovado = 1;
    else
        set comprovado = 0;
    end if;

	RETURN "comprovado";
END;
COMMENT ON PROCEDURE "DBA"."comprobar_aditamentos" IS 'esta funcion sirve para determinar que un objetos en la base de datos exista';
