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

    public void SetItemData()
    {
        int totalNum = RichEngine.Instance.m_setting.m_LottryTypes[UIController.Instance.mLottryType].totalNum;
        mKeepNumbers = new int[totalNum];
        int index = -1;
        int row = -1;
        foreach (var segments in RichEngine.Instance.m_setting.m_LottryTypes[UIController.Instance.mLottryType].segments)
        {
            row++;
            for (int i = 0; i < segments.count; i++)
            {
                index++;
                GameObject uiObject = UIController.Instance.CreateObject("UI/input_Keep", img_bg.gameObject);
                uiObject.transform.localPosition = new Vector3(200 + 80 * i, 300-80* row, 0);
                //Debug.Log("localPosition" + uiObject.transform.localPosition); 
                uiObject.GetComponent<KeepItemUI>().SetItemData(index, mKeepNumbers);
            }
        }


    }

    private void btnSureClick()
    {
       
        var rule = LotteryRuleFactory.GetLotteryRule(UIController.Instance.mLottryType);
        int[] nums = rule.CheckNumsAvailable(mKeepNumbers);
        if (nums != null)
        {
            UIController.Instance.mTextKeep.text = "守号号码：" + UIController.Instance.IntConvertString(nums);
            RichEngine.Instance.m_dataCenter.SetKeepNumbers(UIController.Instance.mLottryType, nums);
            btnCloseClick();
        }           
        else
        {
            GameObject uiObject = UIController.Instance.CreateObject("UI/Tip", UIController.Instance.mCanvas);
            uiObject.GetComponent<Tip>().setTip("输入号码规则不对", 3);
        }
            
    }

    

    public void btnCloseClick()
    {
        Destroy(gameObject);
    }
}
