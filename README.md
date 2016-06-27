# .NET Research

The .NET landscape is drastically changing (for the better). This repo contains research into the "new .NET" - .NET core, Visual Studio Code, and anything .NET "vNext".

## TODO


* Visual Studio Code Documentation
  * https://code.visualstudio.com/Docs
* [xUnit](https://xunit.github.io/)
  * Does `xUnit` have Xcode's concept of expectations / async execution?

* Assertions, logging output, and debugging diagnostics.

* TPL
* Code : remove whitespace on save (extension?)
* Git integration.
* How to use VS Code as a git merge tool?
* File search searches thru `.IncrementalCache` and other non-code objects.

## Common Themes

* All OSS. Everything is developed in the open, on Github. Including Visual Studio Code.

* Modularization.
  * Fully self-contained application distributions possible (containerable). Eliminates versioning problems.
  * "CoreFX" (the standard library) is a modular collection of libraries. You can include only the framework components you need.

* Microsoft.NetCore.App : included with the runtime. Includes base types, Console, Task and other "necessary" types. This is the foundation of .NET.

## Visual Studio Code

Visual Studio code is mostly a "me too" editor in a similar vain to Atom, Sublime, and other package based text editors. With a default install, it's a minimal core editor. The power of VS Code, like Atom and Sublime, comes via extensions. Extensions add large feature sets to the core - like language support, debugging, text formatting, theming, everything. Extensions are critical in VS Code. For example, you can't even program C# in VS Code without a C# extension. And yes, this is from Microsoft.

By requiring extensions for everything, it allows engineers to tailor the editor solely to the feature set they require without having to carry around anything they don't. While you could selectively pick the features Visual Studio included in it's install, VS Code relies completely on extensions and makes it trivial to add / remove and update extensions without Visual Studio's clumsy installer or heavy add-ins.

Stop for a moment and think about the cultural change that must have occurred within Microsoft to create VS Code. VS Code goes completely against 30 years of big, bloated, annually / bi-annually updated, and expensive software for Microsoft. Since many of today's engineers were in diapers, Microsoft has been releasing these massive, multi-gigabyte, $1000+ editors with meaningless names like "Visual Studio 2005 Enterprise Edition". And here we are, in 2016, with a radically modern IDE coming from the same company. 

The differences between Visual Studio of old and VS Code is downright shocking. Visual Studio has an ugly, slow installer, a massive install footprint, multiple versions with extremely complex feature sets (Developer, Enterprise, Professional), lengthy release and update cycles (Release Candidate 1, 2), Windows only, developed in C++ using "native" Microsoft toolkits, and has a massive memory footprint.

VS Code is a simple, small install, has a single "version", is extension based, updated frequently, multi-platform, developed in Javascript, and has a small memory footprint.

This is radical, radical territory for Microsoft and is a leading indicator of what appears to be a massive cultural shift for them. Changing corporate culture is a massive feat that deserves great respect.

Equally interesting is the underlying technology in which VS Code is developed in. Javascript. Modern Javascript. VS Code uses Github's excellent open source javascript application framework Electron, which truly redefines the idea of "native software". Electron and the JS development model is an excellent way to build and package multi-platform software. Using Electron was a major, albeit welcome shift for Microsoft and the .NET community. 

VS Code, while in many respects is a "me too" with other non-bloated source code editors like Atom, it is the best .NET IDE on the market today. More important, VS Code signals the underlying OSS focused culture is sweeping over Microsoft. I'm excited for the Microsoft community.



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

* The term `Visual C#` to correspond to `Visual Basic`. `Visual C#` is a horrible name. It's just `C#`. I give MS the benefit of the doubt with `Visual Basic` since there was a `BASIC` language as a predecessor. But `C#` does not have a predecessor. It just confuses everything - documentation, searching, etc. 

 