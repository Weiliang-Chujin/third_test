using System.Collections;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.Core;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using UnityEngine;

/*
 * 获取BasicListAdapterItem下的组件，保存BasicListAdapterItem组件信息
 */
public class MyListItemViewsHolder : BaseItemViewsHolder
{
    public Image backgroundImage; //背景图片
    public Image rankImage; //排名图片
    public Text rankText; //排名文字
    public Image avatar; //头像图片
    public Text uid; //玩家id文字
    public Text nickName; //玩家昵称文字
    public Image levelImage; //段位图片
    public Image cupImage; //奖杯图片
    public Text trophy; //玩家奖杯数文字

    public override void CollectViews()
    {
        base.CollectViews();
			
        //获取BasicListAdapterItem下的组件
        root.GetComponentAtPath("BackgroundImage", out backgroundImage);
        root.GetComponentAtPath("RankImage", out rankImage);
        root.GetComponentAtPath("RankText", out rankText);
        root.GetComponentAtPath("Avatar", out avatar);
        root.GetComponentAtPath("Uid", out uid);
        root.GetComponentAtPath("NickName", out nickName);
        root.GetComponentAtPath("LevelImage", out levelImage);
        root.GetComponentAtPath("CupPanel/CupImage", out cupImage);
        root.GetComponentAtPath("CupPanel/Trophy", out trophy);
    }
}