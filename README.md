# .NET

My goal with this repo was to build a comprehensive set of examples covering C#
language and .NET base class library fundamentals. **It's not being maintained
at all**. I was planning to delete the repo, but the tests and notes may have
some value to someone at some point.

Most of the code was written in 2016, when I started working at C.H. Robinson,
which used .NET almost exclusively at the time. Since I hadn't worked in C# for
a while (I was using Java for 6 years prior to C.H. at Code42), I wanted to ramp
back up on .NET.

I didn't end up really scratching the surface of .NET. C# and the .NET framework
was 15 years old at the time. C# itself is a massive language and the framework
is large. Add to that comppon 3rd party libraries and I never ended up close to
having a comprehensive set of tests or examples around any of it.

## Contents

This repo contains two .net core projects:

* A small command line application.
* A set of unit tests which demonstrate core language and base class library usage.

## Running the code

Running the command line app:

```shell
$ dotnet run --project src/app/app.csproj
```

Running the unit tests:

```shell
$ dotnet test
```
