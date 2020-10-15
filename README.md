# Csharp_KSPHelper
 Pomocník pro práci s [KSP](https://ksp.mff.cuni.cz/) datasety v .NET

 ## Použití
 1. stažení knihovny
    - stáhni si [knihovnu](https://github.com/pajasry/Csharp_KSPHelper/raw/master/KSPHelper/bin/Release/netstandard2.0/KSPHelper.dll)
    - zkopruj si knihovnu(.dll) ke svému projektu
    - v Solution Explorer klikni pravým na _Dependencies->Add project reference_
    - Vyber .dll soubor
2. Naimportuj knihovnu
    - na začátek programu přidej direktivitu using
    ```csahrp
    using KSPHelperLibrary;
    ```
3. Deklarace
    ```csharp
        KSPHelper helper = new KSPHelper(@"C:\cesta\k\souboru");
    ```
    - pokud zadáš špatnou cestu, můžou vyskočit Exception
    - helper podporuje jenom __.in__ 
4. Práce s daty
    - data jsou uložena v __List<string[]> Collection__
    - to je list polí stringů, kde prvky jsou jednotlivá čísla z řádku
    ```
    3
    1 0 2
    3 5 4
    7 5 3
    ```
    bude načteno jako
    ```csharp
    List<string[]> = {
        {1,0,2},
        {3,5,4},
        {7,5,3}
    }
    ```
    - první řádek je v poli firstLine
5. Výstup
    - knihovna pomáha i s výstupy
    ```csharp
    helper.AddOutput("ANO");
    helper.AddOutput("NE");
    ```
    _data.out_ budou obsahovat
    ```
    ANO
    NE
    ```

## Tipy - triky
#### Doslovný identifikátor [@](https://docs.microsoft.com/cs-cz/dotnet/csharp/language-reference/tokens/verbatim)
 vše za @ je bráno doslova. Lze tak použít klíčová slova jako názvy nebo psát cesty bez potřeby escapování backslashe
#### Iteruje všechna data
```csharp
    KSPHelper helper = new KSPHelper(@"C:\cesta");
    foreach(var line in helper.Collection)
        foreach(var value in line)
            Console.WriteLine(value);
```
#### Převede List na 2D pole
```csharp
    KSPHelper helper = new KSPHelper(@"C:\cesta");
    var data = helper.Collection.ToArray();
```
#### Zvýšení rychlosti - buffrování a vypsání až na přání
```csharp
    KSPHelper helper = new KSPHelper(@"C:\cesta");
    helper.LiveWrite = false;
    helper.AddOutput("Ano");
    helper.AddOutput("Ne");
    helper.FlushOutput();//vystup se ulozi az tady
```
