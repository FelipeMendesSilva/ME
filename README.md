# 🦸‍♂️ Projeto SuperHero

Este projeto é uma solução completa de **cadastro de super-heróis**, composta por:

- **Front-end** em [Vue.js](https://vuejs.org/)  
- **API** em [.NET 8](https://dotnet.microsoft.com/)  
- **Banco de dados** [MySQL](https://www.mysql.com/)  
- Orquestração com **Docker** e **Docker Compose**

O projeto foi desenvolvido para demonstrar uma arquitetura moderna de aplicações web, integrando front-end, back-end e banco de dados em um ambiente containerizado, facilitando a execução e portabilidade.

---

## 📂 Estrutura de Pastas

Na raiz do projeto você encontrará:

├── iniciar-app          # Script para iniciar toda a solução
├── me.superhero.api          # Projeto da API em .NET 8
├── me.superhero.front      # Projeto do front-end em Vue.js
└── data                 # Pasta que guarda os dados do banco MySQL

Código

---

## 🚀 Como executar

1. Certifique-se de ter o **Docker** e **Docker Compose** instalados em sua máquina.  
   - [Instalar Docker](https://docs.docker.com/get-docker/)  

2. Na raiz do projeto, execute o script:

   ```bash
   ./iniciar-app
Aguarde o carregamento dos containers.

O tempo pode variar conforme sua máquina e rede.

Após a inicialização:

A página do Swagger da API será aberta automaticamente.

A página do Front-end Vue também será aberta no navegador.

🌐 Acesso às aplicações  
Swagger (API .NET 8) → documentação e testes dos endpoints.

Front-end Vue → interface para cadastro e consulta de super-heróis.

⚙️ Observações importantes  
É necessário aguardar o carregamento completo das aplicações antes de utilizá-las.

Todos os serviços rodam em containers Docker, sem necessidade de instalação manual de dependências.

Os dados persistidos ficam armazenados na pasta data (volume do MySQL).


