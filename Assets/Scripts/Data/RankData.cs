using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 排行榜数据类，玩家数据
 */
public class RankData
{
    public string uid; //玩家id
    public string nickName; //玩家昵称
    public int avatar; //玩家头像序号
    public int trophy; //玩家奖杯数
    public string thirdAvatar; //第三方头像链接
    public int onlineStatus; //玩家是否在线 1:在线  0:离线 
    public int role; //公会内的职位
    public string abb; //公会缩写
}
