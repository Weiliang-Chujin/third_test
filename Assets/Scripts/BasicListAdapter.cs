using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using SimpleJSON;

namespace Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists
{
	public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
	{
		public SimpleDataHelper<MyListItemModel> Data { get; private set; }
		public int TotalTime; //距离赛季刷新的时间，单位秒

		#region OSA implementation
		protected override void Awake()
		{
			Data = new SimpleDataHelper<MyListItemModel>(this);
			
			base.Awake();
			TotalTime = 600; 
			StartCoroutine(Time());

		}
		
		//通过协程实现倒计时刷新
		IEnumerator Time()
		{
			while (TotalTime >= 0)
			{
				int min = TotalTime / 60;
				int sec = TotalTime - 60 * min;
				GameObject.FindWithTag("TimeText").GetComponent<Text>().text = "Ends in:0" + min.ToString() + "m " + sec.ToString() + "s";
				yield return new WaitForSeconds(1);
				
				RetrieveDataAndUpdate();
				TotalTime--;
			}
		}
		
		protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new MyListItemViewsHolder();
			
			instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);

			return instance;
		}
		
		protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)
		{

			MyListItemModel model = Data[newOrRecycled.ItemIndex];
			int rank = newOrRecycled.ItemIndex + 1;
			
			newOrRecycled.uid.text = model.uid;
			newOrRecycled.nickName.text = model.nickName;
			newOrRecycled.avatar.sprite = Resources.Load("Images/userHead", typeof(Sprite)) as Sprite;
			newOrRecycled.cupImage.sprite = Resources.Load("Images/trophy", typeof(Sprite)) as Sprite;
			newOrRecycled.trophy.text = model.trophy.ToString();

			//前三背景图片和排名显示不一样
			switch (rank)
			{
				case 1: newOrRecycled.backgroundImage.sprite = Resources.Load("Images/rank list_1", typeof(Sprite)) as Sprite; 
						newOrRecycled.rankImage.color = new Color(1, 1, 1, 1);
						newOrRecycled.rankText.color = new Color(1, 1, 1, 0);
						newOrRecycled.rankText.text = rank.ToString();
						newOrRecycled.rankImage.sprite = Resources.Load("Images/rank_1", typeof(Sprite)) as Sprite; 
						newOrRecycled.rankImage.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 100);
					break;
				case 2: newOrRecycled.backgroundImage.sprite = Resources.Load("Images/rank list_2", typeof(Sprite)) as Sprite; 
						newOrRecycled.rankImage.color = new Color(1, 1, 1, 1);
						newOrRecycled.rankText.color = new Color(1, 1, 1, 0);
						newOrRecycled.rankText.text = rank.ToString();
						newOrRecycled.rankImage.sprite = Resources.Load("Images/rank_2", typeof(Sprite)) as Sprite; 
					break;
				case 3: newOrRecycled.backgroundImage.sprite = Resources.Load("Images/rank list_3", typeof(Sprite)) as Sprite; 
						newOrRecycled.rankImage.color = new Color(1, 1, 1, 1);
						newOrRecycled.rankText.color = new Color(1, 1, 1, 0);
						newOrRecycled.rankText.text = rank.ToString();
						newOrRecycled.rankImage.sprite = Resources.Load("Images/rank_3", typeof(Sprite)) as Sprite;
						newOrRecycled.rankImage.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 100);
					break;
				default: newOrRecycled.backgroundImage.sprite = Resources.Load("Images/rank list_normal", typeof(Sprite)) as Sprite;
					     newOrRecycled.rankImage.color = new Color(1, 1, 1, 0);
					     newOrRecycled.rankText.color = new Color(1, 1, 1, 1);
					     newOrRecycled.rankText.text = rank.ToString();
					break;
			}

			//段位计算
			int level = model.trophy / 1000 + 1;
			newOrRecycled.levelImage.sprite = Resources.Load("Images/段位/arenaBadge_" + level.ToString(), typeof(Sprite)) as Sprite;
			
			//给标题处添加自己的信息
			if (newOrRecycled.ItemIndex == 0)
			{
				GameObject.FindWithTag("uid_1").GetComponent<Text>().text = model.uid;
				GameObject.FindWithTag("nickName_1").GetComponent<Text>().text = model.nickName;
				GameObject.FindWithTag("levelImage_1").GetComponent<Image>().sprite = Resources.Load("Images/段位/arenaBadge_" + level.ToString(), typeof(Sprite)) as Sprite;
				GameObject.FindWithTag("trophy_1").GetComponent<Text>().text = model.trophy.ToString();
			}
		}

		
		#endregion
		
		#region data manipulation
		public void AddItemsAt(int index, IList<MyListItemModel> items)
		{
			Data.InsertItems(index, items);
		}

		public void RemoveItemsFrom(int index, int count)
		{
			Data.RemoveItems(index, count);
		}

		public void SetItems(IList<MyListItemModel> items)
		{
			Data.ResetItems(items);
		}
		#endregion
		
		void RetrieveDataAndUpdate()
		{
			StartCoroutine(FetchMoreItemsFromDataSourceAndUpdate());
		}
		
		IEnumerator FetchMoreItemsFromDataSourceAndUpdate()
		{
			// Simulating data retrieving delay
			yield return new WaitForSeconds(.5f);

			//读取数据
			TextAsset txtobj = (TextAsset)Resources.Load("Json/ranklist");
			JSONNode json = JSONNode.Parse(txtobj.text);
			JSONNode data = json["list"];
			var newItems = new MyListItemModel[data.Count];
			for (int i = 0; i < data.Count; i++)
			{
				var model = new MyListItemModel()
				{
					uid = data[i]["uid"],
					nickName = data[i]["nickName"],
					avatar = data[i]["avatar"],
					trophy = data[i]["trophy"]
				};
				newItems[i] = model;
			}
			
			//根据奖杯数排序
			for (int i = 0; i < newItems.Length; i++)
			{
				for (int j = 0; j < newItems.Length - i - 1; j++)
				{
					if (newItems[j].trophy < newItems[j + 1].trophy)
					{
						var t = new MyListItemModel();
						t = newItems[j];
						newItems[j] = newItems[j + 1];
						newItems[j + 1] = t;
					}
				}
			}
			
			OnDataRetrieved(newItems);
		}

		void OnDataRetrieved(MyListItemModel[] newItems)
		{
			Data.InsertItemsAtEnd(newItems);
			Data.ResetItems(newItems);
		}
	}
	
	public class MyListItemModel
	{
		public string uid; //玩家id
		public string nickName; //玩家昵称
		public int avatar; //玩家头像
		public int trophy; //玩家奖杯数
	}
	
	public class MyListItemViewsHolder : BaseItemViewsHolder
	{
		public Text uid; 
		public Text nickName; 
		public Image avatar; 
		public Text trophy;
		public Image cupImage;
		public Image levelImage;
		public Text rankText;
		public Image backgroundImage;
		public Image rankImage;

		public override void CollectViews()
		{
			base.CollectViews();
			
			//获取item下的组件
			root.GetComponentAtPath("uid", out uid);
			root.GetComponentAtPath("nickName", out nickName);
			root.GetComponentAtPath("avatar", out avatar);
			root.GetComponentAtPath("CupPanel/trophy", out trophy);
			root.GetComponentAtPath("CupPanel/cupImage", out cupImage);
			root.GetComponentAtPath("levelImage", out levelImage);
			root.GetComponentAtPath("rankText", out rankText);
			root.GetComponentAtPath("BackgroundImage", out backgroundImage);
			root.GetComponentAtPath("rankImage", out rankImage);
		}
	}
}
