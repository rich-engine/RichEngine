using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class TitleItemUI : MonoBehaviour
{

	private Text txt_LotteryType;

	public int m_index = -1;
    //RichDataEntry m_richDataEntry;


    void Awake()
	{
        txt_LotteryType = transform.Find ("txt_LotteryType").GetComponent<Text>();
        GetComponent<Toggle>().onValueChanged.AddListener(Toggleclick);

    }

    public void SetItemData(int index)
    {
        //m_richDataEntry = data;
        m_index = index;
        txt_LotteryType.text = index.ToString();
        
    }

    private void Toggleclick(bool arg)
    {
        Debug.Log("this is Toggle click");
        Debug.Log(arg);
    }
}
