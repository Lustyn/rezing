# rezing

re-implementation of an "amazing" game server

## Building

You are expected to already have the .NET CLI installed, and to install the required frameworks as needed during the build process.

Place the game's `Assembly-CSharp.dll` into a directory called  `libs`.

```sh
mkdir libs
cp /path/to/Assembly-CSharp.dll libs
```

Then simply build:

```sh
dotnet build
```

Or run:

```sh
dotnet run
```

## Development

Visual Studio Code is generally recommended for development. You will need the [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) extension for syntax highlighting and IntelliSense.
