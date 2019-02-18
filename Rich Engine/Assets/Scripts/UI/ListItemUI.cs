using UnityEngine;
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
    string mType;
    RichDataEntry m_richDataEntry;


    void Awake()
	{
        txt_Issue = transform.Find ("txt_Issue").GetComponent<Text>();
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
        tog_hasBuy.onValueChanged.AddListener(ToggleClick);
        btn_Details.onClick.AddListener(btnDetailsClick);
        btn_Random.onClick.AddListener(btnRandomClick);
        btn_Keep.onClick.AddListener(btnKeepClick);
    }

    public void SetItemData(RichDataEntry data, int index,string type)
    {
        m_richDataEntry = data;
        m_index = index;
        mType = type;
        txt_Issue.text = "第" + m_richDataEntry.m_Issue.ToString() + "期";
        txt_Data.text = m_richDataEntry.m_Date.ToString() + "开奖";
        if (m_richDataEntry.m_KeepNumbers == null)
            txt_KeepNunbers.text = "";
        else
            txt_KeepNunbers.text = UIController.Instance.IntConvertString(m_richDataEntry.m_KeepNumbers);

        if (m_richDataEntry.m_LotteryNumbers == null)
            txt_LotteryNumbers.text = "";
        else
            txt_LotteryNumbers.text = UIController.Instance.IntConvertString(m_richDataEntry.m_LotteryNumbers);

        if (m_richDataEntry.m_RandNumbers == null)
            txt_RandNumbers.text = "";
        else
            txt_RandNumbers.text = UIController.Instance.IntConvertString(m_richDataEntry.m_RandNumbers);

        tog_hasBuy.isOn = m_richDataEntry.m_hasBuy;

        if (m_richDataEntry.m_hasBuy)
        {
            tog_hasBuy.enabled = false;

            if (!m_richDataEntry.m_hasResult)
            {
                txt_HitLevel_Rand.text = "未开奖";
                txt_HitLevel_Keep.text = "未开奖";
            }
            if (m_richDataEntry.m_HitLevel_Rand == -1)
                txt_HitLevel_Rand.text = "未中奖";
            else
                txt_HitLevel_Rand.text = m_richDataEntry.m_HitLevel_Rand + "奖";

            if (m_richDataEntry.m_HitLevel_Keep == -1)
                txt_HitLevel_Keep.text = "未中奖";
            else
                txt_HitLevel_Keep.text = m_richDataEntry.m_HitLevel_Keep + "奖";
        }
        else
        {
            txt_HitLevel_Rand.text = "未购买";
            txt_HitLevel_Keep.text = "未购买";
            tog_hasBuy.enabled = true;
        }

        if (m_richDataEntry.m_hasResult || m_richDataEntry.m_isExpired)
        {
            tog_hasBuy.enabled = false;
            btn_Details.enabled = false;
            btn_Random.enabled = false;
            btn_Keep.enabled = false;
        }
        else
        {
            tog_hasBuy.enabled = true;
            btn_Details.enabled = true;
            btn_Random.enabled = true;
            btn_Keep.enabled = true;
        }
        
    }

    private void ToggleClick(bool arg)
    {
        if (arg)
            RichEngine.Instance.m_dataCenter.SetBuy(mType, m_richDataEntry.m_Issue);
    }

    public void btnDetailsClick()
    {
       GameObject ui = UIController.Instance.CreateObject("UI/BuyDetails", UIController.Instance.mCanvas);
        ui.GetComponent<BuyDetails>().SetItemData(m_richDataEntry);
    }

    public void btnRandomClick()
    {
        GameObject ui = UIController.Instance.CreateObject("UI/RandomUI", UIController.Instance.mCanvas);
        ui.GetComponent<RandomUI>().SetItemData(m_richDataEntry);
    }
    public void btnKeepClick()
    {
        GameObject ui = UIController.Instance.CreateObject("UI/KeepUI", UIController.Instance.mCanvas);
    }
}
