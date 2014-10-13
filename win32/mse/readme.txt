客户端使用：
注意：
1、更新的时候，请不要打开游戏目录。
2、知道显示更新完成，才能关闭本程序。

3、客户端改名：
例如：
自动更新.exe
自动更新.exe.bat
自动更新.exe.config

设置：
保存的文件夹
key="path" value="D:\ygopro"

如果文件已经存在，则跳过，不存在则下载。
key="ignore1" value="textrue/*"
key="ignore上面的数字+1" value="忽略文件的相对路径，允许通配符*"

下载的地址(最后必须为/)
key="url" value="https://github.com/247321453/ygocore-update/raw/master/"

代理设置（可无视）
useproxy的value为true（小写），则通过代理下载文件
key="useproxy" value="false"
key="proxy" value="127.0.0.1:8080"

服务端使用：
运行update.exe.bat，即可生成对应的文件列表

update.exe -m "【需要更新的文件夹】"
【需要更新的文件夹】后最后不能为\
例如：
错误 update.exe -m "D:\pro files\"
正确 update.exe -m "D:\pro files"

注意：
【需要更新的文件夹】为D:\game
update.exe在D:\game\update	【需要更新的文件夹】的子目录
则可以直接使用update.exe -m


结果保存在 【需要更新的文件夹】\update

然后再修改rename和delete文件的内容
注意：delete先执行
