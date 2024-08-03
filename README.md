# Mouse Aim Trainer - Unity3D

## Features

- **Dynamic Target Spawning**: Targets appear at random locations within the defined game area.
- **Score Tracking**: Keep track of successful hits and misses.
- **Timed Sessions**: Practice within a set time limit to challenge yourself.
- **Customizable Difficulty**: Adjust target size, spawn rate, and game duration to suit your skill level.
- **Visual Feedback**: Targets change color when missed or about to disappear, providing immediate feedback.
- **Reset Functionality**: Easily restart the training session with a reset button.

## Getting Started

- use one of the prebuilt releases
  
### Prerequisites

- Unity 2020.3 or later
- Git LFS (for handling large files in the repository)

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/captainzero93/Mouse-Aim-Trainer-UNITY3D.git
   ```

2. Open the project in Unity Hub by selecting the cloned directory.

3. Once opened in Unity, navigate to the main scene (usually located in `Assets/Scenes/`).

4. Press the Play button in the Unity Editor to start the game in the editor.

## How to Play

1. Start the game.
2. Move your mouse to aim at the targets.
3. Click to shoot the targets.
4. Try to click as many targets as possible before the timer runs out.
5. Missing counts as -1 point.
6. Use the reset button to start a new session.

## Customization

You can customize various aspects of the game by adjusting public variables in the Unity Inspector:

- In the `TargetSpawner` component:
  - Adjust spawn rate, target size, and spawn area.
- In the `GameManager` component:
  - Modify game duration and scoring rules.

## Contributing

Contributions to improve Mouse Aim Trainer are welcome. Please feel free to fork the repository, make changes, and submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.
