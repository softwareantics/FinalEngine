<div align="center">
  
  # ✅ Engine MVP Design Document (v0.1.0)
  
</div>

## ⚙️ Engine

- **OS:** Windows 10+
- **Framework:** .NET 9.0+

### 🧱 Architecture

- Clean/onion-based architecture with clear separation between core logic and platform-specific layers.
- Modular package structure optimized for testability and maintainability.
- Visual Studio templates included to streamline project creation and setup.

---

## ✨ Core Engine Features

### 🖼️ Rendering

- Abstracted rendering system using **GDI+** backend
- **Texture** and **Font** rendering with support for:
  - _Positioning, rotation and scaling_
  - _Opacity and color tinting_
- Multiple graphics context support
- Render resource factory:
  - Creates renderable resources from raw data

### 🎮 Input

- Keyboard and mouse input with:
  - Event-based device layer
  - Polling-based high-level layer
- Input mapping system with simple action bindings:
  - Bind named actions to keys or buttons (e.g., `"Attack"` → `Space`, `A`, `MouseLeft`)

### 🔊 Audio

- Core audio abstraction with:
  - Play, stop, pause, volume, and loop control
  - Positional audio (2D simulation via stereo panning and distance attenuation)
- Global/master volume control

### 🧠 Entity Component System (ECS)

- Custom ECS architecture with:
  - Support for user-defined components and systems
  - Event hooking:
    - `Initialize`, `LoadContent`, `Update`, `Render`, `UnloadContent`
    - Includes custom event firing
- Built-in systems:
  - Sprite rendering
  - Text/GUI rendering
  - Audio emitters and listeners
  - Orthographic camera rendering
  - WASD camera movement

### 🌍 Screen Management

- Scene-based structure with:
  - Scene loading and unloading
  - Support for switching between scenes
  - Each scene maintains its own local resource manager

### 📚 Resource Management

- Dual-scope resource handling:
  - Global and screen-specific resource managers
- Reference-counting system to manage lifetime and unloading
- Loader registration system to support multiple resource formats

### 🖋️ GUI System

- Basic text-based rendering for GUI
- Features include:
  - Font scaling
  - Text alignment
  - Color customization
- No interactive components (e.g., buttons, sliders) in MVP

### 🎞️ Animation

- Sprite sheet animation support
  - Adjustable frame speed per animation
- Integration with ECS (e.g., animated sprite components)

### ➗ Math Utilities

- Uses `System.Numerics` for vector and matrix manipulation.
- Included `MathHelper` for common operations.

---

## 🌐 System Features

### 🪟 Windowing

- Abstracted window system
- Support for:
  - Windowed and fullscreen modes
  - Title, size, visibility, and window state management

### 🧾 Configuration

- Environment-based configuration system
  - Supports standard `appsettings.{Environment}.json` pattern
  - Engine auto-detects and applies correct environment settings

### 🪵 Logging

- Built-in structured logging system using `Microsoft.Extensions.Logging`
  - Integration with application-level configuration
  - Logging levels configurable per category

### ⚠️ Error Handling

- Localized exception handling within individual engine systems
- No global error handler or crash recovery in MVP

---

## 📦 Supported File Formats

- **Texture/Image Formats:**
  - Bmp
  - Gif
  - Jpeg
  - Pbm
  - Png
  - Tiff
  - Tga
  - WebP
  - Qoi

- **Audio Formats:**  
  - .Ogg
  - .Mp3

---

<div align="center">
  
  # ✅ Editor MVP Design Document (v0.1.0)
  
</div>

## 🎯 Target Platform

- **OS:** Windows 10+
- **Framework:** .NET 9.0+

### 🧱 Architecture

- MVVM-based **Windows Forms** application
- Modular layout with docking/resizable panels

---

## ✨ Core Editor Features

### 🧩 Scene Hierarchy

- Entity listing with **no** parent-child structure
- Simple right-click actions (rename, delete)

### 🧱 Entity & Component Editing

- Add/edit/remove components from entities
- Create/rename/delete entities
- Modify translation, rotation and scale via gizmos.
- Supports editing common types:
  - Integers, strings, booleans, tec
  - Vectors
  - Enumerations
  - Colors

### 📁 Resource Management

- Displays all project resources (textures, fonts, audio)
- Assign resources to components via drag-and-drop
- Uses shared `IResourceManager`
- Synchronous loading and display
- Supported asset previews (e.g., texture thumbnails)

### 🪵 Logging & Console

- Displays logs from runtime and editor
- Filter by severity (info, warning, error)
- Scrollable with auto-scroll toggle

### ⚙️ ECS Management

- ECS system listing
- On/off toggle for built-in systems
- Modification of system properties

### ↩️ Undo/Redo

- Basic command pattern with undo stack
- Linear undo/redo (Ctrl+Z / Ctrl+Y)
