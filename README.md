<div align="center">
  
  [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=softwareantics_FinalEngine&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=softwareantics_FinalEngine) 
  
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

While currently Windows-only, the architecture is intentionally modular and extensible. Key systems (such as rendering, input, and resource management) follow interface-driven design, allowing cross-platform implementations to be added over time.

### 🎨 Rendering Engine

Final Engine currently uses a minimal GDI+ software renderer to prioritize accessibility and simplicity. Through interface segregation, the rendering backend is **designed to be extensible**, paving the way for future support for APIs like OpenGL, Vulkan, or DirectX without disrupting existing code.

### ⚙️ Entity-Component-System (ECS) Architecture

Final Engine is built on the ECS paradigm, promoting a clean separation of concerns and a flexible game structure. This pattern enables high-performance systems and easily testable game logic.

> Learn more about ECS [here](https://en.wikipedia.org/wiki/Entity_component_system).

### 📦 Effortless Resource Management

Includes a lightweight and intuitive `IResourceManager` system, with plug-and-play support via `ResourceLoaderBase`. It’s easy to register and manage new asset types like textures, audio, and more.

### 🛠️ Desktop Editor *(Work in Progress)*

We're actively developing a desktop editor to streamline game development. This visual tool will help users manage scenes, entities, systems, and resources. A preview build is expected by the end of 2025.

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

We welcome contributions! Please read our [contribution guidelines](./.github/CONTRIBUTING.md) to get started.

---

## 🧾 License

Licensed under the **GNU AGPL-3.0** with a special exception allowing proprietary games built using the engine.

See [LICENSE](./LICENSE) for full terms.
