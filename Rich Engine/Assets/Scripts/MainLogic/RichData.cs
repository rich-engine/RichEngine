using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//数据结构 单期数据
public class RichDataEntry {

    public string m_LotteryType;  //彩票类型

    public string m_Issue;  //期号

    public System.DateTime m_Date;    //开奖日期

    public int[] m_RandNumbers; //推荐号码

    public int[] m_LotteryNumbers; // 中奖号码

    public bool m_hasBuy;   //是否购买

    public int m_HitLevel;  //中几等奖    -1 不中奖

}


//负责 存储读取  持有管理 功能 
public class RichDataManager
{

    Dictionary<string, List<RichDataEntry>> mDataMap;

    public RichDataManager()
    {

    }

    //加载数据
    void LoadData()
    {

    }

    //保存数据
    void SaveData()
    {

    }


}
