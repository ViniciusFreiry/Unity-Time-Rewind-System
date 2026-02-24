# 🎮 Unity Time Rewind System

A gameplay mechanic that allows the player to rewind time while spawning a visual "temporal echo" that replays previously recorded actions.

Built using Unity 2022.3.21f1.

---

## 📌 Overview

This project implements a real-time rewind system where:

- The player’s transform state is continuously recorded.
- A rewind action reverts the player to previous states.
- A temporal clone (echo) replays the recorded movement.
- The Animator is disabled during rewind to prevent animation conflicts.
- The clone is automatically destroyed after playback finishes.

This system focuses on clean architecture, memory control, and deterministic playback behavior.

---

## 🛠 Engine Version

- Unity 2022.3.21f1 (LTS)
- C#

---

## ⚙️ Core Features

- Transform state recording (position, rotation, scale)
- Velocity tracking
- FixedUpdate-based state sampling
- Temporal echo playback
- Animator disabling during rewind
- Automatic clone destruction
- Controlled rewind duration
- Separation of recording and playback logic

---

## 🧠 System Architecture

### 1️⃣ State Recording

The system continuously stores:

- Position
- Rotation
- Scale
- Velocity
- Sprite

Recorded states are pushed into a buffer structure to allow reverse traversal during rewind.

---

### 2️⃣ Rewind Logic

When rewind is triggered:

- Player movement input is disabled.
- Animator component is disabled.
- Stored states are iterated in reverse order.
- Player transform is restored frame by frame.

---

### 3️⃣ Temporal Echo (Clone Playback)

A clone is instantiated at rewind start:

- It replays the recorded movement in forward order.
- Visualizes the past actions of the player.
- Is destroyed automatically when playback ends.

This creates a clear temporal feedback loop.

---

## 🔍 QA-Oriented Testing Notes

The following edge cases were tested:

- Rewind while moving
- Rewind during animation transitions
- Rapid rewind activation (spam input)
- Rewind at state buffer limits
- Clone destruction timing validation
- Physics consistency during rewind

### Observed Constraints

- System currently supports a single rewindable object.
- Does not persist across scene transitions.
- Large rewind buffers may impact memory usage.

---

## 🧪 Performance Considerations

- Uses controlled buffer size to prevent memory overflow.
- Avoids unnecessary allocations during playback.
- Designed for deterministic behavior.

---

## 🚀 Possible Improvements

- Multi-object rewind support
- Event state rewind (not only transform)
- Snapshot-based save/load
- Optimization using struct pooling
- Custom editor tools for rewind debugging

---

## 📂 Project Structure
```
Assets/
├── Scripts/
│ ├── RewindRecorder.cs
│ ├── RewindController.cs
│ └── TemporalClone.cs
├── Prefabs/
├── Scenes/
ProjectSettings/
Packages/
```
---

## 🎯 Learning Goals

This project was developed to explore:

- Time-based gameplay mechanics
- State management systems
- Reverse iteration logic
- Clean system separation
- Gameplay architecture patterns

---

## 📜 License

This project is for educational and portfolio purposes.