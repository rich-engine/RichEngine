﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class ListItemUI : MonoBehaviour
{

    private Text txt_Issue;
    private Text txt_Data;
    private Text txt_KeepNunbers;
    private Text txt_LotteryNumbers;
    private Text txt_RandNumbers;
    private Text txt_HitLevel_Rand;
    private Text txt_HitLevel_Keep;
    private Button btn_Details;
    private Button btn_Keep;
    private Button btn_Random;
    private Toggle tog_hasBuy;

    public int m_index = -1;
    RichDataEntry m_richDataEntry;


    void Awake()
    {
        txt_Issue = transform.Find("txt_Issue").GetComponent<Text>();
        txt_Data = transform.Find("txt_Data").GetComponent<Text>();
        txt_KeepNunbers = transform.Find("txt_KeepNunbers").GetComponent<Text>();
        txt_LotteryNumbers = transform.Find("txt_LotteryNumbers").GetComponent<Text>();
        txt_RandNumbers = transform.Find("txt_RandNumbers").GetComponent<Text>();
        txt_HitLevel_Rand = transform.Find("txt_HitLevel_Rand").GetComponent<Text>();
        txt_HitLevel_Keep = transform.Find("txt_HitLevel_Keep").GetComponent<Text>();
        btn_Details = transform.Find("btn_Details").GetComponent<Button>();
        btn_Keep = transform.Find("btn_Keep").GetComponent<Button>();
        btn_Random = transform.Find("btn_Random").GetComponent<Button>();
        tog_hasBuy = transform.Find("tog_hasBuy").GetComponent<Toggle>();
        //tog_hasBuy.onValueChanged.AddListener(ToggleClick);
        btn_Details.onClick.AddListener(btnDetailsClick);
        btn_Random.onClick.AddListener(btnRandomClick);
        btn_Keep.onClick.AddListener(btnKeepClick);
        tog_hasBuy.enabled = false;
    }

    public void SetItemData(RichDataEntry data, int index)
    {
        m_richDataEntry = data;
        m_index = index;
        txt_Issue.text = "第" + m_richDataEntry.m_Issue.ToString() + "期";
        txt_Data.text = m_richDataEntry.m_Date.ToString() + "开奖";
        refreshKeepText();
        refreshRandomText();
        refreshBuy();

        if (m_richDataEntry.m_LotteryNumbers == null)
            txt_LotteryNumbers.text = "";
        else
            txt_LotteryNumbers.text = UIController.Instance.IntConvertString(m_richDataEntry.m_LotteryNumbers);

        if (m_richDataEntry.m_hasBuy)
        {
            if (!m_richDataEntry.m_hasResult)
            {
                txt_HitLevel_Rand.text = "未开奖";
                txt_HitLevel_Keep.text = "未开奖";
            }
            else
            {

                txt_HitLevel_Rand.text = returnChinese(m_richDataEntry.m_HitLevel_Rand);
                txt_HitLevel_Keep.text = returnChinese(m_richDataEntry.m_HitLevel_Keep);
                    
            }
        }
        else
        {
            txt_HitLevel_Rand.text = "未购买";
            txt_HitLevel_Keep.text = "未购买";
        }

        if (m_richDataEntry.m_hasResult || m_richDataEntry.m_isExpired || m_richDataEntry.m_hasBuy)
        {
            
            btn_Random.enabled = false;
            btn_Keep.enabled = false;
        }
        else
        {
            btn_Random.enabled = true;
            btn_Keep.enabled = true;
        }
        
    }

    private void ToggleClick(bool arg)
    {
        //if (arg)
            //RichEngine.Instance.m_dataCenter.SetBuy(UIController.Instance.mLottryType, m_richDataEntry.m_Issue);

    }

    public void btnDetailsClick()
    {
       GameObject ui = UIController.Instance.CreateObject("UI/BuyDetails", UIController.Instance.mCanvas);
        ui.GetComponent<BuyDetails>().SetItemData(m_richDataEntry,this);
    }

    public void btnRandomClick()
    {
        GameObject ui = UIController.Instance.CreateObject("UI/RandomUI", UIController.Instance.mCanvas);
        ui.GetComponent<RandomUI>().SetItemData(m_richDataEntry,this);
    }
    public void btnKeepClick()
    {
        GameObject ui = UIController.Instance.CreateObject("UI/KeepUI", UIController.Instance.mCanvas);
        ui.GetComponent<KeepUI>().SetItemData(this);
    }

    string returnChinese(int level)
    {
        string strLevel = "";
        switch (level)
        {
            case -1:
                strLevel = "未中奖";
                break;
            case 1:
                strLevel = "一等奖";
                break;
            case 2:
                strLevel = "二等奖";
                break;
            case 3:
                strLevel = "三等奖";
                break;
            case 4:
                strLevel = "四等奖";
                break;
            case 5:
                strLevel = "五等奖";
                break;
            case 6:
                strLevel = "六等奖";
                break;
            case 7:
                strLevel = "七等奖";
                break;
            case 8:
                strLevel = "八等奖";
                break;
            case 9:
                strLevel = "九等奖";
                break;
            default:
                break;
        }
        return strLevel;
    }

    public void refreshKeepText()
    {
        if (m_richDataEntry.m_KeepNumbers == null)
            txt_KeepNunbers.text = "";
        else
            txt_KeepNunbers.text = UIController.Instance.IntConvertString(m_richDataEntry.m_KeepNumbers);
    }

    public void refreshRandomText()
    {
        if (m_richDataEntry.m_RandNumbers == null)
            txt_RandNumbers.text = "";
        else
            txt_RandNumbers.text = UIController.Instance.IntConvertString(m_richDataEntry.m_RandNumbers);
    }

    public void refreshBuy()
    {
        tog_hasBuy.isOn = m_richDataEntry.m_hasBuy;
    }
}
