# Convenções do projeto

## Limites de tamanho das entidades

- Todo limite de tamanho de uma propriedade de entidade deve ser declarado uma única vez no projeto `Domain`, na pasta e namespace `Domain.EntidadeConfiguracoes`.
- Crie uma classe estática por entidade, nomeada `{Entidade}Configuracoes`.
- Nomeie a constante como `{Propriedade}MaxLength` (por exemplo, `PessoaConfiguracoes.NomeMaxLength`).
- As validações de entrada e as configurações de tamanho das colunas no banco de dados devem reutilizar essa constante. Não duplique valores numéricos de tamanho em DTOs, validadores ou mapeamentos do Entity Framework.
- Ao adicionar ou alterar uma propriedade textual em uma entidade, crie ou atualize sua constante em `EntidadeConfiguracoes` e use-a em todas as camadas consumidoras.

## Criação de entidades

- Toda entidade concreta deve expor um método de fábrica estático chamado `Novo` para construir novos objetos.
- O método `Novo` deve inicializar os campos controlados pela aplicação, como identificador padrão para geração pelo banco, data de cadastro, data de atualização e estado inicial da entidade.
- Ao receber strings, o método `Novo` deve usar `Limitar` nos valores obrigatórios e `LimitarOuNull` nos valores opcionais.
- Os tamanhos passados para `Limitar` e `LimitarOuNull` devem vir da classe correspondente em `Domain.EntidadeConfiguracoes`; não use valores numéricos literais.
