using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Text;


public class RichArchieve
{
    public string m_AppVersion;
    public DateTime m_CreateTime;

    public List<RichLotteryRecord> m_RecordsList;


}


public class RichLotteryRecord
{
    public string m_LotteryType;  //彩票类型

    public int[] m_KeepNumbers; //守号号码

    public List<RichDataEntry> m_RichList;  //购买纪录列表


    public RichLotteryRecord()
    {

    }
}


//数据结构 单期数据
public class RichDataEntry {


    public ulong m_Issue;  //期号

    public DateTime m_Date;    //开奖日期

    public string m_RandAlgothm;//随机算法

    public int[] m_RandNumbers; //推荐号码

    public int[] m_KeepNumbers; //守号

    public int[] m_LotteryNumbers; // 中奖号码

    public bool m_hasBuy = false;   //是否购买

    public int m_HitLevel_Rand;  //中几等奖    -1 不中奖

    public int m_HitLevel_Keep;  //中几等奖    -1 不中奖

    public bool m_hasResult = false;    //是否开奖

    public bool m_isExpired = false;    //是否超出查询



    public RichDataEntry()
    {

    }

    //Json 处理 

    //public RichDataEntry(JsonData data)
    //{
    //    m_Issue = (string)data["m_Issue"];
    //    m_Date = DateTime.Parse((string)data["m_Date"]);

    //    m_RandNumbers = JsonHelper.String2Array<int>((string)data["m_RandNumbers"],JsonHelper.Str2Int);
    //    m_LotteryNumbers = JsonHelper.String2Array<int>((string)data["m_LotteryNumbers"], JsonHelper.Str2Int);

    //    m_hasBuy = (bool)data["m_hasBuy"];
    //    m_HitLevel = (int)data["m_HitLevel"];
    //    m_hasResult = (bool)data["m_hasResult"];
    //}


    //public void ToJson(JsonWriter writer)
    //{
    //    writer.WriteObjectStart();

    //    writer.WritePropertyName("m_Issue");
    //    writer.Write(m_Issue);

    //    writer.WritePropertyName("m_Date");
    //    writer.Write(m_Date.ToString());

    //    writer.WritePropertyName("m_RandNumbers");
    //    writer.Write(JsonHelper.Array2String<int>(m_RandNumbers));

    //    writer.WritePropertyName("m_LotteryNumbers");
    //    writer.Write(JsonHelper.Array2String<int>(m_LotteryNumbers));

    //    writer.WritePropertyName("m_hasBuy");
    //    writer.Write(m_hasBuy);

    //    writer.WritePropertyName("m_HitLevel");
    //    writer.Write(m_HitLevel);

    //    writer.WritePropertyName("m_hasResult");
    //    writer.Write(m_hasResult);

    //    writer.WriteObjectEnd();
    //}

}


//负责 存储读取  持有管理 功能 
public class RichDataManager
{
    RichArchieve m_archieve;

    string m_filePath;

    public RichDataManager()
    {
        m_filePath = Application.persistentDataPath + RichEngine.Instance.m_setting.DataSaveFileName;
    }

    //加载数据
    public void LoadData()
    {
        if (!File.Exists(m_filePath))
        {
            //无初始文件
            //创建新的
            CreateNewArchieve();
            return;
        }


        StreamReader json = File.OpenText(m_filePath);
        string input = json.ReadToEnd();
        JsonData jd = JsonMapper.ToObject(input);

        // 判断 Version
        if ((string)jd["m_AppVersion"] != RichEngine.Instance.m_setting.VERSION)
        {
            //警告有错 以及 错误处理

            Debug.LogWarning("Version 不同！加载失败！");


            return;
        }


        m_archieve = JsonMapper.ToObject<RichArchieve>(input);

    }
    void CreateNewArchieve()
    {
        m_archieve = new RichArchieve();
        m_archieve.m_AppVersion = RichEngine.Instance.m_setting.VERSION;
        m_archieve.m_CreateTime = DateTime.Now;
        m_archieve.m_RecordsList = new List<RichLotteryRecord>();
    }

    //保存数据
    public void SaveData()
    {     
        if (File.Exists(m_filePath))
            File.Delete(m_filePath);

        StreamWriter file = new StreamWriter(m_filePath, true, Encoding.UTF8);

        file.Write(JsonMapper.ToJson(m_archieve));

        file.Close();
    }


    //UI 数据查询接口
    //获取档案数据结构
    public RichArchieve GetQueryArchieve()
    {
        return m_archieve;
    }
    
    //创建新的彩票记录
    public void CreateNewRecord(string lotteryType)
    {
        bool isExist = false;
        
        foreach(var rec in m_archieve.m_RecordsList)
        {
            if(rec.m_LotteryType == lotteryType)
            {
                isExist = true;
                break;
            }
        }

        if(isExist)
        {
            //已经存在  报错

            return;
        }

        //创建新的

        RichLotteryRecord record = new RichLotteryRecord();
        record.m_LotteryType = lotteryType;
        //几个数据
        record.m_KeepNumbers = null;

        record.m_RichList = new List<RichDataEntry>();

        m_archieve.m_RecordsList.Add(record);

        //查询 
        RichEngine.Instance.Query(lotteryType);

        //保存
        SaveData();
    }

    //设置守号
    public void SetKeepNumbers(string lotteryType,int[] nums)
    {
        var rec = GetRecordOf(lotteryType);

        if(rec == null)
        {
            //不存在 报错

            return;
        }
        //
        rec.m_KeepNumbers = nums;

        //所有没有开奖 没有超期 还没有买的 刷新守号
        //Todo 可能不需要刷新  在点击购买时候填充就好
        foreach(var entry in rec.m_RichList)
        {
            if(!entry.m_isExpired && !entry.m_hasResult  && !entry.m_hasBuy)
            {
                entry.m_KeepNumbers = nums;
            }
        }


        //保存
        SaveData();
    }

    //设置购买
    public void SetBuy(string lotteryType,ulong issue)
    {
        var rec = GetRecordOf(lotteryType);

        if (rec == null)
        {
            //不存在 报错

            return;
        }

        var entry = GetEntryOf(rec, issue);
        if (entry == null)
        {
            //不存在 报错

            return;
        }

        if(entry.m_KeepNumbers == null || entry.m_RandNumbers == null)
        {
            //没有号码

            return;
        }


        entry.m_hasBuy = true;

        //保存
        SaveData();
    }


    //设置随机号
    public void SetRandNumbers(string lotteryType, ulong issue, int[] nums,string randAlgothm)
    {
        var rec = GetRecordOf(lotteryType);

        if (rec == null)
        {
            //不存在 报错

            return;
        }

        var entry = GetEntryOf(rec, issue);
        if (entry == null)
        {
            //不存在 报错

            return;
        }

        entry.m_RandNumbers = nums;
        entry.m_RandAlgothm = randAlgothm;
        //保存
        SaveData();
    }


    public RichLotteryRecord GetRecordOf(string lotteryType)
    {
        foreach (var rec in m_archieve.m_RecordsList)
        {
            if (rec.m_LotteryType == lotteryType)
            {
                return rec;
            }
        }

        return null;
    }

    public RichDataEntry GetEntryOf(RichLotteryRecord record,ulong issue)
    {
        foreach (var entry in record.m_RichList)
        {
            if (entry.m_Issue == issue)
            {
                return entry;
            }
        }

        return null;
    }
}
