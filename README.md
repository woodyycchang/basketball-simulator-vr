# CS579 Basketball VR

A VR basketball game built with Unity for the CS579 course.

## Requirements

- **Unity Version:** 6000.0.44f1
- **Render Pipeline:** Universal Render Pipeline (URP)
- **Target Platform:** Android (Meta Quest)

## Project Setup

1. Clone the repository
2. Open the project in Unity 6000.0.44f1
3. Ensure XR Plugin Management is configured for your target headset

## Key Features

- VR basketball shooting mechanics
- Score tracking system
- XR Interaction Toolkit integration

## Project Structure

```
Assets/
├── Scenes/
│   └── SampleScene.unity    # Main game scene
├── BasketScore.cs           # Score tracking interface
└── ...
```

## Development Notes

### Score System

The scoring interface is located in `Assets/BasketScore.cs`. Access the current score via:

```csharp
BasketScore.score
```

### Adding New Basketballs

All basketball objects must have the **"Basketball"** tag to interact properly with the hoop detection system. When adding a new ball:

1. Create or import your ball prefab
2. Select the GameObject
3. In the Inspector, set Tag → `Basketball`

### Custom Tags

| Tag | Purpose |
|-----|---------|
| `Basketball` | Ball objects for hoop interaction |
| `Anchor` | XR anchor points |
| `Rack` | Ball rack/storage |
| `HoopCenter` | Basket detection trigger |
| `Ground` | Floor collision |

## Build Instructions

1. Go to **File → Build Settings**
2. Select **Android** platform
3. Ensure XR settings are configured in **Project Settings → XR Plug-in Management**
4. Click **Build and Run**

## Dependencies

- XR Interaction Toolkit 3.1.1
- XR Hands 1.5.1
- OpenXR 1.14.1
- Input System 1.13.1
- Universal RP 17.0.4

## License

[Add your license here]
