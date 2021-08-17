using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.CustomParams;
using Com.TheFallenGames.OSA.DataHelpers;
using SimpleJSON;

/*
 * OSA插件类，实现排行榜显示
 */
namespace Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists
{
	public class BasicListAdapter : OSA<BaseParamsWithPrefab, MyListItemViewsHolder>
	{
		public SimpleDataHelper<RankData> Data { get; private set; }
		public Text timeText; //倒计时文字
		public JsonReader jsonRead; //json读取类
		public RankItemController rankItemController; //排行榜item控制
		
		private int totalTime; //距离赛季刷新的时间，单位秒
		private int day; //天数
		private int hour; //小时
		private int minute; //分钟数
		private int second; //秒数
		StringBuilder stringBuilder; //生成倒计时的string

		#region OSA implementation
		protected override void Awake()
		{
			Data = new SimpleDataHelper<RankData>(this);

			totalTime = JsonReader.ReadCountdown();
			
			base.Awake();
			StartCoroutine(updateCountdown());
		}
		
		//通过协程实现倒计时刷新
		IEnumerator updateCountdown()
		{
			while (totalTime > 0)
			{
				RetrieveDataAndUpdate();
				ComputeTime(totalTime);
				yield return new WaitForSeconds(1);
				totalTime--;
			}
		}
		
		//计算时间，秒数转为天时分秒
		private void ComputeTime(int timer)
		{
			day = timer / (60 * 60 * 24);
			hour = timer / (60 * 60) - day * 24;
			minute = timer / 60 - hour * 60 - day * 24 * 60;
			second = timer - minute * 60 - hour * 60 * 60 - day * 24 * 60 * 60;

			stringBuilder = new StringBuilder();
			stringBuilder.Append("Ends in:");
			stringBuilder.Append(day);
			stringBuilder.Append("d ");
			stringBuilder.Append(hour);
			stringBuilder.Append("h ");
			stringBuilder.Append(minute);
			stringBuilder.Append("m ");
			stringBuilder.Append(second);
			stringBuilder.Append("s");
			timeText.text = stringBuilder.ToString();
			stringBuilder.Clear();
		}
		
		protected override MyListItemViewsHolder CreateViewsHolder(int itemIndex)
		{
			var instance = new MyListItemViewsHolder();
			
			instance.Init(_Params.ItemPrefab, _Params.Content, itemIndex);
			
			return instance;
		}
		
		protected override void UpdateViewsHolder(MyListItemViewsHolder newOrRecycled)
		{
			rankItemController.InitializeRankItemInfo(Data, newOrRecycled);
		}

		
		#endregion
		
		#region data manipulation
		public void AddItemsAt(int index, IList<RankData> items)
		{
			Data.InsertItems(index, items);
		}

		public void RemoveItemsFrom(int index, int count)
		{
			Data.RemoveItems(index, count);
		}

		public void SetItems(IList<RankData> items)
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
			yield return new WaitForSeconds(.5f);

			OnDataRetrieved(JsonReader.ReadRankData());
		}

		void OnDataRetrieved(RankData[] newItems)
		{
			Data.InsertItemsAtEnd(newItems);
			Data.ResetItems(newItems);
		}
	}

}
