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
	private Text txt_KeepNunber;
	private Text txt_LotteryNumbers;
	private Text txt_RandNumbers;
	private Text txt_HitLevel_Rand;
	private Text txt_HitLevel_Keep;
	private Button btn_Details;
	private Toggle tog_hasBuy;


	private int m_flag = -1;

	public int m_index = -1;
	void Start()
	{
        txt_Issue = transform.Find ("txt_Issue").GetComponent<Text>();
        txt_Data = transform.Find("txt_Data").GetComponent<Text>();
        txt_KeepNunber = transform.Find("txt_KeepNunber").GetComponent<Text>();
        txt_LotteryNumbers = transform.Find("txt_LotteryNumbers").GetComponent<Text>();
        txt_RandNumbers = transform.Find("txt_RandNumbers").GetComponent<Text>();
        txt_HitLevel_Rand = transform.Find("txt_HitLevel_Rand").GetComponent<Text>();
        txt_HitLevel_Keep = transform.Find("txt_HitLevel_Keep").GetComponent<Text>();
        btn_Details = transform.Find("btn_Details").GetComponent<Button>();
        tog_hasBuy = transform.Find("tog_hasBuy").GetComponent<Toggle>();

    }

    public void SetItemData(RichDataEntry data, int index)
    {
        m_index = index;
        //txt_Issue.text = data.m_Issue.ToString();
        //txt_Data.text = data.m_Date.ToString();
        //txt_KeepNunber.text = data.m_KeepNumbers.ToString();
        //txt_LotteryNumbers.text = data.m_LotteryNumbers.ToString();
        //txt_HitLevel_Rand.text = data.m_HitLevel_Rand.ToString();
        //txt_HitLevel_Keep.text = data.m_HitLevel_Keep.ToString();
        //tog_hasBuy.isOn = data.m_hasBuy;
        //int sum = 0;
        //int num = (int)data.m_Issue;
        //while (data.m_Issue > 0)
        //{
        //    sum++;
        //    num /= 10;
        //}
    }
}
