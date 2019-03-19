using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class UIController : MonoBehaviour
{
    static UIController mUIController;
    public GameObject mCanvas;
    public Text mTextKeep;
    public string mLottryType = "";

    static public UIController Instance
    {
        get { return mUIController; }
    }

    void Start()
    {
        mUIController = this;
        mLottryType = getLottryType(0);
        mCanvas = GameObject.Find("Canvas");
        mTextKeep = GameObject.Find("txt_Keep").GetComponent<Text>();
    }

    public GameObject CreateObject(string prefabName, GameObject parentObj)
    {
        try
        {
            UnityEngine.Object o = Resources.Load(prefabName, typeof(GameObject));
            GameObject mObject = GameObject.Instantiate(o) as GameObject;
            if (mObject != null)
            {
                mObject.transform.SetParent(parentObj.transform);
                mObject.transform.localPosition = new Vector3(0, 0, 0);
                mObject.transform.localScale = new Vector3(1, 1, 1);
                return mObject;
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Load prefab fail >" + prefabName + ", " + e.ToString());
        }
        return null;
    }

    public string IntConvertString(int[] num,string strMark = ",")
    {
        if (num == null)
            return "";
        string[] s_RandNumbers = Array.ConvertAll<int, string>(num, delegate (int input) { return input.ToString(); });
        for (int i = 0; i < s_RandNumbers.Length; i++)
        {
            if (s_RandNumbers[i] != "" && int.Parse(s_RandNumbers[i]) < 10)
            {
                s_RandNumbers[i] = "0" + s_RandNumbers[i];
            }
        }
        string str = string.Join(strMark, s_RandNumbers);
        return str;
    }

    public string getLottryType(int index)
    {
        string key = "";
        int _index = -1;
        foreach (var lottryType in RichEngine.Instance.m_setting.m_LottryTypes)
        {
            _index++;
            if (index == _index)
            {
                key = lottryType.Key;
            }
        }
        return key;
    }

    public bool IsRepeat(int[] array)
    {
        Hashtable ht = new Hashtable();
        for (int i = 0; i < array.Length; i++)
        {
            if (ht.Contains(array[i]))
            {
                return true;
            }
            else
            {
                ht.Add(array[i], array[i]);
            }
        }
        return false;
    }

    public GameObject setTip(string str, float time = 2)
    {
        GameObject uiObject = CreateObject("UI/Tip", UIController.Instance.mCanvas);
        uiObject.GetComponent<Tip>().setTip(str, time);
        //返回节点已供需要自己控制关闭的需求
        return uiObject; 
    }

}

