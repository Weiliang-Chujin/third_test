using System.Collections;
using System.Collections.Generic;
using Com.TheFallenGames.OSA.DataHelpers;
using UnityEngine;
using UnityEngine.UI;

/*
 * 排行榜item控制，初始化排行榜信息
 */
public class RankItemController : MonoBehaviour
{
    public Text MyUid; //自己的uid文字
    public Text MyNickName; //自己的昵称文字
    public Image MyLevelImage; //自己的段位图片
    public Text MyTrophy; //自己的奖杯数

    public void InitializeRankItemInfo(SimpleDataHelper<RankData> Data, MyListItemViewsHolder newOrRecycled)
    {
        RankData model = Data[newOrRecycled.ItemIndex];
        int rank = newOrRecycled.ItemIndex + 1; //排名
        int level = 0; //段位

        newOrRecycled.uid.text = model.uid;
        newOrRecycled.nickName.text = model.nickName;
        newOrRecycled.avatar.sprite = Resources.Load<Sprite>("Images/userHead");
        newOrRecycled.cupImage.sprite = Resources.Load<Sprite>("Images/trophy");
        newOrRecycled.trophy.text = model.trophy.ToString();
        newOrRecycled.rankText.text = rank.ToString();
        
        //前三背景图片和排名图片跟普通排名显示不一样，且前三名的排名图片都不一样大，做了三个是适配的图片展示前三名的排名图片
        switch (rank)
        {
            case 1: 
                newOrRecycled.backgroundImage.sprite = Resources.Load<Sprite>("Images/rank list_1");
                newOrRecycled.rankFirstImage.sprite = Resources.Load<Sprite>("Images/rank_1");
                newOrRecycled.rankFirstImage.color = new Color(1, 1, 1, 1);
                newOrRecycled.rankSecondImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankThirdImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankText.color = new Color(1, 1, 1, 0);
                break;
            case 2:
                newOrRecycled.backgroundImage.sprite = Resources.Load<Sprite>("Images/rank list_2");
                newOrRecycled.rankSecondImage.sprite = Resources.Load<Sprite>("Images/rank_2");
                newOrRecycled.rankFirstImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankSecondImage.color = new Color(1, 1, 1, 1);
                newOrRecycled.rankThirdImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankText.color = new Color(1, 1, 1, 0);
                break;
            case 3:
                newOrRecycled.backgroundImage.sprite = Resources.Load<Sprite>("Images/rank list_3");
                newOrRecycled.rankThirdImage.sprite = Resources.Load<Sprite>("Images/rank_3");
                newOrRecycled.rankFirstImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankSecondImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankThirdImage.color = new Color(1, 1, 1, 1);
                newOrRecycled.rankText.color = new Color(1, 1, 1, 0);
                break;
            default:
                newOrRecycled.backgroundImage.sprite = Resources.Load<Sprite>("Images/rank list_normal");
                newOrRecycled.rankFirstImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankSecondImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankThirdImage.color = new Color(1, 1, 1, 0);
                newOrRecycled.rankText.color = new Color(1, 1, 1, 1);
                break;
        }
        
        //段位计算
        level = model.trophy / 1000 + 1;
        newOrRecycled.levelImage.sprite = Resources.Load<Sprite>(string.Concat("Images/段位/arenaBadge_", level));
			
        //给自己信息处添加信息
        if (newOrRecycled.ItemIndex == 0)
        {
            MyUid.text = model.uid;
            MyNickName.text = model.nickName;
            MyLevelImage.sprite = Resources.Load<Sprite>(string.Concat("Images/段位/arenaBadge_", level));
            MyTrophy.text = model.trophy.ToString();
        }
    }
}
