[DataEditorX]2.3.5.1[DataEditorX]
[URL]https://github.com/247321453/DataEditorX/raw/master/win32/win32.zip[URL]

★运行环境(Environment)
本程序基于.Net framework 2.0(3.5)开发
必备DLL：
System.Data.SQLite.dll				数据库操作
FastColoredTextBox.dll				脚本编辑
WeifenLuo.WinFormsUI.Docking.dll	多标签

winXP(需安装.Net2.0)
win7(自带.Net2.0)
win8(需要安装.Net3.5包含2.0)


★文件关联(File association)
.cdb (必选)DataEditorX
.lua (可选)Notepad++/Sublime text/DataEditorX
方法：
右键文件，打开方式，浏览文件，选择，点击确定。
注意：
默认，lua是用本程序打开，如果需要修改，在★设置 open_file_in_this设为false

★bug反馈(Feedback)
不接受任何bug和建议。

★设置
DataEditorX.exe.config ★语言设置，★图片设置，★CodeEditor设置
data/language_xxx.txt 界面和消息提示文字
data/cardinfo_xxx.txt 种族，类型，系列名

★其他设置
async					后台加载数据（显示快）为true，直接加载数据为false
opera_with_cards_file	修改，删除卡片的同时，也会删除，修改，复制卡图和脚本。
open_file_in_this		用自带的脚本编辑器打开lua

★语言设置
DataEditorX.exe.config
<add key="language" value="chinese" />简体
其他语言请自己添加，需要2个文件，xxx为语言
data/language_xxx.txt
data/cardinfo_xxx.txt

★图片设置
在裁剪和导入图片时候使用。
	image_quilty	保存的图片质量 1-100
	image			游戏图片大小，小图宽，高，大图宽，高，共4个值
	image_other		一般卡图裁剪
	image_xyz		xyz卡图裁剪
	image_pendulum	Pendulum卡图裁剪
★CodeEditor设置
	IME			使用输入法
    wordwrap	自动换行
    tabisspace	tab转换为space
	fontname    字体名
	fontsize	字体大小

★DataEditor：
攻击力为？，可以输入？，?，-2任意一个都可以。
文件夹pics和script和cdb所在文件夹一致。

★修改卡片密码
删除，则是直接修改密码
不删除，就是复制一张新卡片

★从ydk和图片文件夹读取卡片列表
支持：密码，带0密码，卡名的png，jpg图片

★MSE存档
读取
存档结构：(要求：每张卡的内容，开头是card，最后一行是gamecode,在MSE的card_fields修改gamecode为最后的元素)
card:
...
	gamecode: 123456
card:
....
	gamecode: 123456

★MSE图片
支持：密码，带0密码，卡名的png，jpg图片
在“设置为MSE图片库”（“Set MSE'Image ”）的打勾时候，导入卡图都是放到MSE的图片文件夹

★导出数据
例如：mydiy.cdb
新卡：deck/mydiy.ydk
说明：mydiy.txt
脚本script
图片pics
生成mydiy.zip，可以放在音效版的expansions，直接使用，也可以用来发布。

★数据库对比

★Lua函数查找
从C++源码获取Lua函数
返回类型，参数类型，C++实现代码。

★卡片复制：
替换复制：如果存在卡片，就用最新的替换
不替换复制：如果存在卡片，就跳过

★卡片搜索
1.仅支持第一个系列名搜索,暂不支持P的刻度搜索
2.支持卡片名称，效果描述，规则，属性，等级，种族，卡片类型，效果类型，密码
3.ATK,DEF搜索：
	如果是0，则输入-1或者.搜索
	如果是?，则输入-2或者?或者？搜索
4.卡片名称搜索：
	AOJ%%		以“AOJ”开头
	流%%天		以“流”开头，“天”结尾
	%%战士		以“战士”结尾

5.密码范围搜索示例：
--密码或同名卡为10000000，的卡片        卡片密码： 10000000 同名卡: 0
--同名卡为10000000的卡片               卡片密码：  0       同名卡: 10000000 
--大于密码10000000，小于20000000的卡片  同名卡：  10000000   卡片密码: 20000000

★CodeEditor：
在下面的文本框输入关键字，按Enter
Ctrl+F			查找
Ctrl+H			替换
Ctrl+鼠标左键 	跳转到函数定义
Ctrl+K			函数列表
Ctrl+T			常量列表
Ctrl+鼠标滑轮 	缩放文字

★Magic Set Editor 2
下载/更新："Magic Set Editor 2/download.bat"
或者
https://github.com/247321453/MagicSetEditor2

★MSE存档生成设置
在每个语言的mse_xxx.txt修改\r\n会替换为换行，\t会替换为tab
简体转繁体，
cn2tw = false
每个存档最大数，0则是无限
maxcount = 0
从下面的文件夹找图片添加到存档，名字为密码/卡名.png/jpg
imagepath = ./Images
魔法陷阱标志，%%替换为符号，如果只是%% ，需要设置下面的ST mark is text: yes
spell = [魔法卡%%]
trap = [陷阱卡%%]
游戏yugioh，风格standard，语言CN，Edition：MSE，P怪的中间图不包含P文本区域
head = mse version: 0.3.8\r\ngame: yugioh\r\nstylesheet: standard\r\nset info:\r\n\tlanguage: CN\r\n\tedition: MSE\r\n\tST mark is text: no\r\n\tpendulum image is small: yes
读取存档，卡片描述
text =【摇摆效果】\n%ptext%\n【怪兽效果】\n%text%\n
获取P文本
pendulum-text = 】[\s\S]*?\n([\S\s]*?)\n【
获取怪兽文本
monster-text = [果|介|述|報]】\n([\S\s]*)
替换特数字
replace = ([鮟|鱇|・|·]) <i>$1</i>
把空格替换为^，（占1/3字宽）
#replace = \s <sym-auto>^</sym-auto>
把A-Z替换为另外一种字体
#replace = ([A-Z]) <i>$1</i>