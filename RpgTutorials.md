# 从早稻叽难度教起的Nova-RPG框架使用教程

## 安装

### Unity

- 首先去Unity官网注册个账号，然后从Unity官网的下载界面找到2020版的LTS（下面的链接就是）。

https://unity.cn/releases/lts/2020

- 然后找到2020.3.13f1。
![Alt text](Graphs/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-06-25%20013634.png)

- 我的建议是从Hub下载，比较方便管理项目。

- 从Unity Hub安装的话，大概会遇到以下选项。

- 开发工具，就一个Visual Studio，用来写C#脚本的，如果不打算写完全可以不安装。
![Alt text](Graphs/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-06-25%20015605.png)

- 平台，用来导出成可执行文件的，记得把`Windows(IL2CPP)`勾上。当然要是以后想做移动端的版本也可以把iOS和Android勾上。（←目前还没做触控支持，除非是想在手机上插个鼠标键盘来玩，否则慎重考虑）
![Alt text](Graphs/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-06-25%20015946.png)

- 然后是一个好东西，语言包！把简体中文勾上。
![Alt text](Graphs/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-06-25%20020238.png)

- 然后静静地等待安装完成。

### Git

（ps:这部分内容要是会用git直接跳过就好，命令行的`git clone https://github.com/lue-trim/Nova-RPG.git`比用GUI的过程方便多了）

（pps:理论上应该有比比我年纪还大的Git更好用的同步工具，要是嫌它长得太丑可以在网上随便找个替代品，用法应该都差不多）

- 去安装个Git用来同步仓库，安装过程随意。

https://github.com/git-for-windows/git/releases/download/v2.41.0.windows.1/Git-2.41.0-64-bit.exe

- 在你的任意一个盘新建一个`Unity Projects`文件夹（其他名字也行），然后在空白处右键单击，选择`Git GUI Here`。
![Alt text](Graphs/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-06-25%20014457.png)

- 选择`Clone existing repository`
![Alt text](Graphs/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-06-25%20014623.png)

- Source Location输入`https://github.com/lue-trim/Nova-RPG.git`，点击Clone，等它自己跑完。
![Alt text](Graphs/%E5%B1%8F%E5%B9%95%E6%88%AA%E5%9B%BE%202023-06-25%20015201.png)

### 导入项目
- 打开Unity Hub，选择右上角那个超级大的打开键，打开刚刚Clone下来的项目文件夹。（太简单了以至于我觉得不需要附图）
