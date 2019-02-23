using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class RandomItemUI : MonoBehaviour
{

	private Text txt_LotteryType;
	private Toggle tog_LotteryType;
	private string mLottryType;
	public int m_index = -1;
    RandomUI mRandomUI;

    void Awake()
	{
        txt_LotteryType = transform.Find ("txt_LotteryType").GetComponent<Text>();
        tog_LotteryType = GetComponent<Toggle>();
        tog_LotteryType.onValueChanged.AddListener(ToggleClick);

    }

    public void SetItemData(int index,string type, RandomUI _randomUI)
    {
        m_index = index;
        mLottryType = type; 
        txt_LotteryType.text = type;
        mRandomUI = _randomUI;
    }

    private void ToggleClick(bool arg)
    {
        //if (arg)
        //{
        //    UIController.Instance.mSelectIndex = m_index;
        //    UIController.Instance.mLottryType = mLottryType;
        //}

        mRandomUI.setRandomText(mLottryType);
    }
}
