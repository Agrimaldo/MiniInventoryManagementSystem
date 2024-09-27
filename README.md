# MiniInventoryManagementSystem

## Introduction 
Projeto de teste para avaliação, API com Worker com Entity Framework, integrado com RabbitMQ e SQL Server;


## Getting Started
API, Worker - .NET Core versão 8.0 com EF;
RabbitMQ - padrão user guest;
Script_tables.sql - Estrutura das tabelas utilizadas no banco MiniInventoryManagementSystem (SQLServer);

## Build and Test
 "dotnet run --configuration Debug" ou Visual Studio;

 Test's : "dotnet test ./MiniInventoryManagementSystem.Test/MiniInventoryManagementSystem.Test.csproj"

 Obs.: Certificar que esteja ambos os projetos API e Worker estejam na inicialização;