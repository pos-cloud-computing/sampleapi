# GUIA SAMPLE API .NET 6 RODAR LOCAL

Template para criação de CRUD .net 6 para rodar em container.

## Installation

Suba o Banco Oracle no Docker.

```bash
docker run -d -p 49161:1521 -e ORACLE_ENABLE_XDB=true oracleinanutshell/oracle-xe-11g
```

Crie as tabelas de exemplos.

```bash
CREATE TABLE SAMPLE 
(
  ID_SAMPLE_API NUMBER NOT NULL 
, NM_SAMPLE VARCHAR(50) NOT NULL
, FL_ATIVO NUMBER(1) NOT NULL 
, CONSTRAINT SAMPLE_API_PK PRIMARY KEY 
  (
    ID_SAMPLE_API 
  )
 ,CONSTRAINT SAMPLE_API_NAME_PK UNIQUE  
  (
    NM_SAMPLE 
  )
  ENABLE 
);

COMMENT ON COLUMN SAMPLE.NM_SAMPLE IS 'Nome do Sample';
CREATE SEQUENCE SAMPLE_API_SEQUENCE;
```

## Usage

```python
Set as Startup Project => Web.API
```

## Contributing

Time desenvolvimento XPTO.

## License

Uso exclusivo da EMPRESA XPTO