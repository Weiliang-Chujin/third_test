using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/*
 * 信息弹窗的显示与关闭
 */
public class PopupWindowController : MonoBehaviour
{
    public GameObject popupWindow; //信息弹窗对象
    public Button popupShowBtn; //打开信息弹窗按钮
    public Button popupCloseBtn; //关闭信息弹窗按钮
    public Text info; //信息弹窗中个人信息容器
    public Text nickName; //排行榜上昵称文字
    public Text rankText; //排行榜上排名文字

    void Start()
    {
        popupWindow.SetActive(false);
        popupShowBtn.onClick.AddListener(ShowPopupWindow);
        popupCloseBtn.onClick.AddListener(ClosePopupWindow);
    }

    //显示信息弹窗，更新个人信息展示
    public void ShowPopupWindow()
    {
        StringBuilder stringBuilder = new StringBuilder();
        popupWindow.SetActive(true);
        
        stringBuilder.Append("User:");
        stringBuilder.Append(nickName.text);
        stringBuilder.Append("\r\nRank:");
        stringBuilder.Append(rankText.text);
        info.text = stringBuilder.ToString();
    }
    
    //关闭信息弹窗
    public void ClosePopupWindow()
    {
        popupWindow.SetActive(false);
    }
}
