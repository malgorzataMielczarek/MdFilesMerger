# MdFilesMerger
<details>
<summary>Program description in English</summary>
Application to merge all .md files in a set directory (and its subdirectories) into one file. 

## Dictionary
1. **Main directory** - catalog in which program will search for .md files to merge. Program will search in this directory and all its subdirectories.
2. **Selected files** - all .md files that was found in main directory (and its subdirectories).
3. **File title** - first line of file that is a header (`# file title`) or next if first line(s) is\are empty.
4. **Merged file** - new file that contains title, table of contents (if you choose it) and content of all selected files. If selected files contain file with the same name (full path) as merged file, its content is not included in this file (old file is deleted and new one is created) and that file will be removed from selected files list during merged file creating. It will also cause recreating of table of contents (if it was selected), so it no longer contains headers associated with file removed from list.

## Program start view
When launched, the program will take you directly to the [main menu view](#main-menu-view), unless default main directory (stored in `MAIN_DIRECTORY_PATH` constant of the `Program` class) does not exist on your computer. In that case you will by asked to enter main directory path first. So first view that you will see, will be the [main directory change view](#main-directory-change-view).

## Main menu view
This is the main view of the program. It contains list of the main functionalities of the program. You will be transferred to it after completing any functionality, unless you exit the entire program.

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

-------------------------------------------------------MENU GŁÓWNE------------------------------------------------------


1. Zmień katalog główny
2. Wyświetl listę plików do scalenia
3. Utwórz spis treści
4. Scal pliki

Podaj numer czynności z powyższego menu, którą chcesz wykonać lub wciśnij Esc, aby zakończyć działanie programu:
```

At the top, after colon, you can see the absolute path of currently selected main directory. Then we have the title of the view and a list of the main functionalities of the program. And so:
* If you want to change main directory type **_1_**, and you will be taken to the previous view ([main directory change view](#main-directory-change-view)), to enter the new path.
* If you want to see what files will be used in the merge type **_2_**, and you will be taken to [list of selected files view](#list-of-selected-files-view), to see a list of selected files.
* If you want to place a table of contents at the beginning of the merged file (after the merged file title) type **_3_**, and you will be taken to [table of contents menu view](#table-of-contents-menu-view), to select one of table of contents types.
* If you want to create the merged file type **_4_**, and you will be taken to [the merged file menu view](#merged-file-menu-view) to possibly change the settings of the merged file and create it.
* If you want to close the program press **Esc** button.
* If you enter anything else, an error message will by displayed and you will be asked to choose again, what you want to do.

## Main directory change view
This view will be shown to you in one of the following situations:
1. After starting the program if directory of path stored in `MAIN_DIRECTORY_PATH` `Program` class constant doesn't exists on your computer,
2. If you chose '1' in [main menu view](#main-menu-view).

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

-----------------------------------USTAW KATALOG, W KTÓRYM CHCESZ WYSZUKIWAĆ PLIKI .MD----------------------------------


Wprowadź ścieżkę dostępu do katalogu:
```

At the top, after colon, you will see the absolute path of currently selected main directory (unless you are here because of the first situation, than this line will be omitted). Now enter the path of your chosen directory that you want to set as main directory. The path can be absolute or relative to current directory (probably the directory where program is located - presumably MdFilesMerger\bin\Debug\net6.0 subfolder of the project or another directory where the program was executed from). If the entered path exists on your computer you will be taken to the previous view ([main menu view](#main-menu-view)). If the directory of the given path does not exist, an error message will appear and you will be asked to re-enter the path.

## List of selected files view
This view will be shown to you after choosing '2' in the [main menu view](#main-menu-view). As in the previous views, at the top of the widow you can see the absolute path of the curren main directory. Below is a title of the view and a list of selected files. Files are displayed as relative paths to the main directory. Files are sorted alphabetically by name, but if a directory contains several subdirectories that have the same name except of a number at the end, then they are sorted by those numbers in ascending order. The selected files will be added to the merged file in that order. At the bottom is located information, to press Enter to go back to the [main menu](#main-menu-view) or Esc to completely exit the program. So if you want to go back to [main menu](#main-menu-view) press **Enter** (or any other key except Esc). If you press **Esc** the entire program will be closed.

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

------------------------------------------------LISTA PLIKÓW DO SCALENIA------------------------------------------------


\README.md
\Tydzien1\Bonus1\BONUS1-TworzenieRepozytoriumGitHub.md
\Tydzien1\Bonus2\BONUS2-PodstawyPracyZGITem.md
\Tydzien1\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien1\Lekcja2\LEKCJA2-PlanGry.md
\Tydzien1\Lekcja3\LEKCJA3-JakStudiowacTenKurs.md
\Tydzien1\Lekcja4\LEKCJA4-CoMusiszUmiecZanimPrzejdziaszDalej.md
\Tydzien1\Lekcja5\LEKCJA5-CzymJestDotNET.md
\Tydzien1\Lekcja6\LEKCJA6-Wymagania-potrzebneOprogramowanie.md
\Tydzien1\Lekcja7\LEKCJA7-TwojPierwszyProgram.md
\Tydzien1\Lekcja8\LEKCJA8-JakPracowacZVisualStudio.md
\Tydzien1\Lekcja9\LEKCJA9-KonwencjePisania.md
\Tydzien1\Lekcja10\LEKCJA10-Kompilator.md
\Tydzien1\Lekcja11\LEKCJA11-Debugowanie.md
\Tydzien1\Lekcja12\LEKCJA12-BledyPoczatkujacych.md
\Tydzien1\Lekcja13\LEKCJA13-PracaDomowa.md
\Tydzien2\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien2\Lekcja2\LEKCJA2-ZmienneIStale.md
\Tydzien2\Lekcja3\LEKCJA3-TypyWartosciowe.md
\Tydzien2\Lekcja4\LEKCJA4-TypyReferencyjne.md
\Tydzien2\Lekcja5\LEKCJA5-Warunki.md
\Tydzien2\Lekcja6\LEKCJA6-Operatory.md
\Tydzien2\Lekcja7\LEKCJA7-OperatoryLogiczne.md
\Tydzien2\Lekcja8\LEKCJA8-Petle.md
\Tydzien2\Lekcja9\LEKCJA9-InstrukcjeSkoku.md
\Tydzien2\Lekcja10\LEKCJA10-Tablice.md
\Tydzien2\Lekcja11\LEKCJA11-Listy.md
\Tydzien2\Lekcja12\LEKCJA12-Enum.md
\Tydzien2\Lekcja13\LEKCJA13-KlasyIObiekty.md
\Tydzien2\Lekcja14\LEKCJA14-Metody.md
\Tydzien2\Lekcja15\LEKCJA15-ParametryMetod.md
\Tydzien2\Lekcja16\LEKCJA16-PolaIWłaściwości.md
\Tydzien2\Lekcja17\LEKCJA17-ZakresyWidoczności.md
\Tydzien2\Lekcja18\LEKCJA18-PiszemyAplikację.md
\Tydzien2\Lekcja19\LEKCJA19-BledyPoczatkujacych.md
\Tydzien2\Lekcja20\LEKCJA20-PracaDomowa.md
\Tydzien3\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien3\Lekcja2\LEKCJA2-Konstruktory.md
\Tydzien3\Lekcja3\LEKCJA3-Przeciazenia.md
\Tydzien3\Lekcja4\LEKCJA4-Dziedziczenie.md
\Tydzien3\Lekcja5\LEKCJA5-Polimorfizm.md
\Tydzien3\Lekcja6\LEKCJA6-Hermetyzacja.md
\Tydzien3\Lekcja7\LEKCJA7-KlasyAbstrakcyjne.md
\Tydzien3\Lekcja8\LEKCJA8-Interfejsy.md
\Tydzien3\Lekcja9\LEKCJA9-TypyGeneryczne.md
\Tydzien3\Lekcja10\LEKCJA10-Refaktoryzacja.md
\Tydzien3\Lekcja11\LEKCJA11-BledyPoczatkujacych.md
\Tydzien3\Lekcja12\LEKCJA12-PracaDomowa.md
\Tydzien4\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien4\Lekcja2\LEKCJA2-ProjektZTestami.md
\Tydzien4\Lekcja3\LEKCJA3-TwojPierwszyTest.md
\Tydzien4\Lekcja4\LEKCJA4-TestyJednostkowe.md
\Tydzien4\Lekcja5\LEKCJA5-Moq.md
\Tydzien4\Lekcja6\LEKCJA6-FluentAssertions.md
\Tydzien4\Lekcja7\LEKCJA7-PokrycieKoduTestami.md
\Tydzien4\Lekcja8\LEKCJA8-TDD.md
\Tydzien4\Lekcja9\LEKCJA9-TestyIntegracyjne.md
\Tydzien4\Lekcja10\LEKCJA10-BledyPoczatkujacych.md
\Tydzien4\Lekcja11\LEKCJA11-PracaDomowa.md
\Tydzien5\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien5\Lekcja2\LEKCJA2-KolekcjeWdotNET.md
\Tydzien5\Lekcja3\LEKCJA3-IQueryableIIEnumerable.md
\Tydzien5\Lekcja4\LEKCJA4-LINQPodstawy.md
\Tydzien5\Lekcja5\LEKCJA5-ManipulacjePlikami.md

Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.
```

## Table of contents menu view
You will be taken to this view, if you choose '3' in [main menu view](#main-menu-view). As in the previous views, the path to the current main directory is shown at the top. Then after the title you can see the menu. The program will ask you to select the type of table of contents, that you want to add to your merged file. You can choose one of three options:
1. Type **_1_** if you want your table of contents to be plain text, or more specifically a set of appropriate level headers. Selecting this option will take you to [view](#plain-text-table-of-contents-view), which will create such a table of contents and display it.
2. Type **_2_** if you want your table of contents to contain hyperlinks in the file titles headers, instead of plain text. Later, that will allow you to click them and go directly to the fragment of the merged file that contains contents of the corresponding file. Selecting this option will take you to [view](#hyperlinks-table-of-contents-view), which will create and display the text of this type of table of contents.
3. Type **_3_** if you changed your mind and don't want a table of contents in your merged file. Selecting this option will take you to [no table of contents view](#no-table-of-contents-view), where information about the lack of a table of contents will be displayed.

When you select option '1' or '2', the table of content of the selected type (the one that you selected last, if you have visited this view several times) will be added to the merged file, if you create one later.

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------


1. Spis treści będący zwykłym tekstem
2. Spis treści złożony z hiperlinków do odpowiednich paragrafów
3. Bez spisu treści

Podaj numer typu wybranego z powyższego menu:
```

If you enter anything else then '1', '2' or '3', an error message will appear and you will be prompted to re-select an option.

### Plain text table of contents view
You will be taken to this view if you choose option '1' in [previous view](#table-of-contents-menu-view). As in the previous views, at the top of the window is the absolute path to the main directory. Below is the text of the table of contents. It will contain second-level header with the title of the table of contents (`## Spis treści`), followed by a list of headers of appropriate level. Each header is either the name of subdirectory (if there is more then one selected file in that subdirectory) or the title of the selected file (if the file has no title, it is replaced by the filename). So first we have the headers with the titles of the files that are placed directly in the main directory, then the name of the subfolder (if there is more then one selected file in this subfolder), the titles of the files that are in this subfolder, and so on.

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------


## Spis treści
### Kurs "Zostań programistą ASP.NET" - notatki
### Tydzien1
#### BONUS 1 - Tworzenie Repozytorium GitHub
#### BONUS 2 - Podstawy pracy z GITem
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Plan gry
#### LEKCJA 3 - Jak studiować ten kurs
#### LEKCJA 4 - Co musisz umieć zanim przejdziesz dalej
#### LEKCJA 5 - Czym jest .NET
#### LEKCJA 6 - Wymagania (potrzebne oprogramowanie)
#### LEKCJA 7 - Twój pierwszy program
#### LEKCJA 8 - Jak pracować z Visual Studio
#### LEKCJA 9 - Konwencje pisania (Dobre praktyki programowania)
#### LEKCJA 10 - Kompilator
#### LEKCJA 11 - Debugowanie
#### LEKCJA 12 - Błędy początkujących
#### LEKCJA 13 - Praca domowa
### Tydzien2
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Zmienne i stałe
#### LEKCJA 3 - Typy wartościowe
#### LEKCJA 4 - Typy referencyjne
#### LEKCJA 5 - Warunki
#### LEKCJA 6 - Operatory
#### LEKCJA 7 - Operatory Logiczne
#### LEKCJA 8 - Pętle
#### LEKCJA 9 - Instrukcje skoku
#### LEKCJA 10 - Tablice
#### LEKCJA 11 - Listy
#### LEKCJA 12 - Enum
#### LEKCJA 13 - Klasy i obiekty
#### LEKCJA 14 - Metody
#### LEKCJA 15 - Parametry metod
#### LEKCJA 16 - Pola i właściwości
#### LEKCJA 17 - Zakresy widoczności
#### LEKCJA 18 - Piszemy aplikację
#### LEKCJA 19 - Błędy początkujących
#### LEKCJA 20 - Praca domowa
### Tydzien3
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Konstruktory
#### LEKCJA 3 - Przeciążenia
#### LEKCJA 4 - Dziedziczenie
#### LEKCJA 5 - Polimorfizm
#### LEKCJA 6 - Hermetyzacja
#### LEKCJA 7 - Klasy abstrakcyjne
#### LEKCJA 8 - Interfejsy
#### LEKCJA 9 - Typy generyczne
#### LEKCJA 10 - Refaktoryzacja
#### LEKCJA 11 - Błędy początkujących
#### LEKCJA 12 - Praca domowa
### Tydzien4
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Projekt z testami
#### LEKCJA 3 - Twój pierwszy test
#### LEKCJA 4 - Testy jednostkowe
#### LEKCJA 5 - Moq
#### LEKCJA 6 - FluentAssertions
#### LEKCJA 7 - Pokrycie kodu testami
#### LEKCJA 8 - TDD
#### LEKCJA 9 - Testy integracyjne
#### LEKCJA 10 - Błędy początkujących
#### LEKCJA 11 - Praca domowa
### Tydzien5
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Kolekcje w .NET
#### LEKCJA 3 - IQueryable i IEnumerable
#### LEKCJA 4 - LINQ podstawy
#### LEKCJA 5 - Manipulacje plikami


Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.
```

Now, if you want, you can completely close the program by pressing the **Esc** button, or return to the [main menu](#main-menu-view) by pressing **Enter** (or anything other then Esc).

### Hyperlinks table of contents view
You will be taken to this view if you choose option '2' in [table of content menu view](#table-of-contents-menu-view). This view looks the same as [the previous one](#plain-text-table-of-contents-view), except that it displays a different type of table of contents. This type is build according to the same rules as the one above. The only difference is that headers containing the titles (or names) of the selected files are not plain text, but hyperlinks that take the reader to appropriate section of the merged file.

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------


## Spis treści
### [Kurs "Zostań programistą ASP.NET" - notatki](#kurs-zostań-programistą-aspnet---notatki-1)
### Tydzien1
#### [BONUS 1 - Tworzenie Repozytorium GitHub](#bonus-1--tworzenie-repozytorium-github-1)
#### [BONUS 2 - Podstawy pracy z GITem](#bonus-2--podstawy-pracy-z-gitem-1)
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-1)
#### [LEKCJA 2 - Plan gry](#lekcja-2--plan-gry-1)
#### [LEKCJA 3 - Jak studiować ten kurs](#lekcja-3--jak-studiować-ten-kurs-1)
#### [LEKCJA 4 - Co musisz umieć zanim przejdziesz dalej](#lekcja-4--co-musisz-umieć-zanim-przejdziesz-dalej-1)
#### [LEKCJA 5 - Czym jest .NET](#lekcja-5--czym-jest-net-1)
#### [LEKCJA 6 - Wymagania (potrzebne oprogramowanie)](#lekcja-6--wymagania-potrzebne-oprogramowanie-1)
#### [LEKCJA 7 - Twój pierwszy program](#lekcja-7--twój-pierwszy-program-1)
#### [LEKCJA 8 - Jak pracować z Visual Studio](#lekcja-8--jak-pracować-z-visual-studio-1)
#### [LEKCJA 9 - Konwencje pisania (Dobre praktyki programowania)](#lekcja-9--konwencje-pisania-dobre-praktyki-programowania-1)
#### [LEKCJA 10 - Kompilator](#lekcja-10--kompilator-1)
#### [LEKCJA 11 - Debugowanie](#lekcja-11--debugowanie-1)
#### [LEKCJA 12 - Błędy początkujących](#lekcja-12--błędy-początkujących-1)
#### [LEKCJA 13 - Praca domowa](#lekcja-13--praca-domowa-1)
### Tydzien2
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-2)
#### [LEKCJA 2 - Zmienne i stałe](#lekcja-2--zmienne-i-stałe-1)
#### [LEKCJA 3 - Typy wartościowe](#lekcja-3--typy-wartościowe-1)
#### [LEKCJA 4 - Typy referencyjne](#lekcja-4--typy-referencyjne-1)
#### [LEKCJA 5 - Warunki](#lekcja-5--warunki-1)
#### [LEKCJA 6 - Operatory](#lekcja-6--operatory-1)
#### [LEKCJA 7 - Operatory Logiczne](#lekcja-7--operatory-logiczne-1)
#### [LEKCJA 8 - Pętle](#lekcja-8--pętle-1)
#### [LEKCJA 9 - Instrukcje skoku](#lekcja-9--instrukcje-skoku-1)
#### [LEKCJA 10 - Tablice](#lekcja-10--tablice-1)
#### [LEKCJA 11 - Listy](#lekcja-11--listy-1)
#### [LEKCJA 12 - Enum](#lekcja-12--enum-1)
#### [LEKCJA 13 - Klasy i obiekty](#lekcja-13--klasy-i-obiekty-1)
#### [LEKCJA 14 - Metody](#lekcja-14--metody-1)
#### [LEKCJA 15 - Parametry metod](#lekcja-15--parametry-metod-1)
#### [LEKCJA 16 - Pola i właściwości](#lekcja-16--pola-i-właściwości-1)
#### [LEKCJA 17 - Zakresy widoczności](#lekcja-17--zakresy-widoczności-1)
#### [LEKCJA 18 - Piszemy aplikację](#lekcja-18--piszemy-aplikację-1)
#### [LEKCJA 19 - Błędy początkujących](#lekcja-19--błędy-początkujących-1)
#### [LEKCJA 20 - Praca domowa](#lekcja-20--praca-domowa-1)
### Tydzien3
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-3)
#### [LEKCJA 2 - Konstruktory](#lekcja-2--konstruktory-1)
#### [LEKCJA 3 - Przeciążenia](#lekcja-3--przeciążenia-1)
#### [LEKCJA 4 - Dziedziczenie](#lekcja-4--dziedziczenie-1)
#### [LEKCJA 5 - Polimorfizm](#lekcja-5--polimorfizm-1)
#### [LEKCJA 6 - Hermetyzacja](#lekcja-6--hermetyzacja-1)
#### [LEKCJA 7 - Klasy abstrakcyjne](#lekcja-7--klasy-abstrakcyjne-1)
#### [LEKCJA 8 - Interfejsy](#lekcja-8--interfejsy-1)
#### [LEKCJA 9 - Typy generyczne](#lekcja-9--typy-generyczne-1)
#### [LEKCJA 10 - Refaktoryzacja](#lekcja-10--refaktoryzacja-1)
#### [LEKCJA 11 - Błędy początkujących](#lekcja-11--błędy-początkujących-1)
#### [LEKCJA 12 - Praca domowa](#lekcja-12--praca-domowa-1)
### Tydzien4
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-4)
#### [LEKCJA 2 - Projekt z testami](#lekcja-2--projekt-z-testami-1)
#### [LEKCJA 3 - Twój pierwszy test](#lekcja-3--twój-pierwszy-test-1)
#### [LEKCJA 4 - Testy jednostkowe](#lekcja-4--testy-jednostkowe-1)
#### [LEKCJA 5 - Moq](#lekcja-5--moq-1)
#### [LEKCJA 6 - FluentAssertions](#lekcja-6--fluentassertions-1)
#### [LEKCJA 7 - Pokrycie kodu testami](#lekcja-7--pokrycie-kodu-testami-1)
#### [LEKCJA 8 - TDD](#lekcja-8--tdd-1)
#### [LEKCJA 9 - Testy integracyjne](#lekcja-9--testy-integracyjne-1)
#### [LEKCJA 10 - Błędy początkujących](#lekcja-10--błędy-początkujących-1)
#### [LEKCJA 11 - Praca domowa](#lekcja-11--praca-domowa-1)
### Tydzien5
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-5)
#### [LEKCJA 2 - Kolekcje w .NET](#lekcja-2--kolekcje-w-net-1)
#### [LEKCJA 3 - IQueryable i IEnumerable](#lekcja-3--iqueryable-i-ienumerable-1)
#### [LEKCJA 4 - LINQ podstawy](#lekcja-4--linq-podstawy-1)
#### [LEKCJA 5 - Manipulacje plikami](#lekcja-5--manipulacje-plikami-1)


Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.
```
As in [previous view](#plain-text-table-of-contents-view) you can now completely close the program by pressing **Esc** button or return to [main menu](#main-menu-view) by pressing **Enter** button (or anything else other than Esc).

### No table of contents view
You will be taken to this view if you choose option '3' in [table of content menu view](#table-of-contents-menu-view). This view looks the same as the previous two, except, instead of a table of contents, it displays information about the lack of it (`Brak`).

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------


Brak

Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.
```
As in two previous views, you can now completely close the program by pressing **Esc** button or return to [main menu](#main-menu-view) by pressing **Enter** button (or anything else other than Esc).


## Merged file menu view
You will be taken to this view if you choose '4' in the [main menu view](#main-menu-view). Here you will be able to change some settings of the merged file before it is created. As in most views, at the top, you can find the main directory path. Then there is the view title and the currently set merged file settings. You can change three settings for the merged file:
1. the filename of the merged file (the default value is in the `MERGE_FILE_NAME` constant of the `Program` class and is set to `"README.md"`),
2. path of the directory where the merged file will be created (by default it is set to main directory path),
3. the title of the merged file (the default value is in the `MERGED_FILE_TITLE` constant of the `Program` class and is set to `"Kurs \"Zostań programistą ASP.NET\" - notatki"`).

The current values for these settings are after the colons.

Below is a menu to change one of these settings. So if you want to change:
1. the filename of the merged file, type **_1_**, and you will be taken to the [view](#merged-file-rename-view), which will allow you to do it
2. the path of the directory, where the merged file will be created, type **_2_**, and you will be taken to the [view](#merged-file-directory-change-view), which will allow you to do it
3. the title of the merged file, type **_3_**, and you will be taken to the [view](#merged-file-title-change-view), which is responsible for that.

After changing any of these settings, you will return to this view, so don't worry, you can change all the settings that you want.

When all the merged file settings have desired values, press **Enter** to create the merged file. You will be taken to the [merged file creation view](#merged-file-creation-view) and the file will be created in the set directory.

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Zmień ustawienia
1. Zmień nazwę tworzonego pliku
2. Zmień ścieżkę katalogu
3. Zmień nagłówek

Podaj numer ustawienia (1 - 3), które chcesz zmienić lub wciśnij Enter aby połączyć pliki z wybranymi ustawieniami:
```

If you select anything other than one of numbers 1 - 3 or Enter, an error message will be displayed and you will be asked to choose once again, what you want to do.

### Merged file rename view
If you chose '1' in the [previous view](#merged-file-menu-view), you will be taken to this view. As with most views, you will find the path to the main directory at the top. This is followed by the title of the [merged file menu view](#merged-file-menu-view) and the currently selected values of the merged file settings. Here you will be asked to enter the name, you want to give to your merged file. Specify only the name, not the full path. You can include the file extension (.md), but you don't have to. If you don't, it will be added automatically when creating the file.

Example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Podaj nazwę tworzonego pliku:
```

After entering the name and pressing Enter, the new name of the merged file will be set, and you will return to the [previous view](#merged-file-menu-view).

### Merged file directory change view
If you chose '2' in [merged file menu view](#merged-file-menu-view), you will be taken to this view. This view is built analogously to [the previous one](#merged-file-rename-view). Here you will be asked to enter the path to the directory where you want your merged file to be created. Similar to the [main directory change view](#main-directory-change-view), the path you enter can be absolute or relative to current directory (probably the directory where the program resides - probably subfolder MdFilesMerger\bin\Debug\net6.0 of the project or another directory from which the program was executed). If this folder does not exist, it will be created. If the program cannot create a directory with the given path (and such a directory does not exist yet), an error message will be displayed and you will be asked to re-enter the path.

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Podaj ścieżkę do katalogu, w którym chcesz zapisać plik:
```

After entering a valid directory path and pressing Enter, you will return to the [merged file menu view](#merged-file-menu-view), and the new merged file directory path will be set.

### Merged file title change view
If you chose '3' in the [merged file menu view](#merged-file-menu-view), you will be taken to this view. It is built the same as the previous two. Here you will be asked to enter a title of the merged file.

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Podaj nagłówek (tytuł) tworzonego pliku:
```

After entering desired title of the merged file and pressing Enter, you will be taken back to the [merged file menu view](#merged-file-menu-view), and the new title will be set. If you don't want your merged file to have any title, just press Enter and the title will be set to empty. If the title is empty, the title header won't be included in the merged file.

### Merged file creation view
If you have not selected any option in the [merged file menu view](#merged-file-menu-view), and pressed Enter, you will be taken to this view. As in many other views, you will see the absolute path of the current main directory, at the top of the window. Then you have the title of the [merged file menu view](#merged-file-menu-view), selected merged file settings and an information about the progress of merging. Once the merged file is complete, you will be taken back to the [main menu](#main-menu-view).

An example window view:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Scalanie plików                                                   55%

```
</details>
<details>
<summary>Opis programu po polsku</summary>
Aplikacja do łączenia wszystkich plików .md w ustawionym katalogu (i jego podkatalogach) w jeden plik.

## Słownik
1. **Katalog główny** - katalog, w którym program będzie szukał plików .md do scalenia. Program przeszuka ten katalog i wszystkie jego podkatalogi.
2. **Wybrane pliki** - wszystkie pliki .md znalezione w katalogu głównym (i jego podkatalogach).
3. **Tytuł pliku** - pierwsza linia pliku będąca nagłówkiem (`# tytuł pliku`) lub następna jeśli pierwsza linia jest pusta (pierwsze linie są puste).
4. **Scalony plik** - nowy plik zawierający tytuł, spis treści (jeśli go wybierzesz) oraz zawartość wszystkich wybranych plików. Jeśli wybrane pliki zawierają plik o takiej samej nazwie (pełna ścieżka) jak scalony plik, jego zawartość nie zostanie uwzględniona w tym pliku (stary plik zostanie usunięty, a nowy zostanie utworzony) i plik ten zostanie usunięty z listy wybranych plików podczas tworzenia scalonego pliku. Spowoduje to również ponowne utworzenie spisu treści (jeśli został wybrany), tak aby nie zawierał on już nagłówków związanych z plikiem usuniętym z listy.

## Widok startowy programu
Po uruchomieniu program, zostaniesz bezpośrednio przeniesiony(a) do [menu głównego](#widok-menu-głównego), chyba że, domyślny katalog główny (którego ścieżkę zapisano w stałej `MAIN_DIRECTORY_PATH` klasy `Program`) nie istnieje na twoim komputerze. Wówczas zostaniesz najpierw poproszony(a) o wybranie katalogu głównego. W tym wypadku, pierwszym widokiem, jaki zobaczysz, będzie [widok zmiany katalogu głównego](#widok-zmiany-katalogu-głównego).

## Widok menu głównego
To jest główny widok programu. Zawiera listę głównych funkcjonalności programu. Zostaniesz do niego przeniesiony(a) po zakończeniu wykonywania dowolnej funkcjonalności, chyba że wyjdziesz z całego programu.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

-------------------------------------------------------MENU GŁÓWNE------------------------------------------------------


1. Zmień katalog główny
2. Wyświetl listę plików do scalenia
3. Utwórz spis treści
4. Scal pliki

Podaj numer czynności z powyższego menu, którą chcesz wykonać lub wciśnij Esc, aby zakończyć działanie programu:
```

Na górze, po dwukropku, widać bezwzględną ścieżkę do aktualnie wybranego katalogu głównego. Następnie mamy tytuł widoku i listę głównych funkcjonalności programu. A więc:
* Jeśli chcesz zmienić katalog główny napisz **_1_**, a zostaniesz przeniesiony(a) do poprzedniego widoku ([widok zmiany katalogu głównego](#widok-zmiany-katalogu-głównego)), aby wprowadzić nową ścieżkę.
* Jeśli chcesz zobaczyć jakie pliki zostaną użyte w scalaniu napisz **_2_**, a zostaniesz przeniesiony(a) do [widoku listy wybranych plików](#widok-listy-wybranych-plików), aby zobaczyć listę wybranych plików.
* Jeśli chcesz umieścić spis treści na początku scalonego pliku (po tytule scalonego pliku) napisz **_3_**, a zostaniesz przeniesiony(a) do [widoku menu spisu treści](#widok-menu-spisu-treści), aby wybrać jeden z typów spisu treści.
* Jeśli chcesz utworzyć scalony plik, wybierz **_4_**, a zostaniesz przeniesiony(a) do [menu scalonego pliku](#widok-menu-scalonego-pliku), aby ewentualnie zmienić ustawienia scalonego pliku i utworzyć go.
* Jeśli chcesz zamknąć program, naciśnij przycisk **Esc**.
* Jeśli wpiszesz cokolwiek innego, wyświetli się komunikat o błędzie i zostaniesz poproszony(a) o ponowne wybranie tego, co chcesz zrobić.

## Widok zmiany katalogu głównego
Ten widok zostanie wyświetlony w jednej z następujących sytuacji:
1. Po uruchomieniu programu, jeśli katalog ścieżki zapisanej w stałej `MAIN_DIRECTORY_PATH` klasy `Program` nie istnieje na twoim komputerze,
2. Jeśli wybrałeś(aś) „1” w [widok menu głównego](#widok-menu-głównego).

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

-----------------------------------USTAW KATALOG, W KTÓRYM CHCESZ WYSZUKIWAĆ PLIKI .MD----------------------------------


Wprowadź ścieżkę dostępu do katalogu:
```
Na górze, po dwukropku, zobaczysz bezwzględną ścieżkę do aktualnie wybranego katalogu głównego (jeżeli znalazłeś(łaś) się w tym widoku z powodu pierwszej sytuacji, to ta linia będzie pominięta). Teraz wprowadź ścieżkę wybranego katalogu, który chcesz ustawić jako katalog główny. Ścieżka może być bezwzględna lub względna w stosunku do bieżącego katalogu (prawdopodobnie jest to katalog, w którym znajduje się program - przypuszczalnie podfolder MdFilesMerger\bin\Debug\net6.0 projektu lub inny katalog, z którego program został uruchomiony). Jeśli wprowadzona ścieżka istnieje na Twoim komputerze, zostaniesz przeniesiony(a) do poprzedniego widoku ([widok menu głównego](#widok-menu-głównego)). Jeżeli katalog o podanej ścieżce nie istnieje, pojawi się komunikat o błędzie i zostaniesz poproszony(a) o ponowne wprowadzenie ścieżki.

## Widok listy wybranych plików
Ten widok zostanie wyświetlony po wybraniu opcji „2” w [widoku menu głównego](#widok-menu-głównego). Podobnie jak w poprzednich widokach, na górze okna widoczna jest ścieżka bezwzględna aktualnego katalogu głównego. Poniżej znajduje się tytuł widoku i lista wybranych plików. Pliki są wyświetlane jako ścieżki względne w stosunku do katalogu głównego. Pliki są posortowane alfabetycznie według nazw, ale jeśli katalog zawiera kilka podkatalogów, które mają takie same nazwy poza numerem na końcu, to są one posortowane rosnąco według tych numerów. Wybrane pliki zostaną dodane do scalonego pliku w tej właśnie kolejności. Na dole znajduje się informacja, żeby nacisnąć Enter, aby wrócić do [menu głównego](#widok-menu-głównego) lub Esc, aby całkowicie zakończyć działanie programu. Jeśli więc chcesz wrócić do [menu głównego](#widok-menu-głównego) naciśnij **Enter** (lub dowolny inny klawisz oprócz Esc). Jeśli naciśniesz **Esc**, cały program zostanie zamknięty.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

------------------------------------------------LISTA PLIKÓW DO SCALENIA------------------------------------------------


\README.md
\Tydzien1\Bonus1\BONUS1-TworzenieRepozytoriumGitHub.md
\Tydzien1\Bonus2\BONUS2-PodstawyPracyZGITem.md
\Tydzien1\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien1\Lekcja2\LEKCJA2-PlanGry.md
\Tydzien1\Lekcja3\LEKCJA3-JakStudiowacTenKurs.md
\Tydzien1\Lekcja4\LEKCJA4-CoMusiszUmiecZanimPrzejdziaszDalej.md
\Tydzien1\Lekcja5\LEKCJA5-CzymJestDotNET.md
\Tydzien1\Lekcja6\LEKCJA6-Wymagania-potrzebneOprogramowanie.md
\Tydzien1\Lekcja7\LEKCJA7-TwojPierwszyProgram.md
\Tydzien1\Lekcja8\LEKCJA8-JakPracowacZVisualStudio.md
\Tydzien1\Lekcja9\LEKCJA9-KonwencjePisania.md
\Tydzien1\Lekcja10\LEKCJA10-Kompilator.md
\Tydzien1\Lekcja11\LEKCJA11-Debugowanie.md
\Tydzien1\Lekcja12\LEKCJA12-BledyPoczatkujacych.md
\Tydzien1\Lekcja13\LEKCJA13-PracaDomowa.md
\Tydzien2\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien2\Lekcja2\LEKCJA2-ZmienneIStale.md
\Tydzien2\Lekcja3\LEKCJA3-TypyWartosciowe.md
\Tydzien2\Lekcja4\LEKCJA4-TypyReferencyjne.md
\Tydzien2\Lekcja5\LEKCJA5-Warunki.md
\Tydzien2\Lekcja6\LEKCJA6-Operatory.md
\Tydzien2\Lekcja7\LEKCJA7-OperatoryLogiczne.md
\Tydzien2\Lekcja8\LEKCJA8-Petle.md
\Tydzien2\Lekcja9\LEKCJA9-InstrukcjeSkoku.md
\Tydzien2\Lekcja10\LEKCJA10-Tablice.md
\Tydzien2\Lekcja11\LEKCJA11-Listy.md
\Tydzien2\Lekcja12\LEKCJA12-Enum.md
\Tydzien2\Lekcja13\LEKCJA13-KlasyIObiekty.md
\Tydzien2\Lekcja14\LEKCJA14-Metody.md
\Tydzien2\Lekcja15\LEKCJA15-ParametryMetod.md
\Tydzien2\Lekcja16\LEKCJA16-PolaIWłaściwości.md
\Tydzien2\Lekcja17\LEKCJA17-ZakresyWidoczności.md
\Tydzien2\Lekcja18\LEKCJA18-PiszemyAplikację.md
\Tydzien2\Lekcja19\LEKCJA19-BledyPoczatkujacych.md
\Tydzien2\Lekcja20\LEKCJA20-PracaDomowa.md
\Tydzien3\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien3\Lekcja2\LEKCJA2-Konstruktory.md
\Tydzien3\Lekcja3\LEKCJA3-Przeciazenia.md
\Tydzien3\Lekcja4\LEKCJA4-Dziedziczenie.md
\Tydzien3\Lekcja5\LEKCJA5-Polimorfizm.md
\Tydzien3\Lekcja6\LEKCJA6-Hermetyzacja.md
\Tydzien3\Lekcja7\LEKCJA7-KlasyAbstrakcyjne.md
\Tydzien3\Lekcja8\LEKCJA8-Interfejsy.md
\Tydzien3\Lekcja9\LEKCJA9-TypyGeneryczne.md
\Tydzien3\Lekcja10\LEKCJA10-Refaktoryzacja.md
\Tydzien3\Lekcja11\LEKCJA11-BledyPoczatkujacych.md
\Tydzien3\Lekcja12\LEKCJA12-PracaDomowa.md
\Tydzien4\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien4\Lekcja2\LEKCJA2-ProjektZTestami.md
\Tydzien4\Lekcja3\LEKCJA3-TwojPierwszyTest.md
\Tydzien4\Lekcja4\LEKCJA4-TestyJednostkowe.md
\Tydzien4\Lekcja5\LEKCJA5-Moq.md
\Tydzien4\Lekcja6\LEKCJA6-FluentAssertions.md
\Tydzien4\Lekcja7\LEKCJA7-PokrycieKoduTestami.md
\Tydzien4\Lekcja8\LEKCJA8-TDD.md
\Tydzien4\Lekcja9\LEKCJA9-TestyIntegracyjne.md
\Tydzien4\Lekcja10\LEKCJA10-BledyPoczatkujacych.md
\Tydzien4\Lekcja11\LEKCJA11-PracaDomowa.md
\Tydzien5\Lekcja1\LEKCJA1-Powitanie.md
\Tydzien5\Lekcja2\LEKCJA2-KolekcjeWdotNET.md
\Tydzien5\Lekcja3\LEKCJA3-IQueryableIIEnumerable.md
\Tydzien5\Lekcja4\LEKCJA4-LINQPodstawy.md
\Tydzien5\Lekcja5\LEKCJA5-ManipulacjePlikami.md

Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.
```


## Widok menu spisu treści
Zostaniesz przeniesiony(a) do tego widoku, jeśli wybierzesz „3” w [widoku menu głównego](#widok-menu-głównego). Podobnie jak w poprzednich widokach, u góry widoczna jest ścieżka do bieżącego katalogu głównego. Następnie po tytule możesz zobaczyć menu. Program poprosi Cię o wybranie typu spisu treści, który chcesz dodać do scalonego pliku. Możesz wybrać jedną z trzech opcji:
1. Napisz **_1_**, jeśli chcesz, aby Twój spis treści był zwykłym tekstem, a dokładniej zestawem nagłówków odpowiedniego poziomu. Wybranie tej opcji przeniesie Cię do [widoku](#widok-spisu-treści-typu-zwykły-tekst), który stworzy taki spis treści i wyświetli jak będzie on wyglądać.
2. Napisz **_2_**, jeśli chcesz, aby Twój spis treści zawierał w nagłówkach tytułów plików, zamiast zwykłego tekstu, hiperłącza, które pozwolą później, po ich kliknięciu, przejść bezpośrednio do fragmentu scalonego pliku zawierającego zawartość odpowiedniego pliku. Wybranie tej opcji przeniesie Cię do [widoku](#widok-spisu-treści-typu-hiperlinki), który stworzy i wyświetli tekst spisu treści tego typu.
3. Napisz **_3_**, jeśli zmieniłeś(łaś) zdanie i nie chcesz umieszczać w swoim scalonym pliku żadnego spisu treści. Wybranie tej opcji przeniesie Cię do [widoku braku spisu treści](#widok-braku-spisu-treści), gdzie zastanie Ci wyświetlona informacja, o braku spisu treści.

Po wybraniu opcji „1” lub „2” spis treści wybranego typu (taki, który ostatnio wybrałeś(aś), jeśli odwiedziłeś(aś) ten widok kilka razy) zostanie dodany do scalonego pliku, jeśli go później utworzysz.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------


1. Spis treści będący zwykłym tekstem
2. Spis treści złożony z hiperlinków do odpowiednich paragrafów
3. Bez spisu treści

Podaj numer typu wybranego z powyższego menu:
```

Jeśli wpiszesz coś innego niż „1”, „2” lub „3”, pojawi się komunikat o błędzie i zostaniesz poproszony(a) o ponowny wybór opcji.

### Widok spisu treści typu zwykły tekst
Zostaniesz przeniesiony(a) do tego widoku, jeśli wybierzesz opcję „1” w [poprzednim widoku](#widok-menu-spisu-treści). Podobnie jak w poprzednich widokach, na górze okna znajduje się ścieżka bezwzględna do głównego katalogu. Poniżej jest tekst spisu treści. Będzie on zawierał nagłówek drugiego poziomu z tytułem spisu treści (`## Spis treści`), po którym następuje lista nagłówków odpowiedniego poziomu. Każdy nagłówek to albo nazwa podkatalogu (jeśli w tym podkatalogu jest więcej niż jeden wybrany plik) albo tytuł wybranego pliku (jeśli plik nie ma tytułu, to jest on zastępowany nazwą pliku). Więc najpierw mamy nagłówki z tytułami plików, które są umieszczone bezpośrednio w katalogu głównym, następnie nazwę podfolderu (jeśli w tym podfolderze jest więcej niż jeden wybrany plik), tytuły plików, które znajdują się w tym podfolderze i tak dalej.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------


## Spis treści
### Kurs "Zostań programistą ASP.NET" - notatki
### Tydzien1
#### BONUS 1 - Tworzenie Repozytorium GitHub
#### BONUS 2 - Podstawy pracy z GITem
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Plan gry
#### LEKCJA 3 - Jak studiować ten kurs
#### LEKCJA 4 - Co musisz umieć zanim przejdziesz dalej
#### LEKCJA 5 - Czym jest .NET
#### LEKCJA 6 - Wymagania (potrzebne oprogramowanie)
#### LEKCJA 7 - Twój pierwszy program
#### LEKCJA 8 - Jak pracować z Visual Studio
#### LEKCJA 9 - Konwencje pisania (Dobre praktyki programowania)
#### LEKCJA 10 - Kompilator
#### LEKCJA 11 - Debugowanie
#### LEKCJA 12 - Błędy początkujących
#### LEKCJA 13 - Praca domowa
### Tydzien2
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Zmienne i stałe
#### LEKCJA 3 - Typy wartościowe
#### LEKCJA 4 - Typy referencyjne
#### LEKCJA 5 - Warunki
#### LEKCJA 6 - Operatory
#### LEKCJA 7 - Operatory Logiczne
#### LEKCJA 8 - Pętle
#### LEKCJA 9 - Instrukcje skoku
#### LEKCJA 10 - Tablice
#### LEKCJA 11 - Listy
#### LEKCJA 12 - Enum
#### LEKCJA 13 - Klasy i obiekty
#### LEKCJA 14 - Metody
#### LEKCJA 15 - Parametry metod
#### LEKCJA 16 - Pola i właściwości
#### LEKCJA 17 - Zakresy widoczności
#### LEKCJA 18 - Piszemy aplikację
#### LEKCJA 19 - Błędy początkujących
#### LEKCJA 20 - Praca domowa
### Tydzien3
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Konstruktory
#### LEKCJA 3 - Przeciążenia
#### LEKCJA 4 - Dziedziczenie
#### LEKCJA 5 - Polimorfizm
#### LEKCJA 6 - Hermetyzacja
#### LEKCJA 7 - Klasy abstrakcyjne
#### LEKCJA 8 - Interfejsy
#### LEKCJA 9 - Typy generyczne
#### LEKCJA 10 - Refaktoryzacja
#### LEKCJA 11 - Błędy początkujących
#### LEKCJA 12 - Praca domowa
### Tydzien4
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Projekt z testami
#### LEKCJA 3 - Twój pierwszy test
#### LEKCJA 4 - Testy jednostkowe
#### LEKCJA 5 - Moq
#### LEKCJA 6 - FluentAssertions
#### LEKCJA 7 - Pokrycie kodu testami
#### LEKCJA 8 - TDD
#### LEKCJA 9 - Testy integracyjne
#### LEKCJA 10 - Błędy początkujących
#### LEKCJA 11 - Praca domowa
### Tydzien5
#### LEKCJA 1 - Powitanie
#### LEKCJA 2 - Kolekcje w .NET
#### LEKCJA 3 - IQueryable i IEnumerable
#### LEKCJA 4 - LINQ podstawy
#### LEKCJA 5 - Manipulacje plikami


Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.
```

Teraz, jeśli chcesz, możesz całkowicie zamknąć program, naciskając przycisk **Esc** lub wrócić do [menu głównego](#widok-menu-głównego) naciskając przycisk **Enter** (lub cokolwiek innego oprócz Esc).

### Widok spisu treści typu hiperlinki
Zostaniesz przeniesiony(a) do tego widoku, jeśli wybierzesz opcję „2” w [widoku menu spisu treści](#widok-menu-spisu-treści). Ten widok wygląda analogicznie do [poprzedniego](#widok-spisu-treści-typu-zwykły-tekst), z tą różnicą, że wyświetla inny typ spisu treści. Ten typ jest budowany według tych samych zasad co powyższy. Jedyną różnicą jest to, że nagłówki zawierające tytuły (lub nazwy) wybranych plików nie są zwykłym tekstem, a hiperłączami przenoszącymi czytelnika do odpowiedniej części scalonego pliku.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------


## Spis treści
### [Kurs "Zostań programistą ASP.NET" - notatki](#kurs-zostań-programistą-aspnet---notatki-1)
### Tydzien1
#### [BONUS 1 - Tworzenie Repozytorium GitHub](#bonus-1--tworzenie-repozytorium-github-1)
#### [BONUS 2 - Podstawy pracy z GITem](#bonus-2--podstawy-pracy-z-gitem-1)
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-1)
#### [LEKCJA 2 - Plan gry](#lekcja-2--plan-gry-1)
#### [LEKCJA 3 - Jak studiować ten kurs](#lekcja-3--jak-studiować-ten-kurs-1)
#### [LEKCJA 4 - Co musisz umieć zanim przejdziesz dalej](#lekcja-4--co-musisz-umieć-zanim-przejdziesz-dalej-1)
#### [LEKCJA 5 - Czym jest .NET](#lekcja-5--czym-jest-net-1)
#### [LEKCJA 6 - Wymagania (potrzebne oprogramowanie)](#lekcja-6--wymagania-potrzebne-oprogramowanie-1)
#### [LEKCJA 7 - Twój pierwszy program](#lekcja-7--twój-pierwszy-program-1)
#### [LEKCJA 8 - Jak pracować z Visual Studio](#lekcja-8--jak-pracować-z-visual-studio-1)
#### [LEKCJA 9 - Konwencje pisania (Dobre praktyki programowania)](#lekcja-9--konwencje-pisania-dobre-praktyki-programowania-1)
#### [LEKCJA 10 - Kompilator](#lekcja-10--kompilator-1)
#### [LEKCJA 11 - Debugowanie](#lekcja-11--debugowanie-1)
#### [LEKCJA 12 - Błędy początkujących](#lekcja-12--błędy-początkujących-1)
#### [LEKCJA 13 - Praca domowa](#lekcja-13--praca-domowa-1)
### Tydzien2
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-2)
#### [LEKCJA 2 - Zmienne i stałe](#lekcja-2--zmienne-i-stałe-1)
#### [LEKCJA 3 - Typy wartościowe](#lekcja-3--typy-wartościowe-1)
#### [LEKCJA 4 - Typy referencyjne](#lekcja-4--typy-referencyjne-1)
#### [LEKCJA 5 - Warunki](#lekcja-5--warunki-1)
#### [LEKCJA 6 - Operatory](#lekcja-6--operatory-1)
#### [LEKCJA 7 - Operatory Logiczne](#lekcja-7--operatory-logiczne-1)
#### [LEKCJA 8 - Pętle](#lekcja-8--pętle-1)
#### [LEKCJA 9 - Instrukcje skoku](#lekcja-9--instrukcje-skoku-1)
#### [LEKCJA 10 - Tablice](#lekcja-10--tablice-1)
#### [LEKCJA 11 - Listy](#lekcja-11--listy-1)
#### [LEKCJA 12 - Enum](#lekcja-12--enum-1)
#### [LEKCJA 13 - Klasy i obiekty](#lekcja-13--klasy-i-obiekty-1)
#### [LEKCJA 14 - Metody](#lekcja-14--metody-1)
#### [LEKCJA 15 - Parametry metod](#lekcja-15--parametry-metod-1)
#### [LEKCJA 16 - Pola i właściwości](#lekcja-16--pola-i-właściwości-1)
#### [LEKCJA 17 - Zakresy widoczności](#lekcja-17--zakresy-widoczności-1)
#### [LEKCJA 18 - Piszemy aplikację](#lekcja-18--piszemy-aplikację-1)
#### [LEKCJA 19 - Błędy początkujących](#lekcja-19--błędy-początkujących-1)
#### [LEKCJA 20 - Praca domowa](#lekcja-20--praca-domowa-1)
### Tydzien3
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-3)
#### [LEKCJA 2 - Konstruktory](#lekcja-2--konstruktory-1)
#### [LEKCJA 3 - Przeciążenia](#lekcja-3--przeciążenia-1)
#### [LEKCJA 4 - Dziedziczenie](#lekcja-4--dziedziczenie-1)
#### [LEKCJA 5 - Polimorfizm](#lekcja-5--polimorfizm-1)
#### [LEKCJA 6 - Hermetyzacja](#lekcja-6--hermetyzacja-1)
#### [LEKCJA 7 - Klasy abstrakcyjne](#lekcja-7--klasy-abstrakcyjne-1)
#### [LEKCJA 8 - Interfejsy](#lekcja-8--interfejsy-1)
#### [LEKCJA 9 - Typy generyczne](#lekcja-9--typy-generyczne-1)
#### [LEKCJA 10 - Refaktoryzacja](#lekcja-10--refaktoryzacja-1)
#### [LEKCJA 11 - Błędy początkujących](#lekcja-11--błędy-początkujących-1)
#### [LEKCJA 12 - Praca domowa](#lekcja-12--praca-domowa-1)
### Tydzien4
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-4)
#### [LEKCJA 2 - Projekt z testami](#lekcja-2--projekt-z-testami-1)
#### [LEKCJA 3 - Twój pierwszy test](#lekcja-3--twój-pierwszy-test-1)
#### [LEKCJA 4 - Testy jednostkowe](#lekcja-4--testy-jednostkowe-1)
#### [LEKCJA 5 - Moq](#lekcja-5--moq-1)
#### [LEKCJA 6 - FluentAssertions](#lekcja-6--fluentassertions-1)
#### [LEKCJA 7 - Pokrycie kodu testami](#lekcja-7--pokrycie-kodu-testami-1)
#### [LEKCJA 8 - TDD](#lekcja-8--tdd-1)
#### [LEKCJA 9 - Testy integracyjne](#lekcja-9--testy-integracyjne-1)
#### [LEKCJA 10 - Błędy początkujących](#lekcja-10--błędy-początkujących-1)
#### [LEKCJA 11 - Praca domowa](#lekcja-11--praca-domowa-1)
### Tydzien5
#### [LEKCJA 1 - Powitanie](#lekcja-1--powitanie-5)
#### [LEKCJA 2 - Kolekcje w .NET](#lekcja-2--kolekcje-w-net-1)
#### [LEKCJA 3 - IQueryable i IEnumerable](#lekcja-3--iqueryable-i-ienumerable-1)
#### [LEKCJA 4 - LINQ podstawy](#lekcja-4--linq-podstawy-1)
#### [LEKCJA 5 - Manipulacje plikami](#lekcja-5--manipulacje-plikami-1)


Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.
```

Tak jak w [poprzednim widoku](#widok-spisu-treści-typu-zwykły-tekst) możesz teraz całkowicie zamknąć program, naciskając przycisk **Esc** lub wrócić do [menu głównego](#widok-menu-głównego) naciskając przycisk **Enter** (lub cokolwiek innego oprócz Esc).

### Widok braku spisu treści
Zostaniesz przeniesiony(a) do tego widoku, jeśli wybierzesz opcję „3” w [widoku menu spisu treści](#widok-menu-spisu-treści). Ten widok wygląda analogicznie jak dwa poprzednie, z tą różnicą, że zamiast spisu treści, wyświetla informację o jego braku (`Brak`).

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------


Brak

Wciśnij Enter aby wrócić do menu głównego lub Esc by zakończyć program.
```

Tak jak w poprzednich dwóch widokach możesz teraz całkowicie zamknąć program, naciskając przycisk **Esc** lub wrócić do [menu głównego](#widok-menu-głównego) naciskając przycisk **Enter** (lub cokolwiek innego oprócz Esc).

## Widok menu scalonego pliku
Zostaniesz przeniesiony(a) do tego widoku, jeśli wybierzesz „4” w [widok menu głównego](#widok-menu-głównego). Tutaj będziesz mógł(mogła) zmienić niektóre ustawienia scalonego pliku przed jego utworzeniem. Tak jak w większości widoków u góry znajduje się ścieżka do katalogu głównego. Następnie jest tytuł widoku i aktualnie wybrane ustawienia scalonego pliku. Możesz zmienić trzy ustawienia dla scalonego pliku:
1. nazwę scalonego pliku (wartość domyślna to stała `MERGE_FILE_NAME` klasy `Program` i jest ona ustawiona na `"README.md"`),
2. ścieżkę katalogu, w którym zostanie utworzony scalony plik (domyślnie ustawiona jest ścieżka do katalogu głównego),
3. tytuł scalonego pliku (wartość domyślna jest w stałej `MERGED_FILE_TITLE` klasy `Program` i jest ustawiona na `"Kurs \"Zostań programistą ASP.NET\" - notatki"`).

Aktualne wartości dla tych ustawień znajdują się po dwukropkach.

Poniżej znajduje się menu umożliwiające zmianę jednego z tych ustawień. Więc jeśli chcesz zmienić:
1. nazwę scalonego pliku napisz **_1_**, a zostaniesz przeniesiony(a) do [widoku](#widok-zmiany-nazwy-scalonego-pliku), który pozwoli to zrobić
2. ścieżkę do katalogu, w którym zostanie utworzony scalony plik, napisz **_2_**, a zostaniesz przeniesiony(a) do [widoku](#widok-zmiany-katalogu-scalonego-pliku), który na to pozwoli
3. tytuł scalonego pliku, napisz **_3_**, a zostaniesz przeniesiony(a) do [widoku](#widok-zmiany-tytułu-scalonego-pliku), który za to odpowiada.

Po zmianie dowolnego z tych ustawień powrócisz do widoku tego menu, więc nie martw się, możesz zmienić wszystkie ustawienia, które chcesz.

Gdy wszystkie ustawienia scalonego pliku mają pożądane wartości, naciśnij **Enter**, aby utworzyć scalony plik. Zostaniesz przeniesiony(a) do [widoku tworzenia scalonego plików](#widok-tworzenia-scalonego-pliku) i plik zostanie wygenerowany i zapisany w ustawionym katalogu.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Zmień ustawienia
1. Zmień nazwę tworzonego pliku
2. Zmień ścieżkę katalogu
3. Zmień nagłówek

Podaj numer ustawienia (1 - 3), które chcesz zmienić lub wciśnij Enter aby połączyć pliki z wybranymi ustawieniami:
```

Jeśli wybierzesz coś innego niż jedną z cyfr 1 - 3 lub Enter, wyświetli się komunikat o błędzie i zostaniesz poproszony(a) o ponowne wybranie, co chcesz zrobić.

### Widok zmiany nazwy scalonego pliku
Jeśli wybrałeś(aś) „1” w [poprzednim widok](#widok-menu-scalonego-pliku), zostaniesz przeniesiony(a) do tego widoku. Jak w większości widoków, na górze znajdziesz ścieżkę do katalogu głównego. Następnie znajduje się tytuł [widoku menu scalonego pliku](#widok-menu-scalonego-pliku) oraz aktualnie wybrane wartości ustawień scalonego pliku. Tutaj zostaniesz poproszony(a) o podanie nazwy, którą chcesz nadać scalonemu plikowi. Podaj tylko nazwę, a nie pełną ścieżkę. Możesz dołączyć rozszerzenie pliku (.md), ale nie musisz. Jeśli tego nie zrobisz, zostanie ono dodane automatycznie, podczas tworzenia pliku.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Podaj nazwę tworzonego pliku:
```

Po wprowadzeniu nazwy i naciśnięciu Enter, zostanie ustawiona nowa nazwa scalonego pliku i nastąpi powrót do [poprzedniego widoku](#widok-menu-scalonego-pliku).

### Widok zmiany katalogu scalonego pliku
Jeśli wybierzesz „2” w [widoku menu scalonego pliku](#widok-menu-scalonego-pliku), zostaniesz przeniesiony(a) do tego widoku. Widok ten jest zbudowany analogicznie jak [poprzedni](#widok-zmiany-nazwy-scalonego-pliku). Tutaj zostaniesz poproszony(a) o podanie ścieżki do katalogu, w którym chcesz utworzyć scalony plik. Podobnie jak w [widoku zmiany katalogu głównego](#widok-zmiany-katalogu-głównego), wprowadzona ścieżka może być bezwzględna lub względna w stosunku do bieżącego katalogu (prawdopodobnie katalogu, w którym znajduje się program - prawdopodobnie podfolderu MdFilesMerger\bin\Debug\net6.0 projektu lub innego katalog, z którego wykonywany był program). Jeśli ten folder nie istnieje, zostanie on utworzony. Jeżeli program nie będzie mógł utworzyć katalogu o podanej ścieżce (a taki katalog jeszcze nie istnieje) wyświetli się komunikat o błędzie i zostaniesz poproszony(a) o ponowne wprowadzenie ścieżki.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Podaj ścieżkę do katalogu, w którym chcesz zapisać plik:
```

Po wprowadzeniu poprawnej ścieżki do katalogu i naciśnięciu Enter, zostaniesz przeniesiony(a) z powrotem do [widok menu scalonego pliku](#widok-menu-scalonego-pliku), a nowa ścieżka do katalogu scalonego pliku zostanie ustawiona.

### Widok zmiany tytułu scalonego pliku
Jeśli wybrałeś(łaś) „3” w [widoku menu scalonego pliku](#widok-menu-scalonego-pliku), zostaniesz przeniesiony(a) do tego widoku. Jest on zbudowany tak samo jak dwa poprzednie. Tutaj zostaniesz poproszony(a) o wprowadzenie tytułu scalonego pliku.

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Podaj nagłówek (tytuł) tworzonego pliku:
```

Po wprowadzeniu tytułu scalonego pliku i naciśnięciu klawisza Enter, zostaniesz przeniesiony(a) z powrotem do [widoku menu scalonego pliku](#widok-menu-scalonego-pliku), a nowy tytuł zostanie ustawiony. Jeśli nie chcesz, aby scalony plik miał jakikolwiek tytuł, po prostu naciśnij Enter, a tytuł będzie pusty. Jeśli tytuł jest pusty, nagłówek tytułu nie zostanie uwzględniony w scalonym pliku.

### Widok tworzenia scalonego pliku
Jeśli nie wybrałeś żadnej opcji w [widoku menu scalonego pliku](#widok-menu-scalonego-pliku), a wcisnąłeś(łaś) Enter, zostaniesz przeniesiony(a) do tego widoku. Podobnie jak w wielu innych widokach, w górnej części okna zobaczysz bezwzględną ścieżkę aktualnego katalogu głównego. Następnie masz tytuł [widoku menu scalonego plików](#widok-menu-scalonego-pliku), wybrane ustawienia scalonego pliku i informację o postępie scalania plików. Po zakończeniu tworzenia scalonego pliku zostaniesz przeniesiony(a) z powrotem do [menu głównego](#widok-menu-głównego).

Przykładowy widok okna:
```
Katalog główny: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------


Ustawienia
Nazwa tworzonego pliku: README.md
Położenie pliku: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Tytuł: Kurs "Zostań programistą ASP.NET" - notatki

Scalanie plików                                                   55%

```
</details>