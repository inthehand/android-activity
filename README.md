# InTheHand.AndroidActivity
Provides a mechanism to determine the main activity in cross-platform projects

## Usage
In your cross-platform code you can reference this package. Query the AndroidActivity.CurrentActivity property to return the current activity on supported cross-platform frameworks.

On unsupported frameworks you can manually set the CurrentActivity by assigning it inside your platform-specific MainActivity.cs or similar.

## Supported Frameworks
 - Xamarin Forms
 - .NET MAUI
 - Uno Platform

![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/InTheHand.AndroidActivity)