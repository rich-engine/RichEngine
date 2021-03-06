﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class TitleItemUI : MonoBehaviour
{

	private Text txt_LotteryType;
	private Toggle tog_LotteryType;
	private string mLottryType;

	public int m_index = -1;

    void Awake()
	{
        txt_LotteryType = transform.Find ("txt_LotteryType").GetComponent<Text>();
        tog_LotteryType = GetComponent<Toggle>();
        tog_LotteryType.onValueChanged.AddListener(ToggleClick);

    }

    public void SetItemData(int index,string type)
    {
        m_index = index;
        mLottryType = type; 
        txt_LotteryType.text = type;
        if (type == UIController.Instance.mLottryType)
            tog_LotteryType.isOn = true;
        else
            tog_LotteryType.isOn = false;

    }

    private void ToggleClick(bool arg)
    {
        if (arg)
        {
            UIController.Instance.mLottryType = mLottryType;
        }
            

    }
}
