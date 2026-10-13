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
- Ao consolidar uma regra importante de domínio, modelagem ou arquitetura durante uma tarefa, atualizar este `AGENTS.md` no mesmo trabalho para registrar a decisão.

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

## Consistência de empresa

- Todo `Usuario` pertence a uma única `Empresa` e deve possuir `EmpresaId`.
- O `EmpresaId` de `Usuario` deve ser o mesmo da `Pessoa` associada.
- O `EmpresaId` de `UsuarioErp` deve ser o mesmo de seu `Usuario` e de seu `UsuarioErpPerfil`.
- Essas invariáveis devem ser garantidas no banco por chaves estrangeiras compostas que incluam `EmpresaId`; não depender apenas de validações dos serviços.
- `Pessoa.EmpresaId` e `UsuarioErpPerfil.EmpresaId` devem possuir chave estrangeira direta para `Empresa.Id`, com exclusão restrita.
- Não configurar automaticamente uma chave estrangeira para `Empresa` em toda entidade derivada de `EmpresaBase`; relacionamentos com `Empresa` devem ser definidos explicitamente conforme a regra de cada entidade.
- As entidades principais dos relacionamentos compostos devem possuir chave alternativa por `{Id, EmpresaId}`.
- Estado inativo não permite inconsistência de empresa; `Ativo` não participa das chaves que garantem essas invariáveis.

## Login por contexto

- A autenticação deve partir da entidade de vínculo do contexto, e não diretamente de `Usuario`.
- O login do ERP deve consultar `UsuarioErp` como entidade raiz e, por meio dela, acessar `Usuario`, `Pessoa`, `Empresa`, `UsuarioErpPerfil` e os menus permitidos.
- A empresa do login ERP é determinada pelo vínculo `UsuarioErp`; não deve ser selecionada ou inferida diretamente a partir de `Usuario`.
- `Usuario` armazena a conta e suas credenciais, mas não determina isoladamente em qual contexto a conta pode entrar.
- Cada contexto deve possuir entidade de vínculo, fluxo e endpoint de login próprios. Por exemplo, o futuro endpoint `/ecommerce/login` deverá autenticar a partir de `UsuarioEcommerce`.
- Não reutilizar automaticamente no ERP regras de login, perfis, permissões, bloqueios ou dados de retorno pertencentes a outro contexto.
- O retorno do login ERP deve ser montado a partir do vínculo `UsuarioErp` e pode conter os dados da empresa, usuário, pessoa, perfil e menus autorizados desse vínculo.

# Bloqueios

Bloqueio é independente das entidades bloqueadas.

UsuarioBloqueio associa o bloqueio ao usuário.

Regras que determinam quando bloquear pertencem
aos respectivos contextos de negócio.
