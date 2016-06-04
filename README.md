# .NET Research

The .NET landscape is drastically changing (for the better). This repo contains research into the "new .NET" - .NET core, Visual Studio Code, and anything .NET "vNext".


## Common Themes

Common themes underlying the new .NET directions.

* All OSS. Everything is developed in the open, on Github. Including Visual Studio Code.

* Modularization.
  * Fully self-contained application distributions possible (containerable). Eliminates versioning problems.
  * "CoreFX" (the standard library) is a modular collection of libraries. You can include only the framework components you need.

* Microsoft.NetCore.App : included with the runtime. Includes base types, Console, Task and other "necessary" types.

## Visual Studio

> Unlike traditional IDEs with everything but the kitchen sink, you can tune your installation to the development technologies you care about.

* Electron based. Minimal core + extensions (Atom).
