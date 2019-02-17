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
        tog_hasBuy.onValueChanged.AddListener(Toggleclick);
        btn_Details.onClick.AddListener(btnDetailsClick);
        btn_Random.onClick.AddListener(btnRandomClick);
        btn_Keep.onClick.AddListener(btnKeepClick);
    }

    public void SetItemData(RichDataEntry data, int index)
    {
        m_richDataEntry = data;
        m_index = index;
        txt_Issue.text = "第" + data.m_Issue.ToString() + "期";
        txt_Data.text = data.m_Date.ToString() + "开奖";
        if (data.m_KeepNumbers == null)
            txt_KeepNunbers.text = "";
        else
            txt_KeepNunbers.text = UIController.Instance.IntConvertString(data.m_KeepNumbers, ",");

        if (data.m_LotteryNumbers == null)
            txt_LotteryNumbers.text = "";
        else
            txt_LotteryNumbers.text = UIController.Instance.IntConvertString(data.m_LotteryNumbers, ",");

        if (data.m_RandNumbers == null)
            txt_RandNumbers.text = "";
        else
            txt_RandNumbers.text = UIController.Instance.IntConvertString(data.m_RandNumbers, ",");

        tog_hasBuy.isOn = data.m_hasBuy;
        if (tog_hasBuy.isOn)
        {
            if (!data.m_hasResult)
            {
                txt_HitLevel_Rand.text = "未开奖";
                txt_HitLevel_Keep.text = "未开奖";
            }
            if (data.m_HitLevel_Rand == -1)
                txt_HitLevel_Rand.text = "未中奖";
            else
                txt_HitLevel_Rand.text = data.m_HitLevel_Rand + "奖";

            if (data.m_HitLevel_Keep == -1)
                txt_HitLevel_Keep.text = "未中奖";
            else
                txt_HitLevel_Keep.text = data.m_HitLevel_Keep + "奖";
        }
        else
        {
            txt_HitLevel_Rand.text = "未购买";
            txt_HitLevel_Keep.text = "未购买";
        }
    }

    private void Toggleclick(bool arg)
    {
        Debug.Log("this is Toggle click");
        Debug.Log(arg);
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
