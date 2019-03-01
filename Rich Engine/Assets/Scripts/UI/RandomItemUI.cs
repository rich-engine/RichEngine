using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class RandomItemUI : MonoBehaviour
{

	private Text txt_LotteryType;
	private Button btn_LotteryType;
	private Image img_Checkmark;
	private string mLottryType;
	public int m_index = -1;
    RandomUI mRandomUI;

    void Awake()
	{
        txt_LotteryType = transform.Find ("txt_LotteryType").GetComponent<Text>();
        btn_LotteryType = GetComponent<Button>();
        img_Checkmark = transform.Find("Checkmark").GetComponent<Image>();
        btn_LotteryType.onClick.AddListener(btnRandomClick);

    }

    public void SetItemData(int index,string type,string currType, RandomUI _randomUI)
    {
        m_index = index;
        mLottryType = type; 
        txt_LotteryType.text = type;
        mRandomUI = _randomUI;
        if (mLottryType == currType)
            setMarkVisible(true);


    }

    public void btnRandomClick()
    {
        setMarkVisible(true);
        mRandomUI.setRandomText(mLottryType,m_index);
    }

    public void setMarkVisible(bool isVisible)
    {
        img_Checkmark.gameObject.SetActive(isVisible);
    }
}
