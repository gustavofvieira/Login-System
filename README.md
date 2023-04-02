# Primeiros passos:
## Instalação
**Docker**: para a instalação do banco de dados NoSQL **MongoDB**

**.Net 6**: para execução da depuração.

**Angular**: para execução do front-end


## Banco

a aplicação contém validação de rota por JWT, contendo dois Roles 'Adm' e 'Common', caso queria um usuário Adm grave no banco de dados o seguinte objeto:
```
{
    "_id" : LUUID("645FA83F-1757-6245-B3FC-2C963F66AFA5"),
    "Name" : "Your Name",
    "EmailAddress" : "your_email@domain.com",
    "Password" : "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", //senha criptografada, valor 123456 
    "Role" : "Adm",
    "CreatedAt" : new Date()
}
```
