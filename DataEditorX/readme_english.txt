
★Environment
This program based on .Net2.0/winXP(need .Net2.0)/win7(with.Net2.0)/win8(need.Net3.5 include 2.0)

★File association
.lua Notepad++/Sublime text/DataEditorX
.cdb DataEditorX

Click file with right mouse button, Open with, Browse Files, Choose confirm(Yes）

★Feedback
Email:247321453@qq.com
Title：DataEditorX X.X.X.X
Text：
The error message text: If there is a error message box, please press Ctrl+C, then paste in someplace.
please give a detailed description of: card message; antivirus; programe location;the operation that time.


★Setting
DataEditorX.exe.config ★Language，★Image，★CodeEditor
data/language_xxx.txt Interface and prompt message 
data/cardinfo_xxx.txt types/series


★Language setting
DataEditorX.exe.config
<add key="language" value="chinese" />Simplified Chinese 
chinese => english
If you want to add a language，you need two file，xxx is a type of language:
data/language_xxx.txt
data/cardinfo_xxx.txt


★Image setting
you need it when you want to add or pendulum.
	image_quilty	        1-100
	image			Width/height of image and thumbnail,total four numerical value; 
	image_other		pendulum of other cards
	image_xyz		pendulum of Xyz monsters
	image_pendulum	        Pendulum




★CodeEditor Setting   
       IME			Input Method Editors
    wordwrap	 
    tabisspace	tab→space
	fontname     
	fontsize	 



★DataEditor：
If you need to input Attack "?", you can use anyone of ？/?/-2 instead. 
The folder of pics, script and cdb should be in a same folder consistent.



★Read cardlist from ydk and folder pics
Support：png,jpg pics with card number/card number with 0



★Output data
Example：mydiy.cdb
New card：deck/mydiy.ydk
Instruction：mydiy.txt
script
pics
↑ all of them in mydiy.zip，you can use it in expansions of ygopro with sound effect or release it.



★Database comparison

★Lua search
Find lua from C++ Source
Return in parameter type, C++ implement code

★Copy a card：
Copy and Replace: If there's a card with same name, replace it.
Copy without Replace: If there's a card with same name, ignore it.


★Card search
1.Now it can not support search by Pendulum Scale 
2.You can search card with card name/effect/Attribute/Types/Level（racnk）/effect type/card number
3.Search by ATK,DEF：
	If there is a "0", input"-1"or"."
	If there is a "?", input"-2"or"?"
4.Search by card name：
	AOJ%%		start with AOJ
	Shooting%%Dragon		start with “Shooting” and end with “Dragon”
	%%Warrior		end with “Warrior”

5.Search by card number
--A card(or a card with same name) with card number of 10000000，
  card number： 10000000  card with same name: 0

--The same name card with card number:10000000
  card number：  0        card with same name: 10000000 .

--Card number over 10000000,less than 20000000
  card with same name: 10000000  card number: 10000000



★CodeEditor：
Input keyword in the under text box，press Enter
Ctrl+F			Look up
Ctrl+H			Replace
Ctrl+left mouse 	skip to function definition
Ctrl+K			List of function 
Ctrl+T			List of constant 
Ctrl+The mouse wheel 	Zoom in/out


★Magic Set Editor 2
Download/update："Magic Set Editor 2/download.bat"
Or
https://github.com/247321453/MagicSetEditor2



★MSE pics
Support：png,jpg pics with card number/card number with 0
Tick “Set MSE'Image ”，Import pics will go into pics folder under MSE


★MSE flie making setting
mse_xxx.txt modify\r\n can be newlines，\t will replace for tab

Turn Simplified into traditional，
cn2tw = false

The maxcount of every file，0 stands for unlimited
maxcount = 0

Add image from the folder，pic name:card number(or card name).png/jpg
imagepath = ./Images

Symbol of spell and traps，turn %% into your sign，If you want to use %% ，put the "ST mark is text: yes"
spell = [魔法卡%%]
trap = [陷阱卡%%]

Game name:yugioh，Style:standard，Language:CN，Edition：MSE，pics of pendulum monster don't include the text box in the central
head = mse version: 0.3.8\r\ngame: yugioh\r\nstylesheet: standard\r\nset info:\r\n\tlanguage: CN\r\n\tedition: MSE\r\n\tST mark is text: no\r\n\tpendulum image is small: yes

Read flie text
text =【摇摆效果】\n%ptext%\n【怪兽效果】\n%text%\n

Obtain Pendulum-text
pendulum-text = 】[\s\S]*?\n([\S\s]*?)\n【

Obtain monster-text
monster-text = [果|介|述|報]】\n([\S\s]*)

Replace speical number
replace = ([鮟|鱇|?|·]) <i>$1</i>

Replace blank space with^，（takes a third of a word）
#replace = \s <sym-auto>^</sym-auto>
Change A-Z into another Typeface
#replace = ([A-Z]) <i>$1</i>