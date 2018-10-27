@echo off

call preview.bat

git add --all .
git commit -a -m %1
git push