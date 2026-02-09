# Aventuras Digitais Matemáticas

**Jogo educativo 2D desenvolvido em Unity** que utiliza o conceito do **Material Dourado** (Maria Montessori) para ensinar operações aritméticas a crianças de forma interativa e lúdica.

O jogador manipula blocos de unidade (1), dezena (10) e centena (100) por meio de **drag and drop**, construindo representações numéricas, resolvendo adições e subtrações — tudo com feedback visual e sonoro em tempo real.

---

## Sumário

- [Sobre o Projeto](#sobre-o-projeto)
- [Funcionalidades](#funcionalidades)
- [Telas e Navegação](#telas-e-navegação)
- [Modos de Jogo](#modos-de-jogo)
- [Sistema de Níveis](#sistema-de-níveis)
- [Sistemas de Recompensa e Progressão](#sistemas-de-recompensa-e-progressão)
- [Login e Perfil do Jogador](#login-e-perfil-do-jogador)
- [Arquitetura Técnica](#arquitetura-técnica)
- [Tecnologias, Linguagens e Ferramentas](#tecnologias-linguagens-e-ferramentas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Como Executar](#como-executar)
- [Design e Prototipação](#design-e-prototipação)

---

## Sobre o Projeto

O **Aventuras Digitais Matemáticas** foi desenvolvido como Trabalho de Conclusão de Curso (TCC) com o objetivo de oferecer uma ferramenta digital que auxilie no ensino de matemática básica para crianças do ensino fundamental.

A proposta pedagógica é baseada no **Material Dourado Montessori**, um recurso manipulativo amplamente utilizado em escolas para tornar conceitos abstratos de numeração mais concretos e acessíveis. O jogo digitaliza essa experiência, permitindo que o aluno interaja com blocos virtuais que representam unidades, dezenas e centenas.

### Principais diferenciais

- **Abordagem pedagógica fundamentada** — baseada no Material Dourado, recurso validado e usado por educadores em todo o Brasil
- **Dois sistemas numéricos** — suporte a números naturais e números romanos em modos independentes
- **Progressão gamificada** — sistema completo de estrelas, medalhas, missões, emblemas e ranking que incentivam o engajamento
- **Feedback sensorial** — dicas sonoras direcionais orientam o aluno durante a resolução ("adicione blocos" ou "remova blocos")
- **Mecânica de agrupamento e decomposição** — ao acumular 10 unidades, o jogo automaticamente as agrupa em uma dezena, reforçando o conceito de valor posicional. Para subtração, blocos maiores são decompostos (1 centena vira 10 dezenas)
- **Modo livre para experimentação** — além dos níveis estruturados, há um modo sandbox para exploração espontânea
- **Design responsivo** — interface adaptável a diferentes resoluções de tela

---

## Funcionalidades

| Funcionalidade | Descrição |
|----------------|-----------|
| Drag and Drop | Arrastar e soltar blocos base-10 nas áreas de resposta |
| Agrupamento automático | 10 unidades se transformam em 1 dezena; 10 dezenas em 1 centena |
| Decomposição | Centena se decompõe em 10 dezenas; dezena em 10 unidades (para subtração) |
| Dicas sonoras | Áudio contextual indicando se o jogador deve adicionar ou remover blocos |
| Conversão para romanos | Algoritmo que converte números de 1 a 999 para algarismos romanos |
| Geração procedural | Números gerados aleatoriamente sem repetição dentro de cada nível |
| Sistema de fases | 3 fases por nível com 4 tentativas cada |
| Sistema de estrelas | Até 3 estrelas por nível baseado no desempenho |
| Ranking local | Classificação dos 3 melhores jogadores por pontuação |
| Missões e emblemas | 9 missões organizadas em 3 blocos com emblemas colecionáveis |
| 10 medalhas | Desbloqueadas conforme o jogador acumula XP |
| Efeito de confetti | Celebração visual ao completar desafios |
| Múltiplos perfis | Suporte a diversos jogadores com progresso independente |

---

## Telas e Navegação

O jogo é composto por **16 cenas ativas**, cada uma com função específica dentro do fluxo de navegação:

```
                              +-----------+
                              |  Início   |
                              |  (Login)  |
                              +-----+-----+
                                    |
            +-----------+-----------+-----------+-----------+
            |           |           |           |           |
       +----v----+ +----v----+ +----v----+ +----v----+ +----v----+
       | Opções  | |Naturais | |Romanos  | |Aquisições| |  Help  |
       |(M.Livre)| |(Níveis) | |(Níveis) | |         | |        |
       +----+----+ +----+----+ +----+----+ +----+----+ +--------+
            |           |           |           |
       +----v----+ +----v----+ +----v------+ +--+------+------+
       |Seleção /| |  Mat    | |    Mat    | |Conquist.|Classi.|
       |Operação | |Dourado 2| |Dourado   | |  (Ach.) | (Rank)|
       +----+----+ +----+----+ |Romanos   | +----+----+-------+
            |           |      +----+------+      |
       +----v----+ +----v----+      |         +---v----+
       |  Mat    | | Placar  |<-----+         |Créditos|
       |Dourado  | |(Result.)|                +--------+
       |(M.Livre)| +----+----+
       +---------+      |
                   +----v----+     +----------+
                   |Medalhas |---->| Missões  |
                   |(Notify) |     | (Notify) |
                   +---------+     +----------+
```

### Detalhamento das telas

| Tela | Cena Unity | Descrição |
|------|------------|-----------|
| **Início** | `Inicio.unity` | Menu principal com login/cadastro, botões para todos os modos e seções |
| **Opções** | `Opcoes.unity` | Hub do modo livre — acesso por Seleção (representar) ou Operação (calcular) |
| **Seleção** | `selecao.unity` | Entrada de um número para representar com blocos no modo livre |
| **Operação** | `op2.unity` | Entrada de dois operandos e escolha de operação (+/-) no modo livre |
| **Mat. Dourado** | `MatDourado.unity` | Tela de jogo do modo livre com blocos arrastáveis |
| **Naturais** | `Naturais.unity` | Grade de seleção dos 12 níveis de números naturais (2 páginas) |
| **Mat. Dourado 2** | `MatDourado2.unity` | Tela de jogo dos níveis de números naturais |
| **Romanos** | `Romanos.unity` | Grade de seleção dos 10 níveis de números romanos (2 páginas) |
| **Mat. Dourado Romanos** | `MatDouradoRomanos.unity` | Tela de jogo dos níveis de números romanos |
| **Aquisições** | `Aquisicoes.unity` | Hub para Conquistas e Classificação |
| **Conquistas** | `Conquistas.unity` | Exibe medalha atual, pontuação total e emblemas conquistados |
| **Classificação** | `Classificacao.unity` | Ranking dos 3 melhores jogadores com medalhas |
| **Missões** | `Missoes.unity` | Painel de 3 blocos de missões com progresso, áudio e emblemas |
| **Ajuda** | `Help.unity` | Instruções de como jogar |
| **Créditos** | `Creditos.unity` | Equipe e financiamento |
| **Sistemas** | `Sistemas.unity` | Seleção entre Naturais e Romanos |

---

## Modos de Jogo

### 1. Números Naturais (12 Níveis)

O jogador progride por níveis que introduzem conceitos de forma gradual:

| Níveis | Tipo | Descrição |
|--------|------|-----------|
| 1 – 3 | Representação | Representar um número usando blocos (unidades, dezenas, centenas) |
| 4 – 6 | Adição | Montar o resultado de uma soma com os blocos |
| 7 – 9 | Subtração | A partir de blocos pré-dispostos, remover para obter o resultado |
| 10 – 12 | Misto | Sorteia aleatoriamente entre representação, adição ou subtração |

Cada nível possui **3 fases** com números gerados aleatoriamente (sem repetição). O jogador tem **4 tentativas por fase**. Ao acertar as 3 fases, completa o nível e desbloqueia o próximo.

### 2. Números Romanos (10 Níveis)

Variante que trabalha a conversão entre números decimais e romanos:

| Níveis | Faixa | Exemplos |
|--------|-------|----------|
| 1 – 3 | Unidades (1-9) | I, IV, VII |
| 4 – 6 | Dezenas (10-90) | X, XL, LXX |
| 7 – 9 | Centenas (100-900) | C, CD, DCC |
| 10 | Misto (1-999) | CDXLIV, DCCCXCI |

O jogador vê o número em algarismos romanos e deve representá-lo com blocos base-10.

### 3. Modo Livre

Dois sub-modos para experimentação sem pressão de nível:

- **Seleção** — o jogador digita um número (ex: 347) e deve representá-lo com blocos
- **Operação** — o jogador escolhe dois números e uma operação (+/-), depois monta o resultado

---

## Sistema de Níveis

### Mecânica por Fase

```
Nível N
├── Fase 1 → Número gerado aleatoriamente → 4 tentativas
├── Fase 2 → Novo número (sem repetir) → 4 tentativas
└── Fase 3 → Novo número (sem repetir) → 4 tentativas
                                            │
                                    Acertou 3 fases?
                                    ├── Sim → Placar + Estrelas + Próx. nível desbloqueado
                                    └── Não (tentativas esgotadas) → Placar + Tentar novamente
```

### Feedback durante o jogo

- **Resposta correta (fase intermediária)** — som de acerto, nova fase gerada
- **Resposta correta (fase 3)** — som de vitória, confetti, placar de resultados
- **Resposta incorreta** — dica sonora direcional:
  - Valor montado **menor** que o esperado → "Adicione mais blocos"
  - Valor montado **maior** que o esperado → "Remova blocos"

### Bloqueio e Desbloqueio

- O nível 1 começa desbloqueado
- Cada nível subsequente é desbloqueado ao completar o anterior
- O progresso de desbloqueio é salvo por jogador

---

## Sistemas de Recompensa e Progressão

### Estrelas (3 por nível)

O desempenho é avaliado com base nas tentativas usadas e fases completadas:

| Resultado | Estrelas |
|-----------|----------|
| Completou com sobra de tentativas | 3 estrelas |
| Completou usando a maioria das tentativas | 2 estrelas |
| Completou no limite ou perdeu fase | 1 estrela |
| Não completou | 0 estrelas |

As estrelas são exibidas na tela de seleção de níveis e são persistidas individualmente por jogador.

### Pontuação (XP)

```
XP por nível = estrelas ativas x 100
Pontuação máxima = 7.200 XP (todos os níveis com 3 estrelas)
```

### Medalhas (10 níveis)

Medalhas são desbloqueadas automaticamente conforme o acúmulo de XP:

| Medalha | XP necessário | Medalha | XP necessário |
|---------|---------------|---------|---------------|
| 1 | 100 | 6 | 3.900 |
| 2 | 1.200 | 7 | 4.800 |
| 3 | 2.100 | 8 | 5.700 |
| 4 | 3.000 | 9 | 6.600 |
| 5 | 3.600 | 10 | 7.200 |

Ao desbloquear uma medalha, um popup de notificação é exibido em tempo real durante o jogo.

### Missões (9 missões em 3 blocos)

Missões são desafios vinculados a níveis e desempenho específicos:

| Bloco | Missão 1 | Missão 2 | Missão 3 |
|-------|----------|----------|----------|
| **Bloco 1** | Completar nível 1 com 1+ estrela | Completar nível 2 com 2+ estrelas | Completar nível 3 com 3 estrelas |
| **Bloco 2** | Completar nível 4 com 1+ estrela | Completar nível 5 com 2+ estrelas | Completar nível 6 com 3 estrelas |
| **Bloco 3** | Completar nível 7 com 1+ estrela | Completar nível 8 com 2+ estrelas | Completar nível 9 com 3 estrelas |

Cada bloco possui:
- Indicador de progresso em percentual
- Áudio descritivo de cada missão
- UI com cores que mudam conforme o estado (cinza, rosa, laranja, amarelo, verde)

### Emblemas (3 colecionáveis)

Ao completar todas as 3 missões de um bloco, o jogador recebe um **emblema** exclusivo. Os emblemas são exibidos na tela de Conquistas e servem como troféu de maestria.

### Ranking

Classificação local dos **3 melhores jogadores** com:
- Nome do jogador
- Pontuação total em XP
- Posição (1o, 2o, 3o)
- Medalha correspondente à pontuação
- Destaque visual para o jogador logado

### Cadeia de Notificações Pós-Nível

Após completar um nível, o jogo exibe notificações em sequência:

```
Placar (resultado) → Medalha nova? (popup) → Missão concluída? (popup)
```

Cada notificação aguarda a anterior ser fechada, proporcionando momentos de celebração sem sobrecarga de informação.

---

## Login e Perfil do Jogador

### Cadastro e Autenticação

- O jogador informa **nome** e **senha** na tela inicial
- Se o nome não existe, é criado um novo perfil automaticamente
- Se o nome existe, a senha é verificada
- Validação: o nome deve conter ao menos uma letra

### Persistência de Dados

Todo o progresso é salvo localmente via `PlayerPrefs` da Unity, incluindo:

- Credenciais (nome e senha)
- Pontuação total (XP)
- Estado de cada nível (bloqueado/desbloqueado)
- Estrelas conquistadas por nível
- Missões e emblemas completados
- Medalhas desbloqueadas
- Nível atual em andamento

### Múltiplos Perfis

O sistema suporta diversos jogadores no mesmo dispositivo. Cada perfil mantém progresso totalmente independente, permitindo uso compartilhado em ambientes escolares.

### Logout

O botão de sair limpa a sessão ativa e retorna à tela de login, sem apagar o progresso salvo.

---

## Arquitetura Técnica

### Padrões de Projeto

| Padrão | Implementação | Propósito |
|--------|---------------|-----------|
| **Singleton** | `GerenciaJogador` | Instância única que persiste entre cenas (`DontDestroyOnLoad`) para gerenciar dados do jogador globalmente |
| **Observer/Event** | Interface `IHasChanged` | Comunicação desacoplada entre sistema de drag-and-drop e inventário — quando blocos são movidos, eventos notificam os observers |
| **State Machine** | `MDNivel` / `MDNivelRomanos` | Gerenciamento de estados do jogo (fase atual, tentativas, nível) com transições controladas |
| **Chain of Responsibility** | Notificações pós-nível | `Placar` → `Medalhas` → `NotificadorDeMissoes` processam sequencialmente |
| **Facade** | `changeScenes` | Ponto único de navegação entre cenas, abstraindo complexidade do `SceneManager` |

### Fluxo do Gameplay

```
[Jogador arrasta bloco]
        │
        v
  DragHandeler (OnBeginDrag → OnDrag → OnEndDrag)
        │
        v
  Slots.OnDrop() ─── Aceita bloco ─── Verifica agrupamento (10→1)
        │                                      │
        v                                      v
  IHasChanged.HasChanged() ◄──── Dispara evento via ExecuteEvents
        │
        v
  Inventory.HasChanged() ─── Calcula valor total
        │                     Decomposição se subtração
        │                     Atualiza UI
        v
  [Jogador confirma]
        │
        v
  MDNivel.Verificar() ─── Compara valor ─── Correto?
        │                                      │
   ┌────┴────┐                            ┌────┴────┐
   │ Errado  │                            │ Correto │
   │Dica som │                            │Prox fase│
   │-1 tent. │                            │ou Vitória│
   └─────────┘                            └─────────┘
```

### Mapa de Dependências dos Scripts

```
                    GerenciaJogador (Singleton)
                           │
         ┌─────────────────┼─────────────────┐
         │                 │                  │
    Usuario           Ranking            Conquistas
         │                                    │
   ┌─────┴──────┐                    ┌────────┴────────┐
   │            │                    │                 │
CenaNivel  CenaNivelRom         Medalhas      GerenciadorMissoes
   │            │                    │                 │
MDNivel    MDNivelRom         NotificadorDeMissoes     │
   │            │                    │                 │
 Placar    PlacarRom          ◄──────┘─────────────────┘
   │            │
   └──────┬─────┘
          │
   ┌──────┴──────┐
   │             │
GerenciadorEstrelas    [Gameplay Core]
EstrelaController    DragHandeler ↔ Slots ↔ Inventory
GerenciadorNiveis              FlowLayoutGroup
```

---

## Tecnologias, Linguagens e Ferramentas

### Engine e Linguagem

| Tecnologia | Versão | Uso |
|------------|--------|-----|
| **Unity** | 6000.0.48f1 (Unity 6) | Game engine principal |
| **C#** | .NET Standard | Toda a lógica de jogo (27 scripts, ~3.500 linhas) |

### Bibliotecas e Packages Unity

| Package | Propósito |
|---------|-----------|
| **TextMesh Pro** | Renderização avançada de texto com fontes SDF, suporte a emojis e gradientes |
| **Unity UI (uGUI)** | Sistema de interface — Canvas, Button, Image, Text, Toggle, Dropdown, InputField, ScrollView |
| **Unity EventSystem** | Gerenciamento de eventos de entrada e interfaces de drag-and-drop (`IBeginDragHandler`, `IDragHandler`, `IEndDragHandler`, `IDropHandler`) |
| **Unity SceneManager** | Navegação entre as 16 cenas do jogo |
| **Unity Analytics** | Coleta de dados de uso |
| **Unity Ads** | Infraestrutura de monetização |
| **Unity Purchasing (IAP)** | Sistema de compras in-app |
| **Unity Recorder** | Gravação de gameplay para documentação |
| **Unity Timeline** | Sequenciamento de animações e eventos |
| **Unity AI Navigation** | Sistema de navegação por malha |
| **Unity 2D Sprite** | Manipulação de sprites 2D |
| **Unity 2D Tilemap** | Sistema de tilemaps |
| **Unity Test Framework + NUnit** | Framework de testes unitários |

### Integrações e Ferramentas Externas

| Ferramenta | Propósito |
|------------|-----------|
| **Figma** | Prototipação e design de todas as telas do jogo |
| **UnityFigmaBridge** | Plugin que importa componentes do Figma diretamente para prefabs Unity, mantendo fidelidade visual (77 componentes importados) |
| **Google Fonts** | Fontes tipográficas integradas via Figma Bridge (Aclonica, Inter, Roboto Condensed, Suwannaphum) |
| **Unity UI Extensions** | Componente `FlowLayoutGroup` para layout responsivo de blocos com wrap automático |
| **Simple UI** | Biblioteca de assets visuais — botões, painéis, frames, ícones vetoriais (80+ ícones) |
| **Visual Studio / VS Code / Rider** | IDEs suportadas para desenvolvimento C# |
| **Git** | Controle de versão |

### Assets de Áudio

| Áudio | Tipo | Uso |
|-------|------|-----|
| `DicaAdicionar.mp3` | Efeito | Dica sonora "adicione blocos" |
| `DicaRemover.mp3` | Efeito | Dica sonora "remova blocos" |
| `fase.mp3` | Efeito | Acerto de fase intermediária |
| `nivel.mp3` / `vitoria.mp3` | Efeito | Completar nível / Vitória |
| `sommissao[1-3]bloco[1-3].mp3` | Narração | 9 áudios descritivos das missões |

### Tipografia

| Fonte | Peso | Uso no Projeto |
|-------|------|----------------|
| **Aclonica** | Regular (400) | Títulos e cabeçalhos |
| **Inter** | Regular (400) | Textos de interface |
| **Roboto Condensed** | Bold (700) | Elementos em destaque |
| **Suwannaphum** | Regular (400) | Textos decorativos |
| **FredokaOne** | Regular | Botões e labels de UI |
| **AlmaMono** | Light | Placares e números |

---

## Estrutura do Projeto

```
versaoTCC/
├── Assets/
│   ├── Scripts/               # 27 scripts C# da lógica do jogo
│   │   ├── GerenciaJogador.cs       # Singleton — gerenciamento do jogador
│   │   ├── Usuario.cs               # Login e autenticação
│   │   ├── MDNivel.cs               # Controlador de nível (Naturais)
│   │   ├── MDNivelRomanos.cs        # Controlador de nível (Romanos)
│   │   ├── DragHandeler.cs          # Drag and drop dos blocos
│   │   ├── Slots.cs                 # Recebimento e agrupamento de blocos
│   │   ├── Inventory.cs             # Cálculo de valor total e decomposição
│   │   ├── Placar.cs                # Placar e cálculo de estrelas
│   │   ├── GerenciadorMissoes.cs    # Sistema de missões
│   │   ├── Medalhas.cs              # Sistema de medalhas
│   │   ├── Ranking.cs               # Classificação dos jogadores
│   │   ├── Conquistas.cs            # Tela de conquistas
│   │   └── ...                      # Demais scripts auxiliares
│   │
│   ├── Scenes/                # 16 cenas ativas + 2 desabilitadas
│   │   ├── Inicio.unity             # Menu principal + Login
│   │   ├── MatDourado.unity         # Jogo modo livre
│   │   ├── MatDourado2.unity        # Jogo níveis naturais
│   │   ├── MatDouradoRomanos.unity  # Jogo níveis romanos
│   │   ├── Naturais.unity           # Seleção de níveis naturais
│   │   ├── Romanos.unity            # Seleção de níveis romanos
│   │   ├── Missoes.unity            # Painel de missões
│   │   └── ...                      # Demais cenas
│   │
│   ├── Prefabs/               # 47 prefabs do jogo
│   │   ├── b1.prefab, b10.prefab, b100.prefab    # Blocos base-10
│   │   ├── Slot.prefab, Slot10.prefab, Slot100.prefab  # Slots de destino
│   │   ├── num0-num9.prefab         # Dígitos visuais
│   │   ├── Estrelas.prefab          # Componente de 3 estrelas
│   │   └── ...                      # Warnings, erros, componentes
│   │
│   ├── Figma/                 # Assets importados do Figma
│   │   ├── Components/              # 77 prefabs de componentes visuais
│   │   ├── Fonts/                   # 4 fontes SDF do Figma
│   │   └── ServerRenderedImages/    # Imagens renderizadas do Figma
│   │
│   ├── Sounds/                # 5 efeitos sonoros + prefabs de áudio
│   ├── Resources/Sounds/      # 9 áudios de narração das missões
│   ├── Confetti/              # Sistema de efeito confetti
│   ├── Simple UI/             # Biblioteca de UI (botões, ícones, painéis)
│   ├── TextMesh Pro/          # Recursos de renderização de texto
│   └── Editor/                # Utilitários do editor (limpar PlayerPrefs)
│
├── ProjectSettings/           # Configurações do projeto Unity
├── Packages/                  # Dependências e packages do Unity
└── versaoTCC.sln              # Solution Visual Studio
```

---

## Como Executar

### Pré-requisitos

- **Unity Hub** instalado
- **Unity 6000.0.48f1** (Unity 6) ou superior

### Passos

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/aventuras-digitais-matematicas.git
   ```

2. Abra o **Unity Hub** e clique em **Open > Add project from disk**

3. Selecione a pasta `versaoTCC/` do repositório clonado

4. Aguarde a Unity importar todos os assets (primeira abertura pode levar alguns minutos)

5. No Unity, abra a cena inicial:
   ```
   Assets/Scenes/Inicio.unity
   ```

6. Pressione **Play** no editor para executar o jogo

### Build

Para gerar um executável:

1. Vá em **File > Build Settings**
2. Verifique se todas as 16 cenas estão listadas e habilitadas
3. Selecione a plataforma desejada (PC, Mac, Linux, Android, iOS)
4. Clique em **Build**

---

## Design e Prototipação

O design de todas as interfaces foi criado no **Figma** e importado para a Unity por meio do plugin **UnityFigmaBridge**, garantindo fidelidade visual entre o protótipo e o produto final.

### Pipeline de Design

```
Figma (Prototipação) → UnityFigmaBridge (Importação) → Unity (Implementação)
```

- **77 componentes** importados automaticamente como prefabs
- **4 famílias tipográficas** do Google Fonts integradas via Figma
- **Escala de imagem 3x** para alta resolução
- Design responsivo com duas referências de resolução:
  - Telas de menu: **3840 x 2160** (4K, Match Width/Height 0.5)
  - Telas de jogo: **1920 x 1200** (Full HD+)

---

<p align="center">
  Desenvolvido com Unity 6 &bull; Design no Figma &bull; C# &bull; Material Dourado Digital
</p>
