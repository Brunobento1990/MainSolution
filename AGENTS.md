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

# Regras do projeto

- Respeitar os padrões existentes.
- Não gerar migrations sem autorização.
- Não criar testes sem solicitação.
- Não adicionar propriedades ou entidades sem necessidade.
- Não modificar arquivos fora do escopo solicitado.
- Não alterar referências entre projetos sem autorização explícita.
- Para gerar uma migration, usar `dotnet ef migrations add {NomeDaMigration} --project Migrations --startup-project Api --context AppDbContext`; executar somente com autorização explícita.
- Para aplicar migrations ao banco, usar `dotnet ef database update --project Migrations --startup-project Api --context AppDbContext`; executar somente com autorização explícita.
- Na dúvida sobre uma regra de negócio, perguntar.
- Não presumir que entidades semelhantes possuem as mesmas regras.

## Procedimento antes de alterar código

- Reler este `AGENTS.md` no início de cada nova tarefa no repositório e antes de decisões arquiteturais.
- Quando houver mais de uma interpretação possível, declarar a interpretação e pedir confirmação antes de editar arquivos.
- Não preencher lacunas de regra de negócio por inferência, conveniência técnica ou semelhança com outro projeto.
- Antes do build, revisar todas as alterações contra cada regra aplicável deste arquivo; corrigir as violações antes de continuar.
- Ao corrigir um problema, revisar o fluxo completo afetado, não apenas o trecho apontado.

# Identidade e autenticação

Pessoa representa dados cadastrais.

Usuario representa uma conta de autenticação.

Uma Pessoa pode possuir múltiplos Usuarios,
com credenciais independentes.

UsuarioErp representa o vínculo de uma conta
com o ERP de determinada empresa.

Alterações cadastrais em Pessoa não devem
modificar credenciais de Usuario.

# Bloqueios

Bloqueio é independente das entidades bloqueadas.

UsuarioBloqueio associa o bloqueio ao usuário.

Regras que determinam quando bloquear pertencem
aos respectivos contextos de negócio.
