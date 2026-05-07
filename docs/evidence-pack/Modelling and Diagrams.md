# Modelling and Diagrams

## System Context Diagram

```mermaid
flowchart TB
    Player[Player]
    ConsoleApp[ConsoleTest console app]
    DisplayLayer[PixelBoardDisplay]
    SharedFile[shared/latest_score.json]
    LeaderboardAPI[External leaderboard API]
    Hardware[Arduino / serial hardware]

    Player --> ConsoleApp
    ConsoleApp --> DisplayLayer
    ConsoleApp --> SharedFile
    ConsoleApp --> LeaderboardAPI
    DisplayLayer --> Hardware
    SharedFile --> LeaderboardAPI
```

## Container and Component Diagram

```mermaid
flowchart LR
    subgraph ConsoleTest
        Program[Program.cs]
        Games[Snake / Tetris / Education]
        LeaderboardClient[LeaderboardClient.cs]
    end

    subgraph PixelBoardDisplay
        IDisplay[IDisplay]
        ConsoleDisplay[ConsoleDisplay]
        ArduinoDisplay[ArduinoDisplay]
        DisplayHelper[DisplayHelper]
    end

    subgraph PixelCatsClient
        ApiClient[ApiClient]
        PixelCatsApiClient[PixelCatsApiClient]
        ScoreWatcher[ScoreWatcher]
        ProgramHelpers[ProgramHelpers]
    end

    Program --> Games
    Program --> LeaderboardClient
    Games --> IDisplay
    IDisplay --> ConsoleDisplay
    IDisplay --> ArduinoDisplay
    ConsoleDisplay --> DisplayHelper
    ArduinoDisplay --> DisplayHelper
    ScoreWatcher --> ProgramHelpers
    ApiClient --> LeaderboardAPI[External leaderboard API]
    PixelCatsApiClient --> LeaderboardAPI
    LeaderboardClient --> LeaderboardAPI
```

## Sequence Diagram for a Major User Workflow

```mermaid
sequenceDiagram
    participant U as User
    participant P as ConsoleTest.Program
    participant G as Selected IGame
    participant D as IDisplay implementation
    participant L as External leaderboard API
    participant F as shared/latest_score.json

    U->>P: Start game and provide input
    P->>G: Initialize()
    loop Game loop
        P->>G: Update(pixels)
        G-->>P: Score and board state
        P->>D: Draw(pixels)
    end
    G-->>P: IsGameOver() = true
    P->>F: Write final score atomically
    P->>L: POST /api/codes
    L-->>P: Return claim code
    P->>F: Rewrite score file with code
```

## Activity Diagram

```mermaid
flowchart TD
    Start([Start]) --> Title[Title state]
    Title -->|S pressed| Playing[Playing state]
    Title -->|A/D pressed| SwitchGame[Change selected game]
    Playing -->|Escape pressed| Title
    Playing -->|Game over detected| GameOver[GameOver state]
    GameOver -->|Submit score and mint code| ReturnTitle[Return to Title]
    ReturnTitle --> Title
```

## Storage Model Diagram

The repository does not contain a physical relational database, so a traditional ERD is not applicable. The closest useful model is the file-backed score export plus the external leaderboard record.

```mermaid
flowchart LR
    ScoreExport[Local score export JSON]
    LeaderboardRecord[External leaderboard record JSON]

    ScoreExport -->|fields: score, state, gameName, timestamp, code? | Consumer[ScoreWatcher / client tools]
    LeaderboardRecord -->|fields: id, name, score, created_at, gameName| Consumer
```

## Detaile UML Diagram

```mermaid
flowchart LR
  subgraph "ConsoleTest App"
    CTProgram["ConsoleTest Program"]
    CodeGen["CodeGenerator"]
    LeaderboardClient["LeaderboardClient"]
    ScoreFile["Shared score file latest_score.json"]
  end

  subgraph "Games (ConsoleTest.Games)"
    IGame["IGame"]
    Snake["Snake"]
    Tetris["Tetris"]
    Education["Education"]
  end

  subgraph "PixelBoardDisplay"
    IDisplay["IDisplay"]
    ConsoleDisplay["ConsoleDisplay"]
    ArduinoDisplay["ArduinoDisplay"]
    DisplayHelper["DisplayHelper"]
    IPixel["IPixel"]
    ILocatedPixel["ILocatedPixel"]
    Pixel["Pixel"]
    LocatedPixel["LocatedPixel"]
    SerialPortManager["SerialPortManager"]
    ArduinoInput["ArduinoInput"]
  end

  subgraph "PixelCatsClient"
    ApiClient["ApiClient"]
    PixelCatsApiClient["PixelCatsApiClient"]
    ProgramHelpers["ProgramHelpers"]
    ScoreWatcher["ScoreWatcher"]
  end

  subgraph "HerdingCats App"
    HCProgram["HerdingCats Program"]
  end

  subgraph "Tests"
    GenericGameTests["GenericGameTests"]
    EducationTests["EducationTests"]
  end

  subgraph "External Systems"
    LeaderboardAPI["Leaderboard API"]
    ArduinoHW["Arduino hardware"]
    ConsoleUI["Console terminal"]
    OSInput["OS keyboard input"]
  end

  CTProgram -->|"runs game loop"| IGame
  IGame -->|"implemented by"| Snake
  IGame -->|"implemented by"| Tetris
  IGame -->|"implemented by"| Education

  Snake --> Pixel
  Tetris --> Pixel
  Education --> Pixel

  CTProgram -->|"renders via"| IDisplay
  IDisplay -->|"implemented by"| ConsoleDisplay
  IDisplay -->|"implemented by"| ArduinoDisplay

  ConsoleDisplay --> DisplayHelper
  ArduinoDisplay --> DisplayHelper

  DisplayHelper --> IPixel
  DisplayHelper --> ILocatedPixel

  ILocatedPixel -->|"extends"| IPixel

  LocatedPixel -->|"inherits"| Pixel
  Pixel -->|"implements"| IPixel
  LocatedPixel -->|"implements"| ILocatedPixel

  ArduinoDisplay --> SerialPortManager
  SerialPortManager --> ArduinoHW

  ArduinoInput --> OSInput
  ConsoleDisplay --> ConsoleUI

  CTProgram --> LeaderboardClient
  LeaderboardClient --> LeaderboardAPI

  CodeGen --> ApiClient
  ApiClient --> LeaderboardAPI

  PixelCatsApiClient --> LeaderboardAPI

  CTProgram --> ScoreFile
  ScoreWatcher -->|"reads"| ScoreFile
  ScoreWatcher --> ProgramHelpers

  HCProgram --> IDisplay
  HCProgram --> IPixel

  GenericGameTests --> IGame
  EducationTests --> IGame
  GenericGameTests --> IPixel
  EducationTests --> IPixel
```
