# 🔷 TopDownFusionTest

A simple top-down multiplayer demo built with **Unity** and **Photon Fusion 2**.
Players can enter a name, pick a custom color, and join a shared multiplayer session where each player sees others moving in real time.

---

## 📦 Project Overview

* 🎮 **Unity Version**: 2022.3 LTS
* 🌐 **Networking**: Photon Fusion 2 (Shared Mode)
* 🧱 **Template**: 3D (Core)
* 🛠️ **Tools & Libraries Used**:

  * **Unity 2022.3 LTS**
    Long-Term Support (LTS) version for maximum stability and plugin compatibility.
  * **Photon Fusion 2 (Shared Mode)**
    High-performance networking library for Unity. Shared Mode allows each client to control its own player, ideal for lightweight multiplayer demos.
  * **TextMesh Pro**
    High-quality text rendering used for player name input and name tag display above characters.
  * **CharacterController**
    Unity's built-in component for smooth player movement, gravity handling, and collision without needing Rigidbody.
  * **Custom C# Scripts**

    * `PlayerController`, `PlayerNetwork`, `CameraFollow`: Handle player movement, gravity, network syncing, and camera tracking.
    * `MainMenuUI`, `Billboard`: Manage UI and name tag facing camera.
    * `GameSettings`: Global settings like player name and color.
    * `NetworkManager`, `NetworkInputData`: Setup and manage network session and input using Photon Fusion.

---

## 🚀 Getting Started

### 1. Clone the Project

```bash
git clone https://github.com/anhtuanatc/TopDownFusionTest
```

---

### 2. Open in Unity

* Open the project with **Unity 2022.3 LTS**
* Open scene: `Assets/Scenes/MainScene.unity`

---

### 3. Set up Photon Fusion

1. Go to [https://dashboard.photonengine.com/](https://dashboard.photonengine.com/)
2. Create a **Fusion (Photon)** app
3. Copy the **App ID**
4. In Unity, go to:
   `Tools → Fusion → Fusion Hub → Fusion 2 Setup`
   Paste your App ID and complete setup

---

### 4. Run Multiplayer Test

* Press **Play** in Unity Editor → input name and select color → click **Start**
* To test multiplayer:

  * Open a **second instance** (File → Build & Run)
  * Join the same session
  * Each player will have a different name and color

---

## 🎯 Features

* ✅ Player name input + color picker UI
* ✅ Player prefab with:

  * Color sync
  * Name tag above head
  * Camera follow
* ✅ Movement (WASD) + sprint (Shift) + jump (Space)
* ✅ Fusion Shared Mode networking with spawn logic
* ✅ Clean folder & prefab structure

---

## 📁 Project Structure

```
Assets/
├── Scripts/
│   ├── Player/              # PlayerController, PlayerNetwork, CameraFollow
│   ├── Network/             # NetworkManager, NetworkInputData
│   ├── UI/                  # MainMenuUI, Billboard
│   └── Config/              # GameSettings
├── Prefabs/
│   └── NetworkPlayer        # Player prefab
├── Scenes/
│   └── MainScene.unity
├── Materials/
│   ├── BoxMat.mat
│   ├── GroundMat.mat
│   ├── PlayerEyeMat.mat
│   └── PlayerHandMat.mat
├── Textures/                # Texture assets used in materials
├── Photon/                  # Photon Fusion 2 assets and setup
├── TextMesh Pro/            # TextMeshPro essentials and fonts
└── README.md
```

---

## 🧠 Technical Notes

* ✅ `Object.HasInputAuthority` is used to ensure local player-only input
* ✅ `NetworkRunner.Spawn()` is used with prefab reference (Inspector-bound)
* ✅ Input is registered via `OnInput()` (Fusion callback) using a `NetworkInputData` struct

---

## 📩 Author

Created for a Unity Developer Test.
**Name**: Kieu Anh Tuan
**Email**: [tuanka1904@gmail.com.com](mailto:tuanka1904@gmail.com.com)
**GitHub**: [github.com/anhtuanatc](https://github.com/anhtuanatc)
