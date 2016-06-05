# .NET Research

The .NET landscape is drastically changing (for the better). This repo contains research into the "new .NET" - .NET core, Visual Studio Code, and anything .NET "vNext".


## Common Themes

Common themes underlying the new .NET directions.

* All OSS. Everything is developed in the open, on Github. Including Visual Studio Code.

* Modularization.
  * Fully self-contained application distributions possible (containerable). Eliminates versioning problems.
  * "CoreFX" (the standard library) is a modular collection of libraries. You can include only the framework components you need.

* Microsoft.NetCore.App : included with the runtime. Includes base types, Console, Task and other "necessary" types.

## Visual Studio Code

> Unlike traditional IDEs with everything but the kitchen sink, you can tune your installation to the development technologies you care about.

> Code has a high productivity code editor, when combined with programming language services, gives you the power of an IDE with the speed of a text editor. 

* Visual Studio Code is the "Anti Visual Studio". Minimal. Extension based. Command line focused. Built with JS. 

* Electron based. Minimal core + extensions (Atom).

* Inline reference information (metadata) is awesome. (Find dead code). See `editor.referenceInfos` setting.

#### Helpful Shortcut Keys

* Cmd-B : Toggle left bar.
* Opt-Z : Toggle word wrap
* Ctrl-_ : Jump to previous point.
* Ctrl-Shift-_ : Jump to next point.
* Ctrl-Cmd-Shift--> : Extend highlight to the next scope.
* Alt-Shift (drag mouse) : Column / box selection.

###### Code

* Cmd-Shift-O : Goto symbol (same file).
* Cmd-T : Jump to symbol (all files).

* F2 : Rename.
* F12 : Goto definition.
* Shift-F12 : Find all references.
* Opt-F12 : Peek definition.
* Cmd-Shift-M : Toggle Warnings / Errors
