
Dotnet Dependecies:
[net8.0]: 
   Top-level Package             Requested   Resolved
   > Avalonia                    11.0.10     11.0.10 
   > Avalonia.Desktop            11.0.10     11.0.10 
   > Avalonia.Diagnostics        11.0.10     11.0.10 
   > Avalonia.Fonts.Inter        11.0.10     11.0.10 
   > Avalonia.ReactiveUI         11.0.10     11.0.10 
   > Avalonia.Themes.Fluent      11.0.10     11.0.10 
   > CommunityToolkit.Mvvm       8.2.2       8.2.2

Aplikacja napisana w C#

Implementuje podstawowa mechanike gry Rummikub dla jednego gracza
Drzewo implementacyjne jest w stanie takim jak na poczatku
Zostalo to jedynie polaczane z interfejsem graficznym Avalonia UI

W aktualnej formie mozemy swobodnie podnosic i wkladac bloczki
do zbiorow, z zachowanie zasady Serii lub grupy, jak w opisie

Aktualnie aby zmienic wyglad nalezy manewrowac kodem w pliku
ViewModels/MainViewModel.cs
przez aktualizacje lista_setow (i w przyszlosci lista_playerow)