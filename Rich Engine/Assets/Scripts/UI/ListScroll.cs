using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class ListScroll : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    private int mCount = 0;
    private string mLottryType = "";
    RichLotteryRecord mRecord;

    

    void Start()
    {
        mCount = 0;
        mRecord = RichEngine.Instance.m_dataCenter.GetRecordOf(UIController.Instance.mLottryType);
       
    }

    void Update()
    {
        

        if (mLottryType != UIController.Instance.mLottryType)
        {
            mLottryType = UIController.Instance.mLottryType;
            mRecord = RichEngine.Instance.m_dataCenter.GetRecordOf(UIController.Instance.mLottryType);
            setRecord();
            UIController.Instance.mTextKeep.text = "守号号码：" + UIController.Instance.IntConvertString(mRecord.m_KeepNumbers);
        }

        if (mCount < mRecord.m_RichList.Count)
        {
            //根据item数量改变滚动区域的大小
            int count = mRecord.m_RichList.Count;
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200 * count);
            for (int i = count - 1; i >= 0; i--)
            {
                if (count - i > 10 || count - i < 0)
                {
                    break;
                }
                items[count - i - 1].SetActive(true);
                items[count - i - 1].GetComponent<ListItemUI>().SetItemData(mRecord.m_RichList[i], i);
            }

            mCount = count;
        }
        else
        {
            items[mCount].SetActive(false);
            mCount--;
        }

        if (mRecord.m_RichList.Count > 10)
        {
            if (items[0].transform.position.y <= 1660)//从上往下滑
            {
                int index = items[0].GetComponent<ListItemUI>().m_index + 1;//首先判断是否为第一个元素，是的话就表示显示完了，不需要换位置了
                if (index >= mRecord.m_RichList.Count)
                {
                    return;
                }


                Vector3 pos = items[0].transform.localPosition;//获得还没有移动位置之前第一个元素的位置

                items.Insert(0, items[9]);//将最后的元素移动到最前面

                items[0].transform.localPosition = new Vector3(pos.x, pos.y + 200, pos.z);//重新设置移动位置之后第一个元素的位置

                items.RemoveAt(items.Count - 1);//将之前最后元素删除

                items[0].GetComponent<ListItemUI>().SetItemData(mRecord.m_RichList[index], index);
            }
            if (items[0].transform.position.y >= 1860)//从下往上滑
            {
                int index = items[items.Count - 1].GetComponent<ListItemUI>().m_index - 1;//首先判断是否为最后，是的话就表示显示完了，不需要换位置了
                if (index < 0)
                {
                    return;
                }

                Vector3 pos = items[items.Count - 1].transform.localPosition;//获得还没有移动位置之前最后一个元素的位置

                items.Add(items[0]);//将最前的元素移动到最后面

                items[items.Count - 1].transform.localPosition = new Vector3(pos.x, pos.y - 200, pos.z);//重新设置移动位置之后最后一个元素的位置

                items.RemoveAt(0);//将之前最前面的元素删除；

                items[items.Count - 1].GetComponent<ListItemUI>().SetItemData(mRecord.m_RichList[index], index);
            }
        }
    }

    void setRecord()
    {
        if (mRecord == null || mRecord.m_RichList == null || mRecord.m_RichList.Count <= 0)
        {
            items[mCount].SetActive(false);
            RichEngine.Instance.m_dataCenter.CreateNewRecord(UIController.Instance.mLottryType);
            mRecord = RichEngine.Instance.m_dataCenter.GetRecordOf(UIController.Instance.mLottryType);
            return;
        }
    }
}

