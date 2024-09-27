CREATE PROCEDURE "DBA"."SP_FIRMAS_X_CUENTA"(
    IN @_TIPO_TRANSACCION integer,
    IN @FXC_IDENTIDAD VARCHAR (100), 
    IN @FXC_CTA_AHO VARCHAR (100) ,
    IN @FXC_TIPO_PERSONA VARCHAR (50),  
    IN @FXC_MANCOMUNADA integer, 
    IN @FXC_AGREGO VARCHAR (50),
    IN @FXC_FECHA_AGREGO datetime,  
    IN @FXC_MOD VARCHAR (50), 
    IN @FXC_FECHA_MOD datetime ,
    IN @FXC_IDEN_DEF VARCHAR (100),  
    IN @FXC_COMPANIA INTEGER,
    IN @FXC_FILIAL INTEGER,
    IN @_IDENTIFICADOR BIGINT 
    
     /* [IN | OUT | INOUT] parameter_name parameter_type [DEFAULT default_value], ... */ )
    /* RESULT( column_name column_type, ... ) */
    BEGIN
     DECLARE @RowsAffected INT;
    IF @_TIPO_TRANSACCION = 0 THEN --consulta
     
    ENDIF ;
    IF @_TIPO_TRANSACCION = 1 THEN --para inserts
    
    INSERT INTO "DBA"."FIRMAS_X_CUENTA" (      
            "FXC_IDENTIDAD",
            "FXC_CTA_AHO",
            "FXC_TIPO_PERSONA",    
            "FXC_AGREGO",
            "FXC_FECHA_AGREGO",       
            "FXC_IDEN_DEF",   
            "FXC_COMPANIA",
            "FXC_FILIAL"
    
        )
    VALUES(     
            @FXC_IDENTIDAD,
            @FXC_CTA_AHO,
            @FXC_TIPO_PERSONA,      
            @FXC_AGREGO,
            @FXC_FECHA_AGREGO, 
            @FXC_IDEN_DEF,
            @FXC_COMPANIA,
            @FXC_FILIAL
        );
    set @RowsAffected =@@ROWCOUNT;
    
     IF @RowsAffected > 0 THEN        
         COMMIT;
    select @RowsAffected;
     ELSE   
         ROLLBACK;
     END IF;
    
    END IF;
        
    IF @_TIPO_TRANSACCION = 2 THEN --para update
    
    UPDATE "DBA"."FIRMAS_X_CUENTA"
    SET     
        "FXC_MANCOMUNADA" = @FXC_MANCOMUNADA,
        "FXC_MOD" = @FXC_MOD,
        "FXC_FECHA_MOD" = @FXC_FECHA_MOD
    WHERE FXC_SEQ= @_IDENTIFICADOR;
    set @RowsAffected =@@ROWCOUNT;
    
     IF @RowsAffected > 0 THEN        
         COMMIT;
        select @RowsAffected;
     ELSE   
         ROLLBACK;
     END IF;

    END IF; 
end;
