# 1912880934

Random repo, nothing interesting here

## Sobre o projeto

Este projeto trata-se de um exerc�cio para atendimento ao desafio [Zx Ventures - Backend](https://github.com/ZXVentures/code-challenge/blob/master/backend.md). Trata-se de uma API Rest desenvolvida em .Net. 

## Instru��es para execu��o/desenvolvimento

Abrir o projeto no Visual Studio e rodar (Ctrl + F5). Testei no Visual Studio Community 2017 e 2015. As depend�ncias s�o baixadas automaticamente pelo Nuget. 

## Considera��es

Por se tratar de uma API pequena, usei uma solu��o muito simples e sem perfumaria (no-bullshit approach). 

A API est� implementada em ZX\Controllers\PdvController. Optei por fazer uma API que retorna 200 sempre. Opera��es todas retornam propriedades success e uma lista de erros.

Como mecanismo de persist�ncia, para evitar maiores dificuldades para executar e avaliar o projeto, usei o HttpRuntime.Cache nativo para implementar um banco de dados em mem�ria.

Implementei testes em cima do recurso de cria��o de PDV, para enumerar como faria cen�rios de valida��o em casos de falha e sucesso. N�o estendi em muito a cobertura.

Para resolver o problema da identifica��o do PDV mais pr�ximo, criei uma classe para abrigar a l�gica referente � Geolocaliza��o em Zx\Utils\GeoUtils. A l�gica foi adaptada de problemas similares identificados no StackOverflow.

No mais, � disposi��o.