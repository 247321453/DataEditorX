﻿[DataEditorX]1.6.6.0[DataEditorX]
[URL]https://github.com/247321453/DataEditorX/raw/master/win32/win32.zip[URL]

★使用前，请关联lua的打开方式，例如记事本，notepad++，等。

★支援Magic Set Editor 2
下载/更新：
Magic Set Editor 2/update/download.bat

★MSE存档生成设置
config文件，设置pendulum文本和普通文本的正则正则表达式，用来分离文本
mse-head 		MSE的风格设置文件
mse-monster		普通怪兽模版
mse-pendulum	P怪兽模版
mse-spelltrap	魔陷模版
mse-italic		特数字替换，达到一个位置使用2种字体的效果


★支持关联cdb文件，命令参数启动。
关联cdb文件：
请确保DataEditorX的文件夹名固定不变，然后右键随意一个cdb文件，打开方式--浏览--DataEditorX.exe。确定。
以后双击cdb文件即可打开DataEditorX。

★支持 新建文本文档.txt 直接改名 新建文本文档.cdb

★文件夹pics和script和cdb所在文件夹一致。

★卡片复制：
替换复制：如果存在卡片，就用最新的替换
不替换复制：如果存在卡片，就跳过

★卡片搜索
1.不支持系列名，ATK，DEF搜索
2.支持卡片名称，描述，规则，属性，等级，种族，卡片类型，效果类型，密码
3.密码范围搜索示例：
--密码或同名卡为10000000，的卡片        卡片密码： 10000000 同名卡: 0
--同名卡为10000000的卡片               卡片密码：  0       同名卡: 10000000 
--大于密码10000000，小于20000000的卡片  同名卡：  10000000   卡片密码: 20000000

★bug反馈
Email:247321453@qq.com
提交版本前，请检查更新。

支持多语言化
DataEditorX.exe.config
<add key="language" value="chinese" />简体
<add key="language" value="english" />英文

标题：(DataEditorX+版本号)
内容：
错误提示文字：（弹出出错框，请按Ctrl+C，然后找地方粘贴）
详细描述：（卡片信息，杀毒软件，本程序目录等等）

注意：
我之修复Email提交的bug。
描述不详细的bug，我修复不了。（都不知道是bug是什么）

★更新历史
1.6.6.0
mse-config.txt添加注释
1.6.5.0
改进自定义魔法陷阱
1.6.4.0
修复setcode输入错误
搜索为空的错误
1.6.3.0
为无种族的token添加token card类型
1.6.2.2
修复没有种族的token
1.6.2.1
修复导出MSE存档
1.6.2.0
MSE存档导出，修正english的魔法陷阱标志
增加单系列搜索
1.6.1.0
把config的MSE设置改为chinese(english)/mse-config.txt
1.6.0.0
增加简体转繁体功能
mse-italic.txt支持正则替换
1.5.5.2
增加MSE的mse-italic.txt
1.5.5.1
修复第2次导入图片，出bug
1.5.5.0
完成导出MSE存档，简体测试OK
注：config设置P描述和正常描述的分离的正则表达式
mse-head.txt的language设置语言：CN,TW,JP,EN,KO
1.5.4.0
setcode编辑框
1.5.3.0
增加压缩数据库
1.5.2.1
导入卡图的路径改为cdb的目录的pics
1.5.2.0
修复复制卡片的替换
增加批量导入卡图
1.5.1.1
改MSE更新器的默认路径
1.5.1.0
完善系列框,等待导出MSE存档
1.5.0.0
修复卡名搜索，读取ydk，读取图片
添加导出MSE存档，裁剪图片
可以记住最后打开数据库
1.4.1.0
添加撤销上一次操作。
1.4.0.0
增加多语言文件的可修改性。
1.3.4.2
新建数据库，改为提示是否打开。
打开空白数据库，将会清空当前列表和内容。
1.3.4.1
ATK/DEF输入？，自动转-2
1.3.4.0
支持 新建文本文档.txt 直接改名 新建文本文档.cdb
1.3.3.0
修复打开方式
1.3.2.2
整理代码
1.3.2.1
完善language.txt
1.3.2.0
修复
1.3.1.0
添加下载文件
1.3.0.1
分文件夹
1.3.0.0
txt文件，顺序可以打乱，关键是数值不能重复
支持多语言化
1.2.1.2
按密码搜索一样会显示同名卡
更改密码搜索
按密码搜索：密码框>0 同名框=0
按同名搜索：密码框=0 同名框>0
按密码范围搜索：密码框>0 同名框>0
1.2.1.1
自动把游戏目录设为cdb的目录
1.2.1.0
更改检查更新网址为我的百度空间
1.2.0.0
添加检查新版本
修复setname
