![GitHub-Mark-Light](https://user-images.githubusercontent.com/50978201/193459338-32d71599-19d6-4eb6-b5b3-c34348d623b9.svg#gh-dark-mode-only)
![GitHub-Mark-Dark](https://user-images.githubusercontent.com/50978201/193459322-b078ed0d-cf0d-4791-ad10-ee2f3131cd20.svg#gh-light-mode-only)


<div align="center">

  [![✅ Build Status Check](https://github.com/softwareantics/FinalEngine/actions/workflows/build-status-check.yml/badge.svg?branch=final)](https://github.com/softwareantics/FinalEngine/actions/workflows/build-status-check.yml)
  [![🧪 Unit Tests Status Check](https://github.com/softwareantics/FinalEngine/actions/workflows/unit-test-status-check.yml/badge.svg)](https://github.com/softwareantics/FinalEngine/actions/workflows/unit-test-status-check.yml)
  
  <strong>Final Engine</strong> is an open-source game engine developed in C# using .NET 9.0. 
  What began as a hobby project has rapidly evolved into a tool we're committed to actively developing and maintaining. 
  The core objective of Final Engine is to offer a feature-rich environment that prioritizes simplicity, accessibility, and full creative freedom.
</div>

<p align="center">
  <em>Create an engine that makes game development enjoyable, straightforward, and effortless while granting users complete creative freedom.</em>
</p>

<div align="center">

https://www.softwareantics.com.au
<br><br>
 <a href="https://www.x.com/softwareantics"><img src="https://raw.githubusercontent.com/CLorant/readme-social-icons/refs/heads/main/large/colored/twitter.svg" alt="Twitter"></a>
  &nbsp;&nbsp;&nbsp;
  <a href="https://www.youtube.com/@softwareantics"><img src="https://raw.githubusercontent.com/CLorant/readme-social-icons/refs/heads/main/large/colored/youtube.svg" alt="YouTube"></a>
  &nbsp;&nbsp;&nbsp;
  <a href="https://discord.gg/UNdKXsdeQb"><img src="https://raw.githubusercontent.com/CLorant/readme-social-icons/refs/heads/main/large/colored/discord.svg" alt="Discord"></a>
</div>

<p align="center">
  <a href="#-key-features">Key Features</a> •
  <a href="#-getting-started">Getting Started</a> •
  <a href="#-download">Download</a> •
  <a href="#-contributing">Contributing</a> •
  <a href="#-license">License</a>
</p>

---

## 🔑 Key Features

### 🖥️ Platform Support

Tested on Windows, but designed to work across Windows, macOS, and Linux. The architecture is intentionally modular and extensible. Key systems—such as rendering, input, and resource management—follow an interface-driven design, enabling cross-platform implementations to be added as needed.

### 🎨 Rendering Engine

_Final Engine_ offers a feature-rich rendering abstraction layer built over OpenGL (with plans to support additional backends like Direct3D and Vulkan in the future). This API empowers users to engage directly with the graphics card while also providing systems and features for easily rendering meshes and sprites within scenes.

### ⚙️ Entity-Component-System (ECS) Architecture

Final Engine is built on the ECS paradigm, promoting a clean separation of concerns and a flexible game structure. This pattern enables high-performance systems and easily testable game logic.

> Learn more about ECS [here](https://en.wikipedia.org/wiki/Entity_component_system).

### 📦 Effortless Resource Management

Includes a lightweight and intuitive `IResourceManager` system, with plug-and-play support via `ResourceLoaderBase`. It’s easy to register and manage new asset types like textures, audio, and more.

### 🛠️ Desktop Editor *(Work in Progress)*

We're actively developing a desktop editor to streamline game development. This visual tool will help users manage scenes, entities, systems, and resources. A preview build is expected by to be available alongside the first initialize release (v0.1.0).

---

## 🚀 Getting Started

Follow these steps to build and run the engine.

> 💬 **Need help?** Join our [Discord](https://discord.gg/UNdKXsdeQb) if you run into any setup issues or have feature questions. While we don’t yet have full user documentation, your feedback is invaluable and helps shape development.

### ✅ Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### 🧱 Building (Windows, Mac, Linux)

1. Clone or download the repository.
2. Open `FinalEngine.sln` in your preferred IDE.
3. Build the solution or run `dotnet build` from the CLI.

---

## 📦 Download

Release builds will be available via:

- [GitHub Releases](https://github.com/softwareantics/FinalEngine/releases)
- [NuGet Packages](https://www.nuget.org/profiles/softwareantics)

---

## 🤝 Contributing

We welcome contributions! Please read our [contribution guidelines](./CONTRIBUTING.md) to get started.

---

## 🧾 License

Licensed under the **GNU AGPL-3.0** with a special exception allowing proprietary games built using the engine.

See [LICENSE](./LICENSE) for full terms.
