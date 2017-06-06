# BlueBird

化身为鸟，追寻幸福

## 当前进程

序章关卡-1制作

+ 鸟类控制：shawnwu
+ 卡片动作：carol
+ 背景建模：cellzero

## 文件结构

+ \_Demos，保存正式项目中不会出现、试验使用的小场景，小脚本等。用于备份，以便查询。
+ \_Scenes，游戏用到的场景文件，所有场景都将放到这里。
+ Externals，保存使用的外部Assets，导入（Import）的Assets都放入该文件夹，以便查找。
+ Resources，各种资源文件，包括美术、音乐等内容。
+ Scripts，游戏用到的代码脚本。

## 命名空间

为了避免类名冲突，BlueBird暂时使用两种命名空间。
1. 用于测试场景（Demos）的脚本，命名空间为**BlueBird.Demos**。
2. 用于正式场景的脚本，命名空间为**BlueBird**。
