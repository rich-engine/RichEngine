using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class TitleItemUI : MonoBehaviour
{

	private Text txt_LotteryType;
	private Toggle tog_LotteryType;

	public int m_index = -1;

    void Awake()
	{
        txt_LotteryType = transform.Find ("txt_LotteryType").GetComponent<Text>();
        tog_LotteryType = GetComponent<Toggle>();
        tog_LotteryType.onValueChanged.AddListener(Toggleclick);

    }

    public void SetItemData(int index,string type)
    {
        m_index = index;
        txt_LotteryType.text = type;
        if (m_index == UIController.Instance.mSelectIndex)
            tog_LotteryType.isOn = true;
        else
            tog_LotteryType.isOn = false;

    }

    private void Toggleclick(bool arg)
    {
        if (arg)
            UIController.Instance.mSelectIndex = m_index;
    }
}
