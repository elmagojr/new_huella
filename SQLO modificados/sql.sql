begin 
    DECLARE  accion_ int;
    DECLARE  cadena_ varchar(200);
    set cadena_ ='ff';
    set accion_ = 2;
    if accion_ = 1 then
    if not EXISTS  (select * from dba.PARAMETROS_X_EMPRESA where PA_ABREV_PARAMETRO = 'DEDO_MANO_ACTIVO') THEN 
    UPDATE  dba.PARAMETROS_X_EMPRESA set PA_VALOR =cadena_ where PA_ABREV_PARAMETRO = 'DEDO_MANO_ACTIVO';
    ELSE 
    RAISERROR 17000 'Parametro de Seleccion de manos no invocado. Favor contacte al personal de soporte';
    end if;    
    ELSEIF   accion_ = 2 THEN 
    select PA_VALOR from dba.PARAMETROS_X_EMPRESA where PA_ABREV_PARAMETRO = 'DEDO_MANO_ACTIVO';
    end if; 
end

call dba.sp_select_mano(2,'mano:DI;dedos:DD1,DD2,DD3,DD4,DD5,DI1,DI2,DI3,DI4,DI5')

call dba.SP_BUSCAR_PERSONA('1613198601087',2)