# Estrutura do projeto

Indicado abrir a solução para depuração no Visual Studio 2022 
## Padrões

**DDD
(Domain-Driven Design)**: O projeto é totalmente estruturado com o DDD. 

**S.O.L.I.D**: aplicado os princípios do SOLID como as injeções de dependências dos serviços e repositórios.

**JWT Role Token**: para validação de permissão das rotas de usuários Adm ou Comum

**IOptions**: captura os valores do appsettings.json implementando os valores nas classes onde pode ser controlado em variáveis de ambientes e secrets

**FluentValidator**: Para realizar as validações dos objetos recebidos na API

**Custom Exception**: Exceções personalizadas para o Domínio


## Testes
**Teste automatizado - Selenium**: Testes automatizados para realizar validações no front-end com Selenium Web Driver

**Teste Unitário - FluentAssertions**: Testes unitários criados para validar os serviços e aumento de cobertura para o SonarQube, aplicando o **Fixture** para criação de valores para o objeto