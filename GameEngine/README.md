# GameEngine

A .NET class library that models game characters, enemies and world state.
It comes with an xUnit test suite that covers assertions, data-driven theories, shared fixtures and test output.

## Project Structure

```
csharp-projects/
├── GameEngine/                              # Class library
│   ├── GameEngine.slnx                      # Solution (references both projects)
│   ├── GameEngine.csproj
│   ├── Models/
│   │   ├── PlayerCharacter.cs               # Health, weapons, sleep, events
│   │   ├── NonPlayerCharacter.cs            # Health and damage
│   │   ├── Enemy.cs                         # Abstract base: special attack power
│   │   ├── NormalEnemy.cs
│   │   ├── BossEnemy.cs
│   │   └── GameState.cs                     # Players and world events (earthquake, reset)
│   ├── Factories/
│   │   └── EnemyFactory.cs                  # Creates normal/boss enemies, validates boss names
│   ├── Exceptions/
│   │   └── EnemyCreationException.cs        # Thrown for invalid boss names
│   ├── Constants/
│   │   └── GameConstants.cs                 # Health limits, damage, weapons, boss name rules
│   ├── README.md
│   └── decision.md                          # Tests implemented and why
│
└── GameEngine.Tests/                        # xUnit test project
    ├── GameEngine.Tests.csproj
    ├── Constants/
    │   └── TestCategories.cs                # Trait category names used for filtering
    ├── Models/
    │   ├── PlayerCharacterShould.cs
    │   ├── NonPlayerCharacterShould.cs
    │   ├── BossEnemyShould.cs
    │   ├── GameStateEarthquakeShould.cs     # Share one GameState via the collection
    │   └── GameStateResetShould.cs
    ├── Factories/
    │   └── EnemyFactoryShould.cs
    ├── Fixtures/
    │   ├── GameStateFixture.cs              # Shared, expensive-to-create GameState
    │   └── GameStateCollection.cs           # Collection definition for sharing the fixture
    └── TestData/
        ├── InternalHealthDamageTestData.cs  # In-code theory data
        ├── HealthDamageDataAttribute.cs     # Custom attribute that reads TestData.csv
        └── TestData.csv                     # Damage/health cases
```

Namespaces follow the folders (`GameEngine.Models`, `GameEngine.Tests.Models`, …), and the test project mirrors the library's layout, so each class's tests are easy to find.

## Tech Stack

- .NET 10 / C#
- xUnit 2.9, restored from NuGet on build

## Getting Started

### Prerequisites

- [.NET SDK 10.0+](https://dotnet.microsoft.com/download) (check with `dotnet --version`)

### Build and Test

```bash
git clone <repo-url>
cd csharp-projects/GameEngine

dotnet build GameEngine.slnx
dotnet test GameEngine.slnx
```

### Run Specific Tests

```bash
# By category: Player, NPC, Enemy, GameState
dotnet test GameEngine.slnx --filter "Category=Enemy"

# Show ITestOutputHelper output
dotnet test GameEngine.slnx --logger "console;verbosity=detailed"
```