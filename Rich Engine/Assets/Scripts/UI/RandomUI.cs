using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class RandomUI : MonoBehaviour
{
	private Text txt_RandNumbers;
	private Button btn_Sure;
	private Button btn_Cancel;
	private Button btn_Random;
    RichDataEntry m_richDataEntry;
    public List<GameObject> items = new List<GameObject>();
    int mCount = 0;
    int[] randomNumbers;

    void Awake()
	{
        txt_RandNumbers = transform.Find("img_bg/txt_RandNumbers").GetComponent<Text>();
        btn_Sure = transform.Find("img_bg/btn_Sure").GetComponent<Button>();
        btn_Cancel = transform.Find("img_bg/btn_Cancel").GetComponent<Button>();
        btn_Random = transform.Find("img_bg/btn_Random").GetComponent<Button>();

        btn_Sure.onClick.AddListener(btnSureClick);
        btn_Cancel.onClick.AddListener(btnCloseClick);
        btn_Random.onClick.AddListener(btnRandClick);
        transform.GetComponent<Button>().onClick.AddListener(btnCloseClick);
    }

    public void SetItemData(RichDataEntry data)
    {
        m_richDataEntry = data;
        if (data.m_RandNumbers == null)
            txt_RandNumbers.text = "";
        else
            txt_RandNumbers.text = UIController.Instance.IntConvertString(data.m_RandNumbers);
    }

    private void btnSureClick()
    {
        if (randomNumbers == null)
            return;
        RichEngine.Instance.m_dataCenter.SetRandNumbers(UIController.Instance.mLottryType, m_richDataEntry.m_Issue, randomNumbers);
        btnCloseClick();
    }

    public void btnRandClick()
    {
    }

    public void btnCloseClick()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        
        if (mCount < RandomFactory.GetRandomFuncList().Count)
        {
            //根据item数量改变滚动区域的大小
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(200 * RichEngine.Instance.m_setting.m_LottryTypes.Count, 0);

            int index = -1;
            foreach (var randomType in RandomFactory.GetRandomFuncList())
            {
                index++;
                if (index >= 10)
                {
                    break;
                }

                items[index].SetActive(true);
                items[index].GetComponent<RandomItemUI>().SetItemData(index, randomType,this);
            }
            mCount = RandomFactory.GetRandomFuncList().Count;
        }

        if (RandomFactory.GetRandomFuncList().Count > 10)
        {
            if (items[0].transform.position.x >= 100)//从左向右滑
            {
                int index = items[0].GetComponent<RandomItemUI>().m_index - 1;//首先判断是否为第一个元素，是的话就表示显示完了，不需要换位置了
                if (index < 0)
                {
                    return;
                }

                Vector3 pos = items[0].transform.localPosition;//获得还没有移动位置之前第一个元素的位置

                items.Insert(0, items[9]);//将最后的元素移动到最前面

                items[0].transform.localPosition = new Vector3(pos.x - 200, pos.y, pos.z);//重新设置移动位置之后第一个元素的位置

                items.RemoveAt(items.Count - 1);//将之前最后元素删除

                items[0].GetComponent<RandomItemUI>().SetItemData(index, returnKey(index),this);
            }
            if (items[0].transform.position.x <= -100)//从右向左滑
            {
                int index = items[items.Count - 1].GetComponent<TitleItemUI>().m_index + 1;//首先判断是否为最后，是的话就表示显示完了，不需要换位置了
                if (index >= RandomFactory.GetRandomFuncList().Count)
                {
                    return;
                }


                Vector3 pos = items[items.Count - 1].transform.localPosition;//获得还没有移动位置之前最后一个元素的位置

                items.Add(items[0]);//将最前的元素移动到最后面

                items[items.Count - 1].transform.localPosition = new Vector3(pos.x + 200, pos.y, pos.z);//重新设置移动位置之后最后一个元素的位置

                items.RemoveAt(0);//将之前最前面的元素删除；

                items[items.Count - 1].GetComponent<RandomItemUI>().SetItemData(index, returnKey(index),this);
            }
        }
    }

    public string returnKey(int index)
    {
        string key = "";
        int _index = -1;
        foreach (var randomType in RandomFactory.GetRandomFuncList())
        {
            _index++;
            if (index == _index)
            {
                key = randomType;
            }
        }
        return key;
    }

    public void setRandomText(string type)
    {
        randomNumbers = RichEngine.Instance.GetRandNumbers(UIController.Instance.mLottryType, m_richDataEntry.m_Issue, type);
        txt_RandNumbers.text = UIController.Instance.IntConvertString(randomNumbers);
    }
}
