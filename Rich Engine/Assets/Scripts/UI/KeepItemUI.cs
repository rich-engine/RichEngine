using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class KeepItemUI : MonoBehaviour
{
	public int m_index = -1;
    InputField mField;
    KeepUI mKeepUI;

    void Awake()
	{
        mField = GetComponent<InputField>();
        mField.onValueChanged.AddListener(InputClick);
    }

    public void SetItemData(int index,string num, KeepUI keepUI)
    {
        m_index = index;
        mKeepUI = keepUI;
        SetItemText(num);
    }

    public void InputClick(string arg)
    {
        if(arg !="")
            mKeepUI.setIndexToKeepNumbers(arg,m_index);
    }

    public void SetItemText(string num)
    {
        mField.text = num;
    }
}
