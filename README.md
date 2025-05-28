# Gra Snake w C# (konsola)

To jest prosta gra typu Snake napisana w języku C# z użyciem konsoli. Głównym celem gry jest poruszanie się wężem, zbieranie jedzenia i unikanie zderzeń ze ścianami oraz z samym sobą.

## Jak gra działa?

- Wąż zaczyna w środku planszy i porusza się automatycznie w jednym z czterech kierunków: góra, dół, lewo, prawo.
- Gracz steruje wężem za pomocą klawiszy strzałek na klawiaturze.
- Na planszy losowo pojawia się "jedzenie" oznaczone symbolem `*`.
- Za każde zjedzone jedzenie gracz zdobywa 1 punkt, a wąż rośnie.
- Gra kończy się, gdy wąż:
    - zderzy się ze ścianą (krawędzią planszy),
    - uderzy sam w siebie.

## Sterowanie

- **Strzałka w górę** – poruszanie się w górę
- **Strzałka w dół** – poruszanie się w dół
- **Strzałka w lewo** – poruszanie się w lewo
- **Strzałka w prawo** – poruszanie się w prawo

Wąż nie może zawrócić bezpośrednio w przeciwnym kierunku (np. z PRAWO na LEWO).

## Wymagania

- System operacyjny: Windows (gra korzysta z konsoli systemowej)
- .NET SDK (np. .NET 6 lub nowszy)
- Kompilator C# (np. `csc.exe` lub Visual Studio)

## Jak uruchomić?

1. Upewnij się, że masz zainstalowane .NET SDK:  
   Możesz pobrać ze strony: https://dotnet.microsoft.com/

2. Następnie w głównym katalogu folderu (SnakeGame) użyj komendy w terminalu

 ```bash
 dotnet run

