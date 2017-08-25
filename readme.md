# 1912880934

Random repo, nothing interesting here

## Sobre o projeto

Este projeto trata-se de um exercício para atendimento ao desafio [Zx Ventures - Backend](https://github.com/ZXVentures/code-challenge/blob/master/backend.md). Trata-se de uma API Rest desenvolvida em .Net. 

## Instruções para execução/desenvolvimento

Abrir o projeto no Visual Studio e rodar (Ctrl + F5). Testei no Visual Studio Community 2017 e 2015. As dependências são baixadas automaticamente pelo Nuget. 

## Considerações

Por se tratar de uma API pequena, usei uma solução muito simples e sem perfumaria (no-bullshit approach). 

A API está implementada em ZX\Controllers\PdvController. Optei por fazer uma API que retorna 200 sempre. Operações todas retornam propriedades success e uma lista de erros.

Como mecanismo de persistência, para evitar maiores dificuldades para executar e avaliar o projeto, usei o HttpRuntime.Cache nativo para implementar um banco de dados em memória.

Implementei testes em cima do recurso de criação de PDV, para enumerar como faria cenários de validação em casos de falha e sucesso. Não estendi em muito a cobertura.

Para resolver o problema da identificação do PDV mais próximo, criei uma classe para abrigar a lógica referente à Geolocalização em Zx\Utils\GeoUtils. A lógica foi adaptada de problemas similares identificados no StackOverflow.

No mais, à disposição.