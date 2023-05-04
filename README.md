# MdFilesMerger
Application to merge all .md files in a set directory (and its subdirectories) into one file.

## Dictionary
1. Main directory - catalog in which program will search for .md files to merge. Program will search in this directory and all its subdirectories.
2. Selected files - all .md files that was found in main directory (and its subdirectories).
3. File title - first line of file that is a header (`# file title`) or next if first line(s) is\are empty.
4. Merged file - new file that contains title, table of contents (if you choose it) and content of all selected files. If selected files contain file with the same name (full path) as merged file, its content is not included in this file (old file is deleted and new one is created).

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

## Table of contents menu view

## Merged file menu view