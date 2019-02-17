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

    static public UIController Instance
    {
        get { return mUIController; }
    }

    void Start()
    {
        mUIController = this;
        mCanvas = GameObject.Find("Canvas");
    }

    public GameObject CreateObject(string prefabName, GameObject parentObj)
    {
        try
        {
            UnityEngine.Object o = Resources.Load(prefabName, typeof(GameObject));
            GameObject mObject = GameObject.Instantiate(o) as GameObject;
            if (mObject != null)
            {
                mObject.transform.parent = parentObj.transform;
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

    public string IntConvertString(int[] num,string strMark)
    {
        string[] s_RandNumbers = Array.ConvertAll<int, string>(num, delegate (int input) { return input.ToString(); });
        string str = string.Join(strMark, s_RandNumbers);
        return str;
    }

}

