using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//彩票票种设置 数据
public class LotteryTypeSetting : MonoBehaviour
{
    public string LotteryType;

    [System.Serializable]
    public struct QueryMap
    {
        public string QueryAPI;
        public string LotteryLink;
    }

    public QueryMap[] queryMaps;

    [System.Serializable]
    public struct NumberSegmentInfo
    {
        public int max;
        public int min;
        public int count;
    }
    //彩票属性
    public int totalNum;
    public NumberSegmentInfo[] segments;

}
