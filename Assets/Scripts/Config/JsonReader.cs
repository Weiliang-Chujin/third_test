using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

/*
 * json数据读取类，读取排行榜数据
 */
public class JsonReader : MonoBehaviour
{
    public RankData[] rankData; //排行榜数据

    private void Awake()
    {
        ReadJson();
    }

    public JSONNode ReadJson()
    {
        //读取数据
        TextAsset txtobj = (TextAsset)Resources.Load("Json/ranklist");
        JSONNode json = JSONNode.Parse(txtobj.text);

        JSONNode data = json["list"];
        rankData = new RankData[data.Count];
        for (int i = 0; i < data.Count; i++)
        {
            var model = new RankData()
            {
                uid = data[i]["uid"],
                nickName = data[i]["nickName"],
                avatar = data[i]["avatar"],
                trophy = data[i]["trophy"],
                thirdAvatar = data[i]["thirdAvatar"],
                onlineStatus = data[i]["onlineStatus"],
                role = data[i]["role"],
                abb = data[i]["abb"]
            };
            rankData[i] = model;
        }
			
        //根据奖杯数降序排序
        Array.Sort(rankData, (x, y) =>
        { 
            return y.trophy - x.trophy;
        });

        return json;
    }
}