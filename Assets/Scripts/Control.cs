using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Your.Namespace.Here.UniqueStringHereToAvoidNamespaceConflicts.Lists;

/*
 * 排行榜的显示与关闭
 */
public class Control : MonoBehaviour
{
    public GameObject Mask; //空界面
    private GameObject controlText; //按钮文字
    // Start is called before the first frame update
    void Start()
    {
        Mask.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    //点击控制排行榜显示或关闭
    public void click()
    {
        controlText = GameObject.FindWithTag("controlText");
        if (controlText.GetComponent<Text>().text == "Close")
        {
            controlText.GetComponent<Text>().text = "Open";
            Mask.SetActive(true);
        }
        else
        {
            controlText.GetComponent<Text>().text = "Close";
            Mask.SetActive(false);
        }
    }
    
}
