# 此项目用于记录学习<从零开发3D跑酷游戏>

## Document

### Basic

* [修改材质颜色](wiki/basic/修改材质颜色.md)
* [修改组件执行顺序](wiki/basic/修改组件执行顺序.md)
* [协程](wiki/basic/协程.md)
* [单例和代理](wiki/basic/单例和代理.md)
* [移动](wiki/basic/移动.md)

### Example

* [动画管理器](wiki/basic/动画管理器.md)
* [添加跳跃和重力](wiki/basic/添加跳跃和重力.md)
* [获取手势方向](wiki/basic/获取手势方向.md)

---

## 开发步骤

- 把人物放到游戏0,0,0
- 把地面拼接成路面,分为两组
- 编写PlayerController让角色可以往前移动
- 编写RoadController两组路面根据用户移动距离自动换位
- 编写CarmerController, 让摄像机追随Player
- 在PlayerController里添加手势方向识别
  - 用协程一直监听用户的InputMouseDown
  - 根据起始MouseDown和结束MouseDown计算向量夹角,来确定手势方向
- 编写AnimeManager管理动画的播放
  - 给AnimeManger创建一个单例方法
  - 给AnimeManger创建一个delegate去用来保存不同的动画方法
  - 根据PlayerController里的手势结束之后,读取AnimeManger的单例,切换delegate函数,用来切换动画
  - 一些非循环的动画,读取播放时间,当normalizedTime>0.95的时候播放回跑步的动画
- 编写角色行为
  - 添加角色的左右跳动, 给一个数组, 用户的横向坐标不能超出数组的范围
  - 添加角色的上下跳动,添加多阶连跳,添加空中下滑加速落地
  - 给这些动画添加结束之后的动画, 添加在空中的idle动画