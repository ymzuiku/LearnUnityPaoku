# 此项目用于记录学习<从零开发 3D 跑酷游戏>

## Document

### Basic

* [修改材质颜色](doc/basic/修改材质颜色.md)
* [修改组件执行顺序](doc/basic/修改组件执行顺序.md)
* [协程](doc/basic/协程.md)
* [单例和代理](doc/basic/单例和代理.md)
* [移动](doc/basic/移动.md)
* [创建和销毁物体](doc/basic/创建和销毁物体.md)
* [碰撞检测](doc/basic/移碰撞检测动.md)

  ...

### Example

* [动画管理器](doc/example/动画管理器.md)
* [添加跳跃和重力](doc/example/添加跳跃和重力.md)
* [获取手势方向](doc/example/获取手势方向.md)

  ...

---

## 开发步骤

* 把人物放到游戏 0,0,0
* 把地面拼接成路面,分为两组
* 编写 PlayerController 让角色可以往前移动
* 编写 RoadController 两组路面根据用户移动距离自动换位
* 编写 CarmerController, 让摄像机追随 Player
* 在 PlayerController 里添加手势方向识别
  * 用协程一直监听用户的 InputMouseDown
  * 根据起始 MouseDown 和结束 MouseDown 计算向量夹角,来确定手势方向
* 编写 AnimeManager 管理动画的播放
  * 给 AnimeManger 创建一个单例方法
  * 给 AnimeManger 创建一个 delegate 去用来保存不同的动画方法
  * 根据 PlayerController 里的手势结束之后,读取 AnimeManger 的单例,切换 delegate 函数,用来切换动画
  * 一些非循环的动画,读取播放时间,当 normalizedTime>0.95 的时候播放回跑步的动画
* 编写角色行为
  * 添加角色的左右跳动, 给一个数组, 用户的横向坐标不能超出数组的范围
  * 添加角色的上下跳动,添加多阶连跳,添加空中下滑加速落地
  * 给这些动画添加结束之后的动画, 添加在空中的 idle 动画
* 编写金币碰撞行为
  * 编写全局状态, 用于保存当前金币
  * 在地面复制几枚金币
  * 给金币编写脚本,控制金币选择,判断是否碰到 tag.player 的物体,如果碰到就销毁自己,并且在全局状态上加一枚金币
