# Shared Microgame Framework

## Purpose

*The framework works via 3 scripts: [MicrogameSession.cs](MobileGameProject\Assets\Scripts\Framework\MicrogameSession.cs), [MicrogameBehaviour.cs](MobileGameProject\Assets\Scripts\Framework\MicrogameBehaviour.cs), and [SceneNavigator.cs](MobileGameProject\Assets\Scripts\Framework\SceneNavigator.cs). The framework intends to handle common game management and navigation independently from each game, and for integration with each game.*

| Component | Responsibility |
|---|---|
| [MicrogameSession.cs](MobileGameProject\Assets\Scripts\Framework\MicrogameSession.cs) | Holds variables for common elements across the microgames. This includes an enum to track what phase the game is in, as well as references to common GameObjects. It also carries methods that are called by UI buttons and other game logic scripts to transition between game phases. |
| [MicrogameBehaviour.cs](MobileGameProject\Assets\Scripts\Framework\MicrogameBehaviour.cs) | A more basic script that has a reference to the [MicrogameSession.cs](MobileGameProject\Assets\Scripts\Framework\MicrogameSession.cs) and a boolean value for whether the game is running or not. It also has short methods to call to begin or end the game, and for when the player wins or loses the game. |
| [SceneNavigator.cs](MobileGameProject\Assets\Scripts\Framework\SceneNavigator.cs) | Carries references and methods to the different microgame scenes. These methods are accessed by buttons in the main menu and the intermediate menus in the microgames. |

## Framework Features
- [X] Microgame selection
- [X] Starting and ending games
- [X] Replay behaviour
- [X] Game pausing
- [ ] Shared score
- [X] Common UI
- [ ] Persistent data
- [X] Common interfaces or base classes

## Architecture Diagram

A small diagram may be included if useful.

## Integration with Microgames
Explain how individual microgames connect to the framework.

## AI-Supported Development

*AI was not used to develop the framework for this microgame collection.*