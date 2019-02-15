using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class ListScroll : MonoBehaviour {
	public List<GameObject> items = new List<GameObject>();

	private int mCount = 0;

	private ArrayList mList = new ArrayList();
	private string m_personID = "-1";
    RichDataEntry data = new RichDataEntry();

    // Use this for initialization
    void Start () {
        mCount = 0;
        //for (int i = 0; i < 12; i++)
        //{
        //    mList.Add(i);

        //}

    }

    void Update()
    {
        if (mCount < mList.Count)
        {
            //根据item数量改变滚动区域的大小
            int count = mList.Count;
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200 * count);
            for (int i = count - 1; i >= 0; i--)
            {
                if (count - i > 10)
                {
                    break;
                }
                items[10 - count + i].SetActive(true);
                items[10 - count + i].GetComponent<ListItemUI>().SetItemData(data, i);
            }

            mCount = count;
            Debug.Log("email count  " + mCount);
        }

        if (mList.Count > 10)
        {
            if (items[0].transform.position.y >= -100)//上滑
            {
                int index = items[0].GetComponent<ListItemUI>().m_index - 1;//首先判断是否为第一封，是的话就表示显示完了，不需要换位置了
                if (index < 0)
                {
                    return;
                }

                Vector3 pos = items[0].transform.localPosition;//获得还没有移动位置之前第一个元素的位置

                items.Insert(0, items[9]);//将最后的元素移动到最前面

                items[0].transform.localPosition = new Vector3(pos.x, pos.y - 200, pos.z);//重新设置移动位置之后第一个元素的位置

                items.RemoveAt(items.Count - 1);//将之前最后元素删除

                items[0].GetComponent<ListItemUI>().SetItemData(data, index);
            }
            if (items[0].transform.position.y <= -100)//下滑
            {
                int index = items[items.Count - 1].GetComponent<ListItemUI>().m_index + 1;//首先判断是否为最后，是的话就表示显示完了，不需要换位置了
                if (index >= mList.Count)
                {
                    return;
                }

                Vector3 pos = items[items.Count - 1].transform.localPosition;//获得还没有移动位置之前最后一个元素的位置

                items.Add(items[0]);//将最前的元素移动到最后面

                items[items.Count - 1].transform.localPosition = new Vector3(pos.x, pos.y + 200, pos.z);//重新设置移动位置之后最后一个元素的位置

                items.RemoveAt(0);//将之前最前面的元素删除；

                items[items.Count - 1].GetComponent<ListItemUI>().SetItemData(data, index);
            }
        }
    }


    void DeleteItem(Transform item)
	{
        mList.Remove (item.gameObject);
		Destroy (item.gameObject);

		int count = mList.Count;
		transform.GetComponent<RectTransform> ().sizeDelta = new Vector2(0, 200*count);
	}
}

