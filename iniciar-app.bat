@echo off
title Inicializador da Aplicação ME.SuperHero.API

set DOCKER_PORT_API="7184"
set DOCKER_PORT_VUE="8080"
REM Abrir no navegador
timeout /t 2 > nul
start http://localhost:%DOCKER_PORT_API%/swagger/index.html
start http://localhost:%DOCKER_PORT_VUE%


echo Construindo imagens Docker...
docker-compose up --build

echo.
echo Aguarde a execução dos containers e acesse http://localhost:%DOCKER_PORT_API%
echo.
echo Pressione qualquer tecla para sair, ou deixe esta janela aberta.
pause