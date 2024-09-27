CREATE PROCEDURE "DBA"."sp_firmas_digitales"( /* [IN | OUT | INOUT] parameter_name parameter_type [DEFAULT default_value], ... */
in p_tipo int,
in p_codigo_cta varchar(100),
in p_compania int,
in p_firma long binary
 )
/* RESULT( column_name column_type, ... ) */
BEGIN
if p_tipo = 0 then --en caso de que sea la primera firma
    if not exists  (select * from dba.firmas_ahorro_copia where codigo_cta_aho = p_codigo_cta) then
        insert into dba.firmas_ahorro_copia (codigo_cta_aho,Codigo_COMPANIA,Firma1_aho) values (p_codigo_cta, p_compania, p_firma);
    else 
     raiserror 17000 'Esta cuenta ya tiene firma registrada';
    end if;    
end if;

if p_tipo = 1 then
    update dba.firmas_ahorro_copia set Firma1_aho =p_firma  where codigo_cta_aho = p_codigo_cta;
end if;
if p_tipo = 2 then
    update dba.firmas_ahorro_copia set Firma2_aho =p_firma  where codigo_cta_aho = p_codigo_cta;
end if;
if p_tipo = 3 then
    update dba.firmas_ahorro_copia set Firma3_aho =p_firma  where codigo_cta_aho = p_codigo_cta;
end if;
if p_tipo = 4 then
    update dba.firmas_ahorro_copia set Firma4_aho =p_firma  where codigo_cta_aho = p_codigo_cta;
end if;
	/* Type the procedure statements here */
END;
