using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists;

/*
 * 排行榜的显示与关闭
 */
public class GameController : MonoBehaviour
{
    public GameObject Mask; //空界面
    public Text controlText; //按钮文字

    void Start()
    {
        Mask.SetActive(true);
    }

    //点击控制排行榜显示或关闭
    private void ControlGame()
    {
        if (controlText.text == "Close")
        {
            controlText.text = "Open";
            Mask.SetActive(true);
        }
        else
        {
            controlText.text = "Close";
            Mask.SetActive(false);
        }
    }
    
}
