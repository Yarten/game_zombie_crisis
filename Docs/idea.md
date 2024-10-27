# 一些想法

[TOC]

## 1. 灵感

### 1.1. 基本轮廓

这是一个 2D 的射击打僵尸的游戏。以练习为主，先做出来东西，不要考虑太多。

#### 小循环

- 射击；
- 移动与寻找；
- 控制灯光；
- 击杀、开箱、捡东西、使用物品。

#### 大循环

- 解锁新地图、区域；
- 获得更好的装备，打更难得怪；
- 一个地图重复刷，让地图达到某种状态（测绘地图、通电、清除污染源、击杀地图 BOSS等）；



### 1.2. 设定

- 亮度作为 SAN 值的设定，亮度越低，僵尸刷新得越多，但亮度越高，僵尸越猛（攻击性强、更容易引来精英怪）；
- 需要在地图中，寻找到信号器，从而产生最终逃离点；
- 考虑多人模式，分布于各个地方，一起逃离，拼凑地图；
- 走过的地方，才能展示在地图中；游戏中存在路标物体，可刷新附近地图、展示重要地标方向；



## 2. 计划

### 2.1. 整体规划

1. 搭建 prototype；
   1. 寻找素材；
   2. 搭建简单场景；
   3. 设置敌人；
   4. 完成射击效果；
   5. 设计光影效果，引入 SAN 值设置；



### 2.2. 美术

使用的素材：

- [枪，而且可以拆卸](https://assetstore.unity.com/packages/2d/characters/pixel-gun-and-throwable-294254)；
- [包含角色、怪物、特效，但是枪没得换](https://assetstore.unity.com/packages/2d/environments/super-grotto-escape-pack-238393)；
- [好看的野外场景](https://assetstore.unity.com/packages/p/pixel-art-top-down-basic-187605)；
- [**更加 mini 的角色**](https://o-lobster.itch.io/simple-dungeon-crawler-16x16-pixel-pack)；
- [**多种枪、效果、子弹**](https://humanisred.itch.io/weapons-and-bullets-pixel-art-asset)；
