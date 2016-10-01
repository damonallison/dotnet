# Visual Studio Code

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

## ASP.NET 

* [Your First ASP.NET Core Application on a Mac Using Visual Studio Code](https://docs.asp.net/en/latest/tutorials/your-first-mac-aspnet.html)

### Tools

* [Yeoman - web code generator](http://yeoman.io/)
* [Bower - web package manager](https://bower.io/)
* [Kestrel - Web Server]()
> brew install node
> npm install -g yo generator-aspnet bower
