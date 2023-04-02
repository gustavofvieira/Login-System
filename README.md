# Primeiros passos:
## Instalação
**Docker**: para a instalação do banco de dados NoSQL **MongoDB**

**.Net 6**: para execução da depuração.

**Angular**: para execução do front-end


## Banco
Devem ser criadas duas collections no banco de dados, a **Collection Users** para usuários cadastrados e a **Collection RecoverPasswords** para guardar as requisições de mudança de senhas
a aplicação contém validação de rota por JWT, contendo dois Roles 'Adm' e 'Common', caso queria um usuário Adm grave no banco de dados o seguinte objeto:

**Collection Users**
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


## Aplicação

Aplique os valores das configurações, o **AuthSmtp** deve ser aplicado os valores do host, porta, email e senha.
```
"AuthSmtp": {
    "SmtpPort": 587,
    "SmtpHost": "smtp.office365.com",
    "Email": "",
    "Password": ""
  }
```
O **FrontService** é para ser aplicado o endereço da aplicação do front, e setado as rotas, neste caso a rota de atualização da senha do link enviado pelo e-mail
```
"FrontService": {
    "Host": "http://localhost:4200/",
    "UpdatePassword":  "updatePassword"
  }
```