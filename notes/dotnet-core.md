# .NET Core

## Links 

[.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
## Questions

* What is the vibe around .NET Core from within Microsoft? 
* What is the strategy around .NET Core from within Microsoft?
    * Why OSS? VS Code on Electron? 
        * Did they realize engineer mindshare can't stand Win32?
* When was .NET Core released and what is it's roadmap?



## .NET Tutorial

```
The standard folder structure (see `global.json`)
  src/
  test/
```

* `dotnet new -t lib` - create a new library project (in the dir you want to create the lib (`cd src/library`)
* `dotnet new -t xunittest` - create a new `xunittest` test project. 
* `dotnet restore` - restore dependencies
* `dotnet new` - create a new console app
