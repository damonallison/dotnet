# .NET Research

This repo contains research into the "new .NET" - .NET core, Visual Studio Code, and Microsoft's new OSS focused ecosystem.

Microsoft's culture has shifted from the 90's "embrace, extend, extinguish" to embracing the spirit of OSS. From a development model, they are decades behind Java. From a language perspective, C# is slightly more advanced than Java (lambdas, linq, TPL async / await), but still C# remains mostly a Java clone.

The changes made with what I'm calling this "new .NET" initiative revolve around opening up the ecosystem to better compete for engineering mindshare.

* Development done in the open, on github.
* Minimalist tools (VS Code) based on Electron, not "WPF" or another heavy MS UI toolchain.
* Creating linux ports of Server products (e.g., SQL Server).
* OSS, multi-platform, command line based by default.

## Questions

* What is the vibe around .NET Core from within Microsoft?
* What is the strategy around .NET Core from within Microsoft?
    * Why OSS? VS Code on Electron?
      * Shift to cloud?
      * Did they realize engineer mindshare can't stand Win32?
* When was .NET Core released and what is it's roadmap?
  * Released June 27, 2016.
  * What is the current status of .NET Core?

## Why .NET Core?

* Legacy .NET was straddled with Windows, IIS, and other obsolete technologies.
* Requests from customers to run web workloads on linux.
* Industry pressure. All web frameworks are open sourced. MS was losing hardcore engineer mindshare.
* Legacy C# applications required machine level installed .NET runtimes, IIS for web apps.
* The .NET BCL included obsolete, proprietary libraries and Windows platform hooks nobody needed for web apps.
* Was built for Windows desktop applications, which are obsolete.

#### .NET Core

* Multi-platform. 
* Containerization. The .NET Core runtime can be deployed side by side with your application. 
* Smaller, web focused BCL. Ditches native development (smart).
* Modular. Only download / distribute the packages you use.
* OSS, multi-platform, command line based.



## Links

* [Announcing .NET Core 1.0](https://blogs.msdn.microsoft.com/dotnet/2016/06/27/announcing-net-core-1-0/)

  * The initial introduction to .NET Core. Includes the history, story, goals, the platform landscape, and creating hello world.

  * > This is the biggest transformation of .NET since its inception and will define .NET for the next decade. We’ve rebuilt the foundation of .NET to be targeted at the needs of today’s world: highly distributed cloud applications, micro services and containers.


* [Getting started with .NET Core](https://docs.microsoft.com/en-us/dotnet/articles/core/index)
  * High level overview of the pieces and architecture of .NET core.


* [Getting started with .NET Core on macOS](https://docs.microsoft.com/en-us/dotnet/articles/core/tutorials/using-on-macos)
  * Tutorial on creating a project with a main project, library, and unit tests.

* [Building Your First Web API with ASP.NET Core MVC and Visual Studio](https://docs.asp.net/en/latest/tutorials/first-web-api.html)

* [Your First ASP.NET Core Application on a Mac Using Visual Studio Code](https://docs.asp.net/en/latest/tutorials/your-first-mac-aspnet.html)

## .NET Core : High level overview

.NET Core is a cross-platform version of .NET. .NET Core includes a .NET Runtime, framework libraries, SDK tools (compilers, SDK tools), and a `dotnet` app host capable of building / running .NET console and web (ASP.NET) applications.

.NET Core is cross platform. Therefore, most of Microsoft's proprietary frameworks (Winforms, registry, security model, windows workflow, etc) are not part of .NET Core. Microsoft is leaving behind the baggage that the traditional Windows / .NET environment brought with it (Visual Studio 200x, IIS, Registry, etc).

Most of .NET Core is shared code. There are platform specific implementations of lower level features, as necessary.

ASP.NET is the primary use case with .NET core. Microsoft has recognized that `native` client development is obsolete.

## Visual Studio Code

Visual Studio code is mostly a "me too" editor in a similar vain to Atom, Sublime, and other package based text editors. With a default install, it's a minimal core editor. The power of VS Code, like Atom and Sublime, comes via extensions. Extensions add large feature sets to the core - like language support, debugging, text formatting, theming, everything. Extensions are critical in VS Code. For example, you can't even program C# in VS Code without a C# extension. And yes, this is from Microsoft.

By requiring extensions for everything, it allows engineers to tailor the editor solely to the feature set they require without having to carry around anything they don't. While you could selectively pick the features Visual Studio included in it's install, VS Code relies completely on extensions and makes it trivial to add / remove and update extensions without Visual Studio's clumsy installer or heavy add-ins.

Stop for a moment and think about the cultural change that must have occurred within Microsoft to create VS Code. VS Code goes completely against 30 years of big, bloated, annually / bi-annually updated, and expensive software for Microsoft. Since many of today's engineers were in diapers, Microsoft has been releasing these massive, multi-gigabyte, $1000+ editors with meaningless names like "Visual Studio 2005 Enterprise Edition". And here we are, in 2016, with a radically modern IDE coming from the same company.

The differences between Visual Studio of old and VS Code is downright shocking. Visual Studio has an ugly, slow installer, a massive install footprint, multiple versions with extremely complex feature sets (Developer, Enterprise, Professional), lengthy release and update cycles (Release Candidate 1, 2), Windows only, developed in C++ using "native" Microsoft toolkits, and has a massive memory footprint.

VS Code is a simple, small install, has a single "version", is extension based, updated frequently, multi-platform, developed in Javascript, and has a small memory footprint.

This is radical, radical territory for Microsoft and is a leading indicator of what appears to be a massive cultural shift for them. Changing corporate culture is a massive feat that deserves great respect.

Equally interesting is the underlying technology in which VS Code is developed in. Javascript. Modern Javascript. VS Code uses Github's open source javascript application framework Electron, which modernizes the development of "native software". Electron and the JS development model is an excellent way to build and package multi-platform software. Using Electron was a major, albeit welcome shift for Microsoft and the .NET community.

VS Code, while in many respects is a "me too" with other non-bloated source code editors like Atom, it is the best .NET IDE on the market today. More important, VS Code signals the underlying OSS focused culture is sweeping over Microsoft.

### Addendum

Assume that Microsoft continues with this OSS track. What advantages will they have over the OSS community? What advantage does Google, Apple, Amazon, or Facebook have over the OSS community? Why would engineers want to program for Windows? For Exchange? For SQL Server? Why would I ever want to program with Microsoft's tools?

* VS Code takes the minimalist route.
> "Unlike traditional IDEs with everything but the kitchen sink, you can tune your installation to the development technologies you care about."
> Code has a high productivity code editor, when combined with programming language services, gives you the power of an IDE with the speed of a text editor.


## Common Language Infrastructure

From Essential C# 6.0 - notes on the CLI.

The common language infrastructure is the set of components required for a high level language to execute.

* CLI : Common Language Infrastructure. The specifications for CIL and runtime. Pieces of the CLI include:
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

* What types / namespaces make up the BCL?
  * What is the landscape for .NET Core projects?

* How do you read an assembly's manifest?
  * How does the assembly loader work?
  * How does NuGet work? (Read the source code)
  * How are assemblies resolved?

## .NET Core Tutorial

```
The standard folder structure (see `global.json`)
  src/
  test/
```

* `dotnet new -t lib` - create a new library project (in the dir you want to create the lib (`cd src/library`)
* `dotnet new -t xunittest` - create a new `xunittest` test project.
* `dotnet restore` - restore dependencies
* `dotnet new` - create a new console app
