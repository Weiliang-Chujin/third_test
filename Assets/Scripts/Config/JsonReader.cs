using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

/*
 * json数据读取类，读取排行榜数据
 */
public class JsonReader 
{
    //解析json数据
    public static JSONNode ReadJson()
    { 
        TextAsset txtobj = (TextAsset)Resources.Load("Json/ranklist");
        JSONNode json = JSONNode.Parse(txtobj.text);
        return json;
    }
    
    //获取倒计时数据
    public static int ReadCountdown()
    {
        return ReadJson()["countDown"];
    }
    
    //获取排行榜数据并排序
    public static RankData[] ReadRankData()
    {
        JSONNode jsonRankData = ReadJson()["list"];
        RankData[] rankData = new RankData[jsonRankData.Count];
        for (int i = 0; i < jsonRankData.Count; i++)
        {
            var model = new RankData()
            {
                uid = jsonRankData[i]["uid"],
                nickName = jsonRankData[i]["nickName"],
                avatar = jsonRankData[i]["avatar"],
                trophy = jsonRankData[i]["trophy"],
                thirdAvatar = jsonRankData[i]["thirdAvatar"],
                onlineStatus = jsonRankData[i]["onlineStatus"],
                role = jsonRankData[i]["role"],
                abb = jsonRankData[i]["abb"]
            };
            rankData[i] = model;
        }
        
        //根据奖杯数降序排序
        Array.Sort(rankData, (x, y) =>
        { 
            return y.trophy - x.trophy;
        });

        return rankData;
    }
    
}