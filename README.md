# .NET Research

This repo contains research into the "new .NET" - .NET core, Visual Studio Code, and Microsoft's new OSS focused ecosystem.

Microsoft's culture has shifted from the 90's "embrace, extend, extinguish" to embracing the spirit of OSS.

* Development done in the open, on github.
* Minimalist tools (VS Code) based on Electron, not "WPF" or another heavy MS UI toolchain. 
* Creating linux ports of Server products (e.g., SQL Server).
* OSS, multi-platform, command line based by default.

## Links

* [Getting started with .NET Core](https://docs.microsoft.com/en-us/dotnet/articles/core/index) 
  * High level overview of the pieces and architecture of .NET core.

* [Getting started with .NET Core on macOS](https://docs.microsoft.com/en-us/dotnet/articles/core/tutorials/using-on-macos)
  * Tutorial on creating a project with a main project, library, and unit tests.

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

> Unlike traditional IDEs with everything but the kitchen sink, you can tune your installation to the development technologies you care about.

> Code has a high productivity code editor, when combined with programming language services, gives you the power of an IDE with the speed of a text editor. 
