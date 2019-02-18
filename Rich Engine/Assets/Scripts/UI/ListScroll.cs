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
    private int mSelectIndex = -1;
    RichArchieve mArchieve;

    private Text txt_Keep;

    void Start()
    {
        txt_Keep = GameObject.Find("txt_Keep").GetComponent<Text>();
        mCount = 0;
        mArchieve = RichEngine.Instance.m_dataCenter.GetQueryArchieve();
       
    }

    void Update()
    {
        if (mArchieve == null || mArchieve.m_RecordsList == null || mArchieve.m_RecordsList.Count <= 0)
        {
            return;
        }

        if (mArchieve.m_RecordsList[UIController.Instance.mSelectIndex] == null)
        {
            return;
        }

        if (mSelectIndex != UIController.Instance.mSelectIndex)
        {
            mSelectIndex = UIController.Instance.mSelectIndex;
            txt_Keep.text = UIController.Instance.IntConvertString(mArchieve.m_RecordsList[UIController.Instance.mSelectIndex].m_KeepNumbers);
        }

        if (mCount < mArchieve.m_RecordsList[mSelectIndex].m_RichList.Count)
        {
            //根据item数量改变滚动区域的大小
            int count = mArchieve.m_RecordsList[mSelectIndex].m_RichList.Count;
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200 * count);
            for (int i = count - 1; i >= 0; i--)
            {
                if (count - i > 10 || count - i < 0)
                {
                    break;
                }
                items[count - i - 1].SetActive(true);
                items[count - i - 1].GetComponent<ListItemUI>().SetItemData(mArchieve.m_RecordsList[mSelectIndex].m_RichList[i], i, mArchieve.m_RecordsList[mSelectIndex].m_LotteryType);
            }

            mCount = count;
        }
        else
        {
            items[mCount].SetActive(false);
            mCount--;
        }

        if (mArchieve.m_RecordsList[mSelectIndex].m_RichList.Count > 10)
        {
            if (items[0].transform.position.y <= 1660)//从上往下滑
            {
                int index = items[0].GetComponent<ListItemUI>().m_index + 1;//首先判断是否为第一个元素，是的话就表示显示完了，不需要换位置了
                if (index >= mArchieve.m_RecordsList[mSelectIndex].m_RichList.Count)
                {
                    return;
                }


                Vector3 pos = items[0].transform.localPosition;//获得还没有移动位置之前第一个元素的位置

                items.Insert(0, items[9]);//将最后的元素移动到最前面

                items[0].transform.localPosition = new Vector3(pos.x, pos.y + 200, pos.z);//重新设置移动位置之后第一个元素的位置

                items.RemoveAt(items.Count - 1);//将之前最后元素删除

                items[0].GetComponent<ListItemUI>().SetItemData(mArchieve.m_RecordsList[mSelectIndex].m_RichList[index], index, mArchieve.m_RecordsList[mSelectIndex].m_LotteryType);
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

                items[items.Count - 1].GetComponent<ListItemUI>().SetItemData(mArchieve.m_RecordsList[mSelectIndex].m_RichList[index], index, mArchieve.m_RecordsList[mSelectIndex].m_LotteryType);
            }
        }
    }
}

