# DataEditorX
Manage card database(.cdb file) for [ygopro](https://github.com/Fluorohydride/ygopro).

## Download
Grab the lastest build from Appveyor in the latest commit of https://github.com/purerosefallen/DataEditorX/commits/master.

> **FAQ**   
Q: I can't run the program.   
A: Please install [.NET Framework](https://www.microsoft.com/en-us/download/details.aspx?id=25150).

## Features
* Create and edit card databases.   
* Compare, copy and paste card records across databases easily.   
* Read card records from ygopro decks(.ydk file) or card picture folders(eg. pics folder of ygooro).  
* Create and edit card scripts(.lua file).  
* Export and import [MSE](https://github.com/247321453/MagicSetEditor2) sets.   
...

> **FAQ**   
Q: How to add a new archetype?  
A: First decide the setcode (a hex number) for the new archetype. Do not confict the existing setcodes. Then type it in the text box on the right of the combo box of archetype. Click Modify. To show the name of the new archetype in the combo box. Open data/cardinfo_xxx.txt (xxx is language), add a new line between "##setname" and "#end", write the setcode (start with 0x) and the archetype name separated by a Tab symbol.

## Language
Open Menu Help-->Language to choose language, then restart the application.   
If you want to add a language xxx for DataEditorX, you need two files:    
>data/language_xxx.txt for graphic interface   
data/cardinfo_xxx.txt for card information    

Each line in language_english.txt/cardinfo_english.txt is separate by a Tab. Translate the content on the right of Tab then put them in language_xxx.txt/cardinfo_xxx.txt.

## Special Features of KoishiDEX
1. The format of scripts will be in Koishi-Style when creating new scripts. Also the module script to be required will be adjustable.
2. Scripts of Non-Pendulum Normal monsters will be openable, for creating module scripts.
3. Will ignore the card alias when opening a script.
