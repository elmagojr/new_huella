@echo off
setlocal enabledelayedexpansion


set "directorio=%~dp0"

if not exist "%directorio%" (
    echo El directorio no existe.
    exit /b
)


set "archivosSql="
for /r "%directorio%" %%f in (*.sql) do (
    set "archivosSql=1"
    echo Ejecutando archivo: %%~nxf

    REM Ejecuta el archivo .sql utilizando dbisql
    "C:\Program Files\SQL Anywhere 12\Bin32\dbisql.exe" -c "DSN=SISC" "%%f" >nul 2>error.txt
)

REM Verifica si se encontraron archivos .sql
if not defined archivosSql (
    echo No se encontraron archivos .sql en el directorio.
)

pause
