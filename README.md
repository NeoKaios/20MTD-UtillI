# UtillI Library

A [BepInEx](https://github.com/BepInEx/BepInEx/releases) library for [20 Minutes Till Dawn](https://store.steampowered.com/app/1966900/20_Minutes_Till_Dawn/).

## Features

Offer a easy way for modders to display text during a game of 20MTD.

### Register yourself

Simply register your instance of `TextUpdater` and the position of the text you wish to display:
```csharp
using UtillI;
using UtillI.Examples;
UtillIRegister.Register(PanelPosition.BottomLeft, new ColorTextUpdater("white"));
```

## Contributions

This UI lib was originally inspired by the [BetterUI](https://github.com/sloverlord/BetterUI) mod of @sloverlord

## For modders

- Clone the [repo](https://github.com/NeoKaios/20MTD-GeneralUtilityMod)
- Open repo in VSCode
- Setup $GameDir variable in *GeneralUtilityMod.csproj*
- ```dotnet build``` to build and deploy mod
- ```dotnet publish``` to publish a .zip file