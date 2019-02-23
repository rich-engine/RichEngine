using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class RandomUI : MonoBehaviour
{
	private Text txt_RandNumbers;
	private Button btn_Sure;
	private Button btn_Cancel;
	private Button btn_Random;
    RichDataEntry m_richDataEntry;

    void Awake()
	{
        txt_RandNumbers = transform.Find("img_bg/txt_RandNumbers").GetComponent<Text>();
        btn_Sure = transform.Find("img_bg/btn_Sure").GetComponent<Button>();
        btn_Cancel = transform.Find("img_bg/btn_Cancel").GetComponent<Button>();
        btn_Random = transform.Find("img_bg/btn_Random").GetComponent<Button>();

        btn_Sure.onClick.AddListener(btnSureClick);
        btn_Cancel.onClick.AddListener(btnCloseClick);
        btn_Random.onClick.AddListener(btnRandClick);
        transform.GetComponent<Button>().onClick.AddListener(btnCloseClick);
    }

    public void SetItemData(RichDataEntry data,string type)
    {
        m_richDataEntry = data;
        if (data.m_RandNumbers == null)
            txt_RandNumbers.text = "";
        else
            txt_RandNumbers.text = UIController.Instance.IntConvertString(data.m_RandNumbers);
    }

    private void btnSureClick()
    {
        int[] dd = new int[] { 1, 2, 3, 15, 5, 6, 7 };
        RichEngine.Instance.m_dataCenter.SetRandNumbers("超级大乐透", m_richDataEntry.m_Issue, dd);
        btnCloseClick();
    }

    public void btnRandClick()
    {
        
    }

    public void btnCloseClick()
    {
        Destroy(gameObject);
    }
}
