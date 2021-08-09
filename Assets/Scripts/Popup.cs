using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 信息弹窗的显示与关闭
 */
public class Popup : MonoBehaviour
{
    public GameObject popup;
    public GameObject item;
    public Button popupShow_Btn;
    public Button popupClose_Btn;
    
    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(false);
        popupShow_Btn.onClick.AddListener(popupShow);
        popupClose_Btn.onClick.AddListener(popupClose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //显示信息弹窗
    public void popupShow()
    {
        popup.SetActive(true);
        popup.transform.Find("info").GetComponent<Text>().text = "User:" + item.transform.Find("nickName").GetComponent<Text>().text + "\r\nRank:" 
                                                                 + item.transform.Find("rankText").GetComponent<Text>().text;
    }
    
    //关闭信息弹窗
    public void popupClose()
    {
        popup.SetActive(false);
    }
}
