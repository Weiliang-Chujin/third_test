### 1.整体框架  

 读取json数据保存排行榜数据，点击展示排行榜，排行榜有倒计时，每秒刷新内容，点击某个玩家item，可显示玩家信息。  

### 2.界面结构     

MainScene：包含三个容器，TopPanel、BottomPanel、Mask（空界面）     

BottomPanel：一个按钮（展示或关闭排行榜）。

TopPanel：OSA（OSA插件所做的排行榜信息）、MyInfo（自己的排行信息）、PopupWindoPanel（玩家信息弹窗）、TimeText（倒计时）

OSA的BasicListAdapterItem容器包括BackgroundImage（背景图）、RankFirstImage（第一名排名图片）、RankSecondImage（第二名排名图片）、RankThirdImage（第三名排名图片）、RankText（排名文字）、Avatar（玩家头像）、Uid（玩家id）、NickName（玩家昵称）、LevelImage（玩家段位图片）、CupImage（玩家奖杯图片）、Trophy（玩家奖杯数）、PopupShowBtn（按钮点击用于显示弹窗信息）

MyInfo包括MyRankImage（自己的排名图片）、MyAvatar（自己的头像）、MyUid（自己的id）、MyNickName（自己的昵称）、MyLevelImage（自己的段位图片）、MyCupImage（自己的奖杯图片）、MyTrophy（自己的奖杯数）
			    
### 3.代码结构

| 类名                  | 功能                                 | 调用关系                                                     |
| --------------------- | ------------------------------------ | ------------------------------------------------------------ |
| RankData              | 保存排行榜的玩家数据                 | 被JsonReader类调用                                           |
| MyListItemViewsHolder | 保存BasicListAdapterItem组件信息     | 被BasicListAdapter类调用                                     |
| JsonReader            | 读取排行榜json数据，并对数据降序排序 | 调用RankData类，被BasicListAdapter类调用                     |
| GameController        | 控制排行榜显示与关闭                 | 无                                                           |
| PopupWindowController | 显示弹窗的显示与关闭                 | 无                                                           |
| RankItemController    | 初始化排行榜每个玩家信息             | 被BasicListAdapter类调用                                     |
| BasicListAdapter      | OSA插件类，用于排行榜数据显示与刷新  | 调用MyListItemViewsHolder类、JsonReader类和RankItemController类 |


### 4.流程图

![flowPath](https://github.com/89trillion-hehuan/third_test/blob/main/FlowChart.png)
