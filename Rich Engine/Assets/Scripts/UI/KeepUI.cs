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
	private Button btn_Clear;
	private Image img_bg;
    int[] mKeepNumbers;
    int mTotalNum;

	void Awake()
	{
        btn_Sure = transform.Find("img_bg/btn_Sure").GetComponent<Button>();
        btn_Cancel = transform.Find("img_bg/btn_Cancel").GetComponent<Button>();
        btn_Clear = transform.Find("img_bg/btn_Clear").GetComponent<Button>();
        img_bg = transform.Find("img_bg").GetComponent<Image>();

        btn_Sure.onClick.AddListener(btnSureClick);
        btn_Cancel.onClick.AddListener(btnCloseClick);
        btn_Clear.onClick.AddListener(btnClearClick);
        //input_Keep.onValueChanged.AddListener(btnKeepClick);
        transform.GetComponent<Button>().onClick.AddListener(btnCloseClick);
        //Debug.Log("dd" + input_Keep.transform.position);


    }

    public void SetItemData()
    {
        mTotalNum = RichEngine.Instance.m_setting.m_LottryTypes[UIController.Instance.mLottryType].totalNum;
        mKeepNumbers = RichEngine.Instance.m_dataCenter.GetRecordOf(UIController.Instance.mLottryType).m_KeepNumbers;
        string num = "";
        int index = -1;
        int row = -1;
        foreach (var segment in RichEngine.Instance.m_setting.m_LottryTypes[UIController.Instance.mLottryType].segments)
        {
            row++;
            for (int i = 0; i < segment.count; i++)
            {
                index++;
                GameObject uiObject = UIController.Instance.CreateObject("UI/input_Keep", img_bg.gameObject);
                uiObject.transform.localPosition = new Vector3(200 + 80 * i, 300-80* row, 0);
                uiObject.name = "input_" + index;
                //Debug.Log("localPosition" + uiObject.transform.localPosition); 
                if (mKeepNumbers != null)
                    num = mKeepNumbers[index].ToString();
                uiObject.GetComponent<KeepItemUI>().SetItemData(index, num, this);
                if (i == segment.count - 1)
                {
                    string txtName = "img_bg/txt_" + row;
                    Text txt_rule = transform.Find(txtName).GetComponent<Text>();
                    txt_rule.text = "(" + segment.min + "-" + segment.max + ")";
                    txt_rule.transform.localPosition = new Vector3(300 + 80 * i, 300 - 80 * row, 0);
                }
               
            }
        }


    }

    private void btnSureClick()
    {
       if(mKeepNumbers == null)
        {
            UIController.Instance.mTextKeep.text = "守号号码：";
            RichEngine.Instance.m_dataCenter.SetKeepNumbers(UIController.Instance.mLottryType, mKeepNumbers);
            btnCloseClick();
            return;
        }
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
            UIController.Instance.setTip("输入号码规则不对", 3);
        }
            
    }

    public void btnCloseClick()
    {
        Destroy(gameObject);
    }

    public void btnClearClick()
    {
        mKeepNumbers = null;
        for (int i = 0; i < mTotalNum; i++)
        {
            string txtName = "img_bg/input_" + i;
            transform.Find(txtName).GetComponent<KeepItemUI>().SetItemText("");
        }
    }

    public void setIndexToKeepNumbers(string num,int index)
    {
        if (mKeepNumbers == null)
        {
            mKeepNumbers = new int[mTotalNum];
            mKeepNumbers[index] = int.Parse(num);
        }           
        else
            mKeepNumbers[index] = int.Parse(num);
    }
}
