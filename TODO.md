# TODO

## Suggestions

* `dotnet test` verbose output.
* A way to reformat a comment block with a max line length of 80. (Provided via an extension?)

## Reading

### .NET Core

* Read : [Visual Studio Code Documentation](https://code.visualstudio.com/Docs)
* Blog post : .NET ecosystem
    * https://blogs.msdn.microsoft.com/dotnet/2016/06/27/announcing-net-core-1-0/
    > "Our lab runs show that ASP.NET Core is faster than some of our industry peers. We see throughput that is 8x better than Node.js and almost 3x better than Go, on the same hardware.

## Projects

* Micro-service web api in .NET core.
    * Finish [Your first ASP.NET Core Application](https://docs.asp.net/en/latest/tutorials/your-first-mac-aspnet.html).
    * Deployed to Azure.
    * Accessing a postgres DB.
    * username / password based auth against a "password" table.
    * oauth

## C#

* C# documentation comment syntax / generation.
* Assertions, logging output, and debugging diagnostics.
* Attributes

## BCL

* Core interface types IEquatable, IFormattable
* System.Collections.Generic
* System.Configurationj
* System.Data (high level classes)
* System,IO
* System.Linq
* System.Reflection
* System.Threading
* System.Threading.Tasks


* Serialization (`ISerializable` and `[Serializable]`) - look into JSON.NET.
* Rx
* Data access.
    * Is there heavy use of ORMs? Entity framework?
* Object equality : Find a decent library to generate proper hash code / equality logic.

## Unit testing

* [xUnit](https://xunit.github.io/)
    * Does `xUnit` have Xcode's concept of expectations / async execution?
    * Read up on xUnit `Fact` and `Theory` attributes.

* How to print the name of each test start / execution / end so any `Console.WriteLine` statements are part of the output.

## Tooling

* Generate documentation comments from source code.
* Git integration.
* How to use VS Code as a git merge tool?
