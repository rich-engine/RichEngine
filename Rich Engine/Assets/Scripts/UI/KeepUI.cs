using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class KeepUI : MonoBehaviour
{
	private Button btn_Sure;
	private Button btn_Cancel;
	private Image img_bg;
    string mType;
    int[] mKeepNumbers;

	void Awake()
	{
        btn_Sure = transform.Find("img_bg/btn_Sure").GetComponent<Button>();
        btn_Cancel = transform.Find("img_bg/btn_Cancel").GetComponent<Button>();
        img_bg = transform.Find("img_bg").GetComponent<Image>();

        btn_Sure.onClick.AddListener(btnSureClick);
        btn_Cancel.onClick.AddListener(btnCloseClick);
        //input_Keep.onValueChanged.AddListener(btnKeepClick);
        transform.GetComponent<Button>().onClick.AddListener(btnCloseClick);
        //Debug.Log("dd" + input_Keep.transform.position);


    }

    public void SetItemData(string type)
    {
        mType = type;
        int totalNum = RichEngine.Instance.m_setting.m_LottryTypes[mType].totalNum;
        mKeepNumbers = new int[totalNum];
        for (int i = 0; i < totalNum; i++)
        {
            GameObject uiObject = UIController.Instance.CreateObject("UI/input_Keep", img_bg.gameObject);
            uiObject.transform.localPosition = new Vector3(500-100 * i, 0, 0);
            uiObject.GetComponent<KeepItemUI>().SetItemData(i, mKeepNumbers);
            
        }
        
    }

    private void btnSureClick()
    {
        RichEngine.Instance.m_dataCenter.SetKeepNumbers(mType, mKeepNumbers);
        btnCloseClick();
    }

    

    public void btnCloseClick()
    {
        Destroy(gameObject);
    }
}
