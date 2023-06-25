# UtillI Library

A [BepInEx](https://github.com/BepInEx/BepInEx/releases) library for [20 Minutes Till Dawn](https://store.steampowered.com/app/1966900/20_Minutes_Till_Dawn/).

## Features

Offer a easy way for modders to display text during a game of 20MTD.

### Create a Registration class
```csharp
using UtillI;
public class ExampleRegistration : Registration
{
    public ExampleRegistration() : base(PanelPosition.BottomLeft) { }
    override public string GetUpdatedText()
    {
        return "Some text";
    }
}
```
For more example look at `UtillI.Examples`.
### Register yourself

Simply register your instance of `Registration` to `UtillIRegister`:
```csharp
using UtillI;
using UtillI.Examples;
UtillIRegister.Register(new ExampleRegistration());
```

## Contributions

This UI lib was originally inspired by the [BetterUI](https://github.com/sloverlord/BetterUI) mod of @sloverlord

## For modders

- Clone the [repo](https://github.com/NeoKaios/20MTD-UtillI)
- Open repo in VSCode
- Setup $GameDir variable in *UtillI.csproj*
- ```dotnet build``` to build and deploy mod
- ```dotnet publish``` to publish a .zip file