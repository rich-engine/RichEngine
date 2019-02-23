using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class KeepItemUI : MonoBehaviour
{
	public int m_index = -1;
    int[] mKeepNumbers;

    void Start()
	{
        GetComponent<InputField>().onValueChanged.AddListener(InputClick);
    }

    public void SetItemData(int index,int[] num)
    {
        m_index = index;
        mKeepNumbers = num;
    }

    public void InputClick(string arg)
    {
        mKeepNumbers[m_index] = int.Parse(arg);
    }
}
