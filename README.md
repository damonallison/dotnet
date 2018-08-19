# .NET

This repo contains research into Microsoft's *new* [.NET](https://github.com/Microsoft/dotnet) strategy.

* .NET Core
* Visual Studio Code
* Microsoft's OSS focus

## The "new" .NET

Microsoft, under Nadella, has shifted its culture from the 90's "embrace, extend, extinguish" to embracing OSS. From a development model, it is similar to Java. Platform agnostic, VM based, Open Source. From a language perspective, C# and the CLR has traditionally been ahead of Java (lambdas, linq, TPL async / await).
At some point, perhaps under the direction of Nadella, Microsoft realized Win32 is a dead end. They pivoted from Ballmer's "Windows Everywhere" to "Cloud and AI everywhere". They opened up their developer tools to all platforms. Thus, .NET Core. The development model moved from the cathedral to the bazaar.

* Development done in the open, on github.
* Minimalist tools (VS Code) based on Electron, not "WPF" or another heavy MS UI toolchain.
* Creating linux ports of Server products (e.g., SQL Server).
* OSS, multi-platform, command line based by default.

## Why .NET Core?

* Legacy .NET was straddled with Windows, IIS, Active Directory, and other obsolete technologies.
* Legacy C# applications required machine level installed .NET runtimes, IIS for web apps.
* The .NET BCL included obsolete, proprietary libraries and Windows platform hooks (WCF, WPF) nobody needs for web apps.
* Was built for Windows desktop applications, which are obsolete.

### .NET Core Major Features

* Cross platform
* Containerization. The .NET Core runtime can be deployed side by side with your application.
* Smaller, web focused BCL. Ditches native development (smart).
* Modular. Only download / distribute the packages you use.
* OSS, multi-platform, command line based.


## Links

* [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)

## .NET Core

### Installation

Install it thru `brew`.

```
$ brew cask install dotnet-sdk
$ brew cask install dotnet
```

### Overview

.NET Core is a cross platform implementation of .NET. .NET Core includes a .NET Runtime, framework libraries, and a `dotnet` CLI capable of building / running .NET console and web (ASP.NET) applications.

Because of it's cross platform nature, Microsoft's Win32 proprietary frameworks and libraries (Winforms, registry, security model, WPF, WCF, IIS, etc) are not part of .NET Core. Other than enabling .NET on multiple OSs, omitting these proprietary win32 hooks lightens up .NET Core's footprint.

Most of .NET Core is shared code. There are platform specific implementations of lower level features (I/O, kqueue), as necessary. .NET Core abstracts away the underlying OS.

Web and server applications are the primary use case with .NET core. Microsoft has recognized that `native` client development is obsolete.

### .NET Core Projects

#### [CoreCLR]() && [CoreFX]()

CoreCLR contains the runtime and low level primitives (`System.Object`) required for .NET. You do not use CoreCLR directly. CoreCLR is exposed by CoreFX. CoreFX is the `BCL`. It is included with every project.

CoreCLR is the set of components required for a high level language to execute. CoreCLR includes:

* CLI : Common Language Infrastructure.
  * The specifications for CIL and runtime.
  * Virtual Execution System (runtime).
  * Common Intermediate Language (CIL).
  * Common Type System.
    * Defines how value types and object types are laid out in memory.
  * Common Language Specification.
    * A subset of the CTS which you must adhere to in order to use a type from multiple source languages.
    * Add an assembly attribute of `System.CLSCompliant` and the compiler will warn on CLS violations.
  * BCL (framework).

* AppDomains are logical "processes" which execute within the same physical HW process.
  * AppDomains were created because physical processes are deemed expensive.
  * Communication between app domains requires cross-process like marshalling and communication mechanisms.

* Assemblies
  * Assemblies are the smallest unit which can be versioned.
  * Assemblies reference other assemblies. References are stored in the manifest.
  * Assemblies can be composed from multiple modules (using al.exe). However VS.NET does not recognize multi-module assemblies.
  * Do *NOT* store assemblies into the Global Assembly Cache (GAC). Entire applications should be contained in a single directory.


#### .NET Standard

The .NET Standard is a set of libraries and classes all implemenations of .NET must support. By targeting .NET Standard, you can be certain your library will work with any .NET Standard compliant implementation.

All libaries you write should target .NET Standard for maximum compatibility.

## Visual Studio Code

Visual Studio code is mostly a "me too" editor in a similar vain to Atom, Sublime, and other package based text editors. With a default install, it's a minimal core editor. The power of VS Code, like Atom and Sublime, comes via extensions. Extensions add large feature sets to the core - like language support, debugging, text formatting, theming, everything. Extensions are critical in VS Code. For example, you can't even program C# in VS Code without a C# extension.

By requiring extensions for everything, it allows engineers to tailor the editor solely to the feature set they require without having to carry around anything they don't. While you could selectively pick the features Visual Studio included in it's install, VS Code relies completely on extensions and makes it trivial to add / remove and update extensions without Visual Studio's clumsy installer or heavy add-ins.

Stop for a moment and think about the cultural change that must have occurred within Microsoft to create VS Code. VS Code goes completely against 30 years of big, bloated, annually / bi-annually updated, and expensive software for Microsoft. Since many of today's engineers were in diapers, Microsoft has been releasing these massive, multi-gigabyte, $1000+ editors with meaningless names like "Visual Studio 2005 Enterprise Edition". And here we are, in 2016, with a radically modern IDE coming from the same company.

The differences between Visual Studio of old and VS Code is downright shocking. Visual Studio has an ugly, slow installer, a massive install footprint, multiple versions with extremely complex feature sets (Developer, Enterprise, Professional), lengthy release and update cycles (Release Candidate 1, 2), Windows only, developed in C++ using "native" Microsoft toolkits, and has a massive memory footprint.

VS Code is a simple, small install, has a single "version", is extension based, updated frequently, multi-platform, developed in Javascript, and has a small memory footprint.

This is radical, radical territory for Microsoft and is a leading indicator of what appears to be a massive cultural shift for them. Changing corporate culture is a massive feat that deserves great respect.

Equally interesting is the underlying technology in which VS Code is developed in. Javascript. Modern Javascript. VS Code uses Github's open source javascript application framework Electron, which modernizes the development of "native software". Electron and the JS development model is an excellent way to build and package multi-platform software. Using Electron was a major, albeit welcome shift for Microsoft and the .NET community.

VS Code, while in many respects is a "me too" with other non-bloated source code editors like Atom, it is the best .NET IDE on the market today. More important, VS Code signals the underlying OSS focused culture is sweeping over Microsoft.

## .NET Core Tutorial Notes

```bash

# Create a new console application in a directory called MyApp

$ dotnet new console -o MyApp


The standard folder structure (see `global.json`)
  src/
  test/
```

* `dotnet new -t lib` - create a new library project (in the dir you want to create the lib (`cd src/library`)
* `dotnet new -t xunittest` - create a new `xunittest` test project.
* `dotnet restore` - restore dependencies
* `dotnet new` - create a new console app

* `project.json` is moving to `csproj`. MS was not able to move all tooling to `project.json`.
