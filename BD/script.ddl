-- Generado por Oracle SQL Developer Data Modeler 19.2.0.182.1216
--   en:        2019-08-12 10:44:11 CST
--   sitio:      SQL Server 2012
--   tipo:      SQL Server 2012



CREATE TABLE piloto (
    id            INTEGER NOT NULL,
    nombre        VARCHAR(50),
    telefono      VARCHAR(20),
    marca_carro   VARCHAR(20),
    linea_carro   VARCHAR(20),
    placa_carro   VARCHAR(20)
)

go

ALTER TABLE Piloto ADD constraint carro_pk PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) go

CREATE TABLE usuario (
    id         INTEGER NOT NULL,
    nombre     VARCHAR(50),
    telefono   VARCHAR(20),
    usr        VARCHAR(20),
    pasword    VARCHAR(20)
)

go

ALTER TABLE Usuario ADD constraint usuario_pk PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) go

CREATE TABLE viaje (
    id           INTEGER NOT NULL,
    fecha        DATE NOT NULL,
    origen       VARCHAR(50),
    destino      VARCHAR(50),
    activo       bit,
    usuario_id   INTEGER NOT NULL,
    piloto_id    INTEGER NOT NULL
)

go

ALTER TABLE Viaje ADD constraint viaje_pk PRIMARY KEY CLUSTERED (id, fecha)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON ) go

ALTER TABLE Viaje
    ADD CONSTRAINT viaje_piloto_fk FOREIGN KEY ( piloto_id )
        REFERENCES piloto ( id )
ON DELETE NO ACTION 
    ON UPDATE no action go

ALTER TABLE Viaje
    ADD CONSTRAINT viaje_usuario_fk FOREIGN KEY ( usuario_id )
        REFERENCES usuario ( id )
ON DELETE NO ACTION 
    ON UPDATE no action go



-- Informe de Resumen de Oracle SQL Developer Data Modeler: 
-- 
-- CREATE TABLE                             3
-- CREATE INDEX                             0
-- ALTER TABLE                              5
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE DATABASE                          0
-- CREATE DEFAULT                           0
-- CREATE INDEX ON VIEW                     0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE ROLE                              0
-- CREATE RULE                              0
-- CREATE SCHEMA                            0
-- CREATE SEQUENCE                          0
-- CREATE PARTITION FUNCTION                0
-- CREATE PARTITION SCHEME                  0
-- 
-- DROP DATABASE                            0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0
