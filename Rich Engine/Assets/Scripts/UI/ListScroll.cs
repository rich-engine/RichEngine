﻿using UnityEngine;
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
    float mItemHeight = 350;
    int mItemCount = 10;
    float mY = 0;

    

    void Start()
    {
        mCount = 0;
        mRecord = RichEngine.Instance.m_dataCenter.GetRecordOf(UIController.Instance.mLottryType);
        mY = items[0].transform.position.y;
    }

    void Update()
    {
        

        if (mLottryType != UIController.Instance.mLottryType)
        {
            mCount = 0;
            mLottryType = UIController.Instance.mLottryType;
            mRecord = RichEngine.Instance.m_dataCenter.GetRecordOf(UIController.Instance.mLottryType);
            setRecord();
            UIController.Instance.mTextKeep.text = "守号号码：" + UIController.Instance.IntConvertString(mRecord.m_KeepNumbers);
        }

        if (mRecord == null)
            return;

        if (mCount < mRecord.m_RichList.Count)
        {
            //根据item数量改变滚动区域的大小
            int count = mRecord.m_RichList.Count;
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, mItemHeight * count);
            for (int i = count - 1; i >= 0; i--)
            {
                if (count - i > 10 || count - i < 0)
                {
                    break;
                }
                
                items[count - i - 1].SetActive(true);
                items[count - i - 1].GetComponent<ListItemUI>().SetItemData(mRecord.m_RichList[i], i);
                items[count - i - 1].transform.localPosition = new Vector3(0, -175 - mItemHeight * (count - i - 1), 0);
            }

            mCount = count;
            if (mCount <= mItemCount)
            {
                for (int i = mCount; i < mItemCount; i++)
                {
                    items[i].SetActive(false);
                }

            }
        }


        //Debug.Log("dds0 " + items[0].name + items[0].transform.position);
        //Debug.Log("dds0 localPosition" + items[0].transform.localPosition);
        //Debug.Log("dds1 "+ items[1].name + items[1].transform.position);
        //Debug.Log("dds1 localPosition" + items[1].transform.localPosition);
        //Debug.Log("dds9 "+ items[9].name + items[9].transform.position);
        //Debug.Log("dds9 localPosition" + items[9].transform.localPosition);
        if (mRecord.m_RichList.Count > mItemCount)
        {
            if (items[0].transform.position.y <= mY)//从上往下滑
            {
                int index = items[0].GetComponent<ListItemUI>().m_index + 1;//首先判断是否为第一个元素，是的话就表示显示完了，不需要换位置了
                if (index >= mRecord.m_RichList.Count)
                {
                    return;
                }


                Vector3 pos = items[0].transform.localPosition;//获得还没有移动位置之前第一个元素的位置

                items.Insert(0, items[9]);//将最后的元素移动到最前面

                items[0].transform.localPosition = new Vector3(pos.x, pos.y + mItemHeight, pos.z);//重新设置移动位置之后第一个元素的位置

                items.RemoveAt(items.Count - 1);//将之前最后元素删除

                items[0].GetComponent<ListItemUI>().SetItemData(mRecord.m_RichList[index], index);
            }
            if (items[0].transform.position.y > mY+mItemHeight)//从下往上滑
            {
                int index = items[items.Count - 1].GetComponent<ListItemUI>().m_index - 1;//首先判断是否为最后，是的话就表示显示完了，不需要换位置了
                if (index < 0)
                {
                    return;
                }

                Vector3 pos = items[items.Count - 1].transform.localPosition;//获得还没有移动位置之前最后一个元素的位置

                items.Add(items[0]);//将最前的元素移动到最后面

                items[items.Count - 1].transform.localPosition = new Vector3(pos.x, pos.y - mItemHeight, pos.z);//重新设置移动位置之后最后一个元素的位置

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

