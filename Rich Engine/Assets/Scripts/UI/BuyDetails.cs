﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class BuyDetails : MonoBehaviour
{
	private Text txt_LotteryType;
	private Text txt_KeepNunbers;
	private Text txt_RandNumbers;
    //private Button btn_KeepNumbers;
    //private Button btn_RandNumbers;
    private Button btn_Buy;
    ListItemUI mListItemUI;

    RichDataEntry m_richDataEntry;

    void Awake()
	{
        txt_LotteryType = transform.Find ("img_bg/txt_LotteryType").GetComponent<Text>();
        txt_KeepNunbers = transform.Find("img_bg/txt_KeepNunbers").GetComponent<Text>();
        txt_RandNumbers = transform.Find("img_bg/txt_RandNumbers").GetComponent<Text>();
        //btn_KeepNumbers = transform.Find("img_bg/btn_KeepNumbers").GetComponent<Button>();
        //btn_RandNumbers = transform.Find("img_bg/btn_RandNumbers").GetComponent<Button>();
        btn_Buy = transform.Find("img_bg/btn_Buy").GetComponent<Button>();

        //btn_KeepNumbers.onClick.AddListener(btnKeepClick);
        //btn_RandNumbers.onClick.AddListener(btnRandomClick);
        btn_Buy.onClick.AddListener(btnBuyClick);
        transform.GetComponent<Button>().onClick.AddListener(btnCloseClick);
    }

    public void SetItemData(RichDataEntry data, ListItemUI listItem)
    {
        m_richDataEntry = data;
        mListItemUI = listItem;
        if (data.m_KeepNumbers == null)
            txt_KeepNunbers.text = "";
        else
            txt_KeepNunbers.text = UIController.Instance.IntConvertString(data.m_KeepNumbers);

        if (data.m_RandNumbers == null)
            txt_RandNumbers.text = "";
        else
            txt_RandNumbers.text = UIController.Instance.IntConvertString(data.m_RandNumbers);

        txt_LotteryType.text = "请帮我购买" + UIController.Instance.mLottryType;
    }

    //public void btnRandomClick()
    //{
    //    GameObject ui = UIController.Instance.CreateObject("UI/RandomUI", UIController.Instance.mCanvas);
    //    ui.GetComponent<RandomUI>().SetItemData(m_richDataEntry);
    //}
    //public void btnKeepClick()
    //{
    //    GameObject ui = UIController.Instance.CreateObject("UI/KeepUI", UIController.Instance.mCanvas);
    //}

    public void btnBuyClick()
    {
        RichEngine.Instance.m_dataCenter.SetBuy(UIController.Instance.mLottryType, m_richDataEntry.m_Issue);
        mListItemUI.refreshBuy();
        btnCloseClick();
    }

    public void btnCloseClick()
    {
        Destroy(gameObject);
    }
}
