# .NET Research

This repo contains research into the "new .NET" - .NET core, Visual Studio Code, and Microsoft's new OSS focus.

The .NET landscape is drastically changing (for the better).  

* Microsoft's culture has shifted from the 90's "embrace, extend, extinguish" to embracing the spirit of OSS.
  * Development done in the open, on github.
  * Minimalist tools (VS Code) based on Electron, not "WPF" or another heavy MS UI toolchain. 
  * Creating linux ports of Server products (e.g., SQL Server).
  * OSS, multi-platform, command line based by default.

## Links 
[Getting started with .NET Core](https://docs.microsoft.com/en-us/dotnet/articles/core/index) 

* High level overview of the pieces and architecture of .NET core.

[Getting started with .NET Core on macOS](https://docs.microsoft.com/en-us/dotnet/articles/core/tutorials/using-on-macos)

* Tutorial on creating a project with a main project, library, and unit tests.


## TODO (VS Code)

* Visual Studio Code Documentation
  * https://code.visualstudio.com/Docs
* [xUnit](https://xunit.github.io/)
  * Does `xUnit` have Xcode's concept of expectations / async execution?
* Code : remove whitespace on save (extension?)
* Git integration.
* How to use VS Code as a git merge tool?

## TODO (C#)

* C# documentation comments.
* Documentation generation.
* Assertions, logging output, and debugging diagnostics.

## TODO (BCL)

* TPL
* async / await

## Common Themes

* All OSS. Everything is developed in the open, on Github. Including Visual Studio Code.
* .NET Core is an alternate .NET Framework version which runs on Linux, macOS, and Windows.

* Modularization.
  * Fully self-contained application distributions possible (containerable). Eliminates versioning problems.
  * "CoreFX" (the standard library) is a modular collection of libraries. You can include only the framework components you need.

* Microsoft.NetCore.App : included with the runtime. Includes base types, Console, Task and other "necessary" types. This is the foundation of .NET.

## .NET Core

Q. What is .NET Core?

A. A cross-platform version of .NET, including : .NET Core includes a .NET Runtime, framework libraries, SDK tools (compilers, SDK tools), and a `dotnet` app host.

* It's interesting that .NET core includes console apps and ASP.NET apps. Anything that is MS specific (like WPF or Winforms) are obviously not part of .NET core. 
* ARM32 and ARM64 are coming soon. Perhaps we could run .NET apps on Android and iOS, but why?
* Most of .NET Core is shared code. There are platform specific implementations 
* .NET core's aim is to create a cross platform environemnt free of platform specific features (windows registry, code access security). 
* The team's goal is to create a version of .NET not bogged down to Windows, IIS, or with obsolete Windows libraries and technologies.
* ASP.NET is the primary use case with .NET core. Microsoft is essentially admitting that native apps are obsolete.





## Visual Studio Code

Visual Studio code is mostly a "me too" editor in a similar vain to Atom, Sublime, and other package based text editors. With a default install, it's a minimal core editor. The power of VS Code, like Atom and Sublime, comes via extensions. Extensions add large feature sets to the core - like language support, debugging, text formatting, theming, everything. Extensions are critical in VS Code. For example, you can't even program C# in VS Code without a C# extension. And yes, this is from Microsoft.

By requiring extensions for everything, it allows engineers to tailor the editor solely to the feature set they require without having to carry around anything they don't. While you could selectively pick the features Visual Studio included in it's install, VS Code relies completely on extensions and makes it trivial to add / remove and update extensions without Visual Studio's clumsy installer or heavy add-ins.

Stop for a moment and think about the cultural change that must have occurred within Microsoft to create VS Code. VS Code goes completely against 30 years of big, bloated, annually / bi-annually updated, and expensive software for Microsoft. Since many of today's engineers were in diapers, Microsoft has been releasing these massive, multi-gigabyte, $1000+ editors with meaningless names like "Visual Studio 2005 Enterprise Edition". And here we are, in 2016, with a radically modern IDE coming from the same company. 

The differences between Visual Studio of old and VS Code is downright shocking. Visual Studio has an ugly, slow installer, a massive install footprint, multiple versions with extremely complex feature sets (Developer, Enterprise, Professional), lengthy release and update cycles (Release Candidate 1, 2), Windows only, developed in C++ using "native" Microsoft toolkits, and has a massive memory footprint.

VS Code is a simple, small install, has a single "version", is extension based, updated frequently, multi-platform, developed in Javascript, and has a small memory footprint.

This is radical, radical territory for Microsoft and is a leading indicator of what appears to be a massive cultural shift for them. Changing corporate culture is a massive feat that deserves great respect.

Equally interesting is the underlying technology in which VS Code is developed in. Javascript. Modern Javascript. VS Code uses Github's excellent open source javascript application framework Electron, which truly redefines the idea of "native software". Electron and the JS development model is an excellent way to build and package multi-platform software. Using Electron was a major, albeit welcome shift for Microsoft and the .NET community. 

VS Code, while in many respects is a "me too" with other non-bloated source code editors like Atom, it is the best .NET IDE on the market today. More important, VS Code signals the underlying OSS focused culture is sweeping over Microsoft. I'm excited for the Microsoft community.

### Addendum

Assume that Microsoft continues with this OSS track. What advantages will they have over the OSS community? What advantage does Google, Apple, Amazon, or Facebook have over the OSS community? Why would engineers want to program for Windows? For Exchange? For SQL Server? Why would I ever want to program with Microsoft's tools?



> Unlike traditional IDEs with everything but the kitchen sink, you can tune your installation to the development technologies you care about.

> Code has a high productivity code editor, when combined with programming language services, gives you the power of an IDE with the speed of a text editor. 

* Visual Studio Code is the "Anti Visual Studio". Minimal. Extension based. Command line focused. Built with JS. 

* Electron based. Minimal core + extensions (Atom).

* Inline reference information (metadata) is awesome. (Find dead code). See `editor.referenceInfos` setting.

* Git based, EOL default is `\n`. Unix wins. 

* Visual Studio Code was written with node.js, which is reflected in the documentation. There is a lot of javascript / node examples in the docs.

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

* Cmd-Shift-[ : Fold current block.
* Cmd-Shift-] : Unfold current block.
* F2 : Rename.
* F12 : Goto definition.
* Shift-F12 : Find all references.
* Opt-F12 : Peek definition.
* Cmd-Shift-M : Toggle Warnings / Errors

* `git config --global core.editor "code --wait"`


## "Old Microsoft" Idiotic Things

* The term `Visual C#` to correspond to `Visual Basic`. `Visual C#` is a horrible name. It's just `C#`. I give MS the benefit of the doubt with `Visual Basic` since there was a `BASIC` language as a predecessor and `Visual Basic` existed in non-.NET form prior to .NET. But `C#` does not have a predecessor. It was born with .NET. It just confuses everything - documentation, searching, etc. 


## .NET Tutorial

````
The standard folder structure (see `global.json`)
  src/
  test/
```

* `dotnet new -t lib` - create a new library project (in the dir you want to create the lib (`cd src/library`)
* `dotnet new -t xunittest` - create a new `xunittest` test project. 
* `dotnet restore` - restore dependencies
* `dotnet new` - create a new console app
