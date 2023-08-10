# InTheHand.AndroidActivity
Provides a mechanism to store a reference to the main activity in cross-platform projects

## Usage
The library is only required on Android, but can be used by cross-platform libraries such as 32feet.NET in Xamarin or .NET MAUI projects. You need to set the CurrentActivity by assigning it inside your platform-specific MainActivity.cs or similar.

## Supported Frameworks
 - Xamarin Forms
 - .NET MAUI
 - Uno Platform
 - Xamarin Android
 - .NET Android (non-MAUI)
 - Avalonia UI
 
 Set the current Activity in your MainActivity OnCreate method:-
 
```
protected override void OnCreate(Bundle savedInstanceState)
{
   // provide reference to current activity
   InTheHand.AndroidActivity.CurrentActivity = this;

   base.OnCreate(savedInstanceState);
}
```

[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/InTheHand.AndroidActivity)](https://www.nuget.org/packages/InTheHand.AndroidActivity)
