@echo off
cd /d "%~dp0"
if exist update_new.exe move /y update_new.exe update.exe
start update.exe -d "%~dp0../Magic Set Editor 2" "https://github.com/247321453/MagicSetEditor2/raw/master/"
exit