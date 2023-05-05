# MdFilesMerger
Application to merge all .md files in a set directory (and its subdirectories) into one file. 

## Dictionary
1. Main directory - catalog in which program will search for .md files to merge. Program will search in this directory and all its subdirectories.
2. Selected files - all .md files that was found in main directory (and its subdirectories).
3. File title - first line of file that is a header (`# file title`) or next if first line(s) is\are empty.
4. Merged file - new file that contains title, table of contents (if you choose it) and content of all selected files. If selected files contain file with the same name (full path) as merged file, its content is not included in this file (old file is deleted and new one is created) and that file will be removed from selected files list during merged file creating. It will also cause recreating of table of contents (if it was selected), so it no longer contains headers associated with file removed from list.

## Program start view
After program initiation program asks you whether you want to change main directory. Default main directory path is stored in `MAIN_DIRECTORY_PATH` `Program` class constant. If directory of stored path exists on your computer program will asks you whether you want to change it or not.

Window example view:

```
----------------------USTAW KATALOG, W KTÓRYM CHCESZ WYSZUKIWAĆ PLIKI .MD---------------------

Domyślna ścieżka do katalogu: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Czy chcesz ją zmienić? (t/n)
```
Write **_t_** if you want to change main directory or **_n_** if you want to keep default one. If you choose **_t_** you will be transported to [the next view](#change-main-directory-view). If you choose **_n_** you will by transported to [the main menu view](#main-menu-view). If you write anything else error message will be displayed and you will be asked to answer again.

If directory of path stored in `MAIN_DIRECTORY_PATH` constant doesn't exists on your computer you will be asked directly to enter the path of main directory (instead of this view you will see [the next one](#change-main-directory-view)).

## Change main directory view
This view will be shown to you in one of the following situations:
1. After starting the program if directory of path stored in `MAIN_DIRECTORY_PATH` `Program` class constant doesn't exists on your computer,
2. If you chose 't' in [previous view](#program-start-view),
3. If you chose '1' in [main menu view](#main-menu-view).

Window example view:
```
Katalog główny, w którym wyszukiwane są pliki do scalenia: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET


------------------------------------USTAW KATALOG, W KTÓRYM CHCESZ WYSZUKIWAĆ PLIKI .MD-----------------------------------

Wprowadź ścieżkę dostępu do katalogu:
```

On top, after colon, you will see the absolute path of currently selected main directory. Now enter the path of your chosen directory that you want to set as main catalog. Path can be either absolute or relative to current directory (probably the directory where program is held - probably project's MdFilesMerger\bin\Debug\net6.0 subfolder or other directory from which program was executed). If entered path exists on your computer you will be transported to the next view ([main menu view](#main-menu-view)). If directory of entered path does not exist, error message will be shown and you will be asked to enter the path again.

## Main menu view
This is the main view of the program. It contains list of program's main functionalities to choose from. You will be transported to it after finishing any of the functionality, unless you finish the entire program.

Window example view:
```
Katalog główny, w którym wyszukiwane są pliki do scalenia: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET


--------------------------------------------------------MENU GŁÓWNE-------------------------------------------------------

1. Zmień katalog główny
2. Wyświetl listę plików do scalenia
3. Utwórz spis treści
4. Scal pliki

Podaj numer czynności z powyższego menu, którą chcesz wykonać lub wciśnij Esc, aby zakończyć działanie programu:
```

Once again on top, after colon, you can see the absolute path of currently selected main directory. Then we have view title and list of programs main functionalities. And so:
* If you want to change main directory write **_1_**, and you will be transported to the previous view ([change main directory view](#change-main-directory-view)), to enter new path.
* If you want to see what files will be used in merge write **_2_**, and you will be transported to [list of files view](#list-of-files-view), to see the list of selected files.
* If you want to put a table of contents at the beginning of merged file (after merged file title) write **_3_**, and you will be transported to [table of contents menu view](#table-of-contents-menu-view), to select one of table of contents types.
* If you want to create merged file choose **_4_**, and you will be transported to [the merged file menu view](#merged-file-menu-view) to set merged file final settings and create merged file.
* If you want to close the program press **Esc** button.
* If you enter anything else, error message will by displayed and you will be asked to choose again, what you want to do.

## List of files view
This view will be shown to you after choosing '2' in previous view ([main menu view](#main-menu-view)). Like in previous views, on top of the widow you will see curren main directory absolute path. Beneath it is a list of selected files. Files are displayed as paths relative to main directory. Files are sorted by names alphabetically, but if directory contains few subdirectories that have the same names except of number at the end, then they are sorted ascending by this numbers. Selected files will be added to merged file in this order. At the bottom is located information to press Enter to go back to [main menu](#main-menu-view) or Esc to finish the program entirely. So if you want to go back to [main menu](#main-menu-view) press **Enter** (or any other key except Esc). If you press **Esc** whole program will be closed.

Window example view:
```
Katalog główny, w którym wyszukiwane są pliki do scalenia: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

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
You will be transported to this view, if you choose '3' in [main menu view](#main-menu-view). As in the previous views, at the top you can see here absolute path to current main directory. Then after title you can see menu. Program asks you to select type of table of contents, that you want to add to your merged file. You can choose one of two options:
1. Write **_1_** if you want your table of contents to be ordinary text, or more specifically set of headers of appropriate level. Selecting this option will transport you to [view](#plain-text-table-of-contents-view), that will display what table of contents will look like.
2. Write **_2_** if you want your table of contents to have, in file titles headers, instead of plain text, hyperlinks that will later allow you to go directly to fragment of merged file that contains content of corresponding file, after clicking them. Choosing this option transports you to [view](#hyperlinks-table-of-contents-view), that will display text of this table of contents type.

After choosing one of these options, table of content of chosen type (the one that you chose at last, if you visited this view several times) will be added to the merged file if you create it later.

Example window view:
```
Katalog główny, w którym wyszukiwane są pliki do scalenia: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET


---------------------------------------UTWÓRZ SPIS TREŚCI DLA TWORZONEGO PLIKU .MD--------------------------------------

Wybierz rodzaj spisu treści jaki chcesz utworzyć
1. Spis treści będący zwykłym tekstem
2. Spis treści złożony z hiperlinków do odpowiednich paragrafów

Podaj numer typu wybranego z powyższego menu:
```

If you write anything else then '1' or '2', error message will be displayed and you will be asked to choose again.

### Plain text table of contents view
You will be transported to this view if you choose option '1' in [previous one](#table-of-contents-menu-view). As in the previous views on top of the widow you can find absolute path to main directory. Beneath it is text of table of contents. It will contain second level header with table of contents title (`## Spis treści`), followed by list of headers of appropriate level. Each header is either subdirectory name (if there is more then one selected file in that subdirectory) or selected file title (if file has a title, if it doesn't then it is replaced with file name). So first we have headers with titles of files that are placed directly in main directory, then name of subfolder (if there is more then selected file in that subfolder), titles of files that are in that subfolder and so on.

Example window view:
```
Katalog główny, w którym wyszukiwane są pliki do scalenia: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

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
Now you can close the program entirely by pressing **Esc** button or go back to [main menu](#main-menu-view) by pressing **Enter** button (or anything else except Esc).

### Hyperlinks table of contents view
You will be transported to this view if you choose option '2' in [table of content menu view](#table-of-contents-menu-view). This view looks analogically to [the previous one](#plain-text-table-of-contents-view), except it displays other type of table contents. Table of contents is build according to the same rules as the one above. The only difference is that headers containing selected files titles (or names) aren't plain text, but hyperlinks transporting reader to right section of merged file.

Example window view:
```
Katalog główny, w którym wyszukiwane są pliki do scalenia: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET

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
Just as in [previous view](#plain-text-table-of-contents-view) you can now close the program entirely by pressing **Esc** button or go back to [main menu](#main-menu-view) by pressing **Enter** button (or anything else except Esc).

## Merged file menu view
You will be transported to this view if you choose '4' in [main menu view](#main-menu-view). Here you will be able to change some settings of merged file before creating it. Just like in most views at the top you can find main directory path. Then there is view title and currently set merged file settings. You can set three values for merged file:
1. merged file name (default value is in the `MERGE_FILE_NAME` `Program` class constant and is set to `"README.md"`),
2. path of the directory in which merged file will be created (by default it is set to main directory path),
3. merged file title (default value is in the `MERGED_FILE_TITLE` `Program` class constant and is set to `"Kurs \"Zostań programistą ASP.NET\" - notatki"`).

Currently set values for those settings are located after colons.

Beneath currently set values there is a menu, that allows you to change one of this settings. So if you want to change
1. merged file name write **_1_**, and you will be transported to [view](#change-merged-file-name-view), that will allow you to do it
2. path of the directory in which merged file will be created, write **_2_**, and you will be transported to [view](#change-merged-file-directory-path-view), that will allow you to do it
3. merged file title, write **_3_**, and you will be transported to [view](#change-merged-file-title-view), that will alow you to do just that.

After changing any of those setting you will be transported back to this view, so don't worry, you can change all the settings that you want.

When all merged file settings have desired values, press **Enter** to create merged file. You will be transported to [create merged file view](#create-merged-file-view) and the file will be created in set directory.

Example window view:
```
Katalog główny, w którym wyszukiwane są pliki do scalenia: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET


--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------

Wybrane pliki zostaną połączone w plik: README.md
Który zostanie zapisany w katalogu: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET
Nowy plik będzie mieć nagłówek: Kurs "Zostań programistą ASP.NET" - notatki

Jeżeli chcesz zmienić któreś z tych ustawień wybierz odpowiedni numer z poniższego menu.

1. Zmień nazwę tworzonego pliku
2. Zmień ścieżkę katalogu
3. Zmień nagłówek

Podaj numer ustawienia (1 - 3), które chcesz zmienić lub wciśnij Enter aby połączyć pliki z wybranymi ustawieniami:
```

If you enter anything other than one of numbers 1 - 3 or pressing Enter button, error message will be displayed and you will be asked to once again choose, what you want to do.

### Change merged file name view
If you chose '1' in [previous view](#merged-file-menu-view), you will be transported to this view. Here you will be asked to enter name that you want to give to your merged file. Enter only name, not the full path. You can include file extension (.md), but you don't have to. If you won't it will be automatically added at the end during file creation.

Example window view:
```

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------

Podaj nazwę tworzonego pliku:
```

After entering the name and pressing Enter, new merged file name will be set, and you will be transported back to [previous view](#merged-file-menu-view).

### Change merged file directory path view
If you chose '2' in [merged file menu view](#merged-file-menu-view), you will be transported to this view. Here you will be asked to enter path to the directory where you want your merged file to be created. Just as in the [change main directory view](#change-main-directory-view), entered path can be absolute or relative to current directory (probably the directory where program is held - probably project's MdFilesMerger\bin\Debug\net6.0 subfolder or other directory from which program was executed). If this folder doesn't exist it will be created. If program won't be able to create directory of given path (and it doesn't already exists) error message will be shown and you will be asked to enter the path again.

Example window view:
```

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------

Podaj ścieżkę do katalogu, w którym chcesz zapisać plik:
```

After entering valid directory path and pressing Enter, you will be transported back to [merged file menu view](#merged-file-menu-view), and new merged file directory path will be set.

### Change merged file title view
If you chose '3' in [merged file menu view](#merged-file-menu-view), you will be transported to this view. Here you will be asked to enter merged file title.

Example window view:
```

--------------------------------------------------POŁĄCZ WYBRANE PLIKI--------------------------------------------------

Podaj nagłówek tworzonego pliku:
```

After entering desired merged file title and pressing Enter, you will be transported back to [merged file menu view](#merged-file-menu-view), and new merged file title will be set. If you don't want your merged file to have any title, just press Enter and title will be empty. If title is empty title header won't be included in merged file.

### Create merged file view
If you didn't chosen any option in [merged file menu view](#merged-file-menu-view) pressed Enter, you will be transported to this view. Like in many other views you will see here current main directory absolute path on top of the window. Then you have [merged file menu view](#merged-file-menu-view) title and information, that merging files is in progress. After creating merged file is completed, you will be transported back to [main menu](#main-menu-view).

Example window view:
```
Katalog główny, w którym wyszukiwane są pliki do scalenia: C:\Users\mielczarek\source\repos\KursZostanProgramistaASPdotNET


---------------------------------------------------POŁĄCZ WYBRANE PLIKI---------------------------------------------------

Scalanie plików...
```
