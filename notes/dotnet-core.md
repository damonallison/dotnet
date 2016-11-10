# .NET Core

## Links 

[.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)

## Questions

* What is the vibe around .NET Core from within Microsoft? 
* What is the strategy around .NET Core from within Microsoft?
    * Why OSS? VS Code on Electron? 
        * Did they realize engineer mindshare can't stand Win32?
* When was .NET Core released and what is it's roadmap?


## Strategy

#### Legacy .NET 

* Legacy .NET was straddled with Windows, IIS, and other obsolete technologies.
* Legacy C# applications required machine level installed .NET runtimes, IIS for web apps.
* The .NET BCL included obsolete, proprietary libraries and Windows platform hooks nobody needed for web apps.
* Was built for Windows desktop applications, which are obsolete.

#### .NET Core

* Containerization. The .NET Core runtime can be deployed side by side with your application.
* Smaller, web focused BCL.


## Project

* An ASP.NET application, packaged with the runtime / dependencies, running in a docker container.


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
