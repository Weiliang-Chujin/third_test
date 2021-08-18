using System.Collections;
using System.Collections.Generic;
using System.Text;
using Com.TheFallenGames.OSA.DataHelpers;
using UnityEngine;
using UnityEngine.UI;

/*
 * 排行榜item视图，初始化排行榜信息
 */
public class RankItemView : MonoBehaviour
{
    public Image myRankImage; //自己的排名图片
    public Text myUid; //自己的uid文字
    public Text myNickName; //自己的昵称文字
    public Image myLevelImage; //自己的段位图片
    public Text myTrophy; //自己的奖杯数

    public void InitializeRankItemInfo(SimpleDataHelper<RankData> Data, MyListItemViewsHolder newOrRecycled)
    {
        StringBuilder stringBuilder = new StringBuilder();
        RankData model = Data[newOrRecycled.ItemIndex];
        int rank = newOrRecycled.ItemIndex + 1; //排名
        int level = 0; //段位
        float rankImageSizeScale = 0.75f; //排名图片缩放大小,三个排名图片大小不一样
        float levelImageSizeScale = 0.25f; //段位图片缩放大小，段位图片的大小都不一样

        newOrRecycled.uid.text = model.uid;
        newOrRecycled.nickName.text = model.nickName;
        newOrRecycled.avatar.sprite = Resources.Load<Sprite>("Images/userHead");
        newOrRecycled.cupImage.sprite = Resources.Load<Sprite>("Images/trophy");
        newOrRecycled.trophy.text = model.trophy.ToString();
        newOrRecycled.rankText.text = rank.ToString();
        
        //前三背景图片和排名图片跟普通排名显示不一样
        if (rank <= 3)
        {
            stringBuilder.Append("Images/rank list_");
            stringBuilder.Append(rank);
            newOrRecycled.backgroundImage.sprite = Resources.Load<Sprite>(stringBuilder.ToString());
            stringBuilder.Clear();

            stringBuilder.Append("Images/rank_");
            stringBuilder.Append(rank);
            newOrRecycled.rankImage.sprite = Resources.Load<Sprite>(stringBuilder.ToString());
            newOrRecycled.rankImage.rectTransform.sizeDelta = new Vector2(newOrRecycled.rankImage.sprite.rect.width * rankImageSizeScale, 
                newOrRecycled.rankImage.sprite.rect.height * rankImageSizeScale);
            stringBuilder.Clear();
            
            newOrRecycled.rankImage.color = new Color(1, 1, 1, 1);
            newOrRecycled.rankText.color = new Color(1, 1, 1, 0);
        }
        else
        {
            newOrRecycled.backgroundImage.sprite = Resources.Load<Sprite>("Images/rank list_normal");
            newOrRecycled.rankImage.color = new Color(1, 1, 1, 0);
            newOrRecycled.rankText.color = new Color(1, 1, 1, 1);
        }
        
        //段位计算
        level = model.trophy / 1000 + 1;
        newOrRecycled.levelImage.sprite = Resources.Load<Sprite>(string.Concat("Images/段位/arenaBadge_", level));
        newOrRecycled.levelImage.rectTransform.sizeDelta = new Vector2(newOrRecycled.levelImage.sprite.rect.width * levelImageSizeScale,
                                                                    newOrRecycled.levelImage.sprite.rect.height * levelImageSizeScale);

        //给自己信息处添加信息
        if (newOrRecycled.ItemIndex == 0)
        {
            myRankImage.sprite = Resources.Load<Sprite>("Images/rank_1");
            myRankImage.rectTransform.sizeDelta = new Vector2(newOrRecycled.rankImage.sprite.rect.width * rankImageSizeScale,
                newOrRecycled.rankImage.sprite.rect.height * rankImageSizeScale);
            myUid.text = model.uid;
            myNickName.text = model.nickName;
            myLevelImage.sprite = Resources.Load<Sprite>(string.Concat("Images/段位/arenaBadge_", level));
            myLevelImage.rectTransform.sizeDelta = new Vector2(myLevelImage.sprite.rect.width * levelImageSizeScale, 
                myLevelImage.sprite.rect.height * levelImageSizeScale);
            myTrophy.text = model.trophy.ToString();
        }
    }
}
