# SystrayLauncher
任务栏程序启动器。Systray app launcher.

![屏幕截图](https://raw.githubusercontent.com/jjling2011/SystrayLauncher/main/screenshots/systray.png)

## 下载
[https://github.com/jjling2011/SystrayLauncher/releases/latest](https://github.com/jjling2011/SystrayLauncher/releases/latest)  

## 用法
把要常用程序的快捷方式放入shortcuts目录中，然后运行本软件。  
程序较多时可以创建文件夹，分类存放。  

## 特殊用法
需要管理员权限时，比如修改“hosts”，先把快捷方式的“目标”修改为：
```
%windir%\system32\notepad.exe %SystemRoot%\System32\drivers\etc\hosts
```
然后点“高级”，勾上“用管理员身份运行”。

## 开机启动
点击"工具"-"打开自启动文件夹"，拖个快捷方式进去即可。  
