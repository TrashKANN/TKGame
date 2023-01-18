# TKGame

## About
This is a game created by TrashKANN using C# and the MonoGame framework. Releases currently only support x64 Windows.

## Running the game
1. Download TKGame-\<version\>-win-x64.zip from the latest release.
2. Unzip/Uncompress the file.
3. Run TKGame.exe found in the TKGame-win-x64 folder.

## Style guide
We are using Microsoft's style guide, which can be found [here](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)  
We are also aligning initializer lists so that they're easier to read. For example:
```csharp
Dictionary<TypeA, TypeB> variableName = new Dictionary<TypeA, TypeB>()
{
    { Key1,                  Value1            },
    { Key2,                  ValueWithLongName },
    { KeyWithReallyLongName, Value3            },
};
```