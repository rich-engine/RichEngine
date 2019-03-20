using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class TitleListScroll : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    private int mCount = 0;
    int mItemCount = 10;


    void Start()
    {
        mCount = 0;
    }

    void Update()
    {
        if (mCount <= RichEngine.Instance.m_setting.m_LottryTypes.Count)
        {
            //根据item数量改变滚动区域的大小
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(200 * RichEngine.Instance.m_setting.m_LottryTypes.Count, 0);

            int index = -1;
            foreach (var lottryType in RichEngine.Instance.m_setting.m_LottryTypes)
            {
                index++;

                if (index >= 10)
                {
                    break;
                }

                
                items[index].SetActive(true);
                items[index].GetComponent<TitleItemUI>().SetItemData(index, lottryType.Key);
            }
            mCount = RichEngine.Instance.m_setting.m_LottryTypes.Count;
        }

        if (RichEngine.Instance.m_setting.m_LottryTypes.Count > mItemCount)
        {
            if (items[0].transform.position.x >= 100)//从左向右滑
            {
                int index = items[0].GetComponent<TitleItemUI>().m_index - 1;//首先判断是否为第一个元素，是的话就表示显示完了，不需要换位置了
                if (index < 0)
                {
                    return;
                }

                Vector3 pos = items[0].transform.localPosition;//获得还没有移动位置之前第一个元素的位置

                items.Insert(0, items[9]);//将最后的元素移动到最前面

                items[0].transform.localPosition = new Vector3(pos.x - 200, pos.y, pos.z);//重新设置移动位置之后第一个元素的位置

                items.RemoveAt(items.Count - 1);//将之前最后元素删除

                items[0].GetComponent<TitleItemUI>().SetItemData(index,UIController.Instance.getLottryType(index));
            }
            if (items[0].transform.position.x <= -100)//从右向左滑
            {
                int index = items[items.Count - 1].GetComponent<TitleItemUI>().m_index + 1;//首先判断是否为最后，是的话就表示显示完了，不需要换位置了
                if (index >= RichEngine.Instance.m_setting.m_LottryTypes.Count)
                {
                    return;
                }


                Vector3 pos = items[items.Count - 1].transform.localPosition;//获得还没有移动位置之前最后一个元素的位置

                items.Add(items[0]);//将最前的元素移动到最后面

                items[items.Count - 1].transform.localPosition = new Vector3(pos.x + 200, pos.y, pos.z);//重新设置移动位置之后最后一个元素的位置

                items.RemoveAt(0);//将之前最前面的元素删除；

                items[items.Count - 1].GetComponent<TitleItemUI>().SetItemData(index, UIController.Instance.getLottryType(index));
            }
        }
    }

}

