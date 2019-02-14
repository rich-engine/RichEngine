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


    public string m_Issue;  //期号

    public DateTime m_Date;    //开奖日期

    public int[] m_RandNumbers; //推荐号码

    public int[] m_KeepNumbers; //推荐号码

    public int[] m_LotteryNumbers; // 中奖号码

    public bool m_hasBuy;   //是否购买

    public int m_HitLevel_Rand;  //中几等奖    -1 不中奖

    public int m_HitLevel_Keep;  //中几等奖    -1 不中奖

    public bool m_hasResult;    //是否开奖



    public RichDataEntry()
    {

    }

    //Json 处理 

    public RichDataEntry(JsonData data)
    {
        m_Issue = (string)data["m_Issue"];
        m_Date = DateTime.Parse((string)data["m_Date"]);

        m_RandNumbers = JsonHelper.String2Array<int>((string)data["m_RandNumbers"],JsonHelper.Str2Int);
        m_LotteryNumbers = JsonHelper.String2Array<int>((string)data["m_LotteryNumbers"], JsonHelper.Str2Int);

        m_hasBuy = (bool)data["m_hasBuy"];
        m_HitLevel = (int)data["m_HitLevel"];
        m_hasResult = (bool)data["m_hasResult"];
    }


    public void ToJson(JsonWriter writer)
    {
        writer.WriteObjectStart();

        writer.WritePropertyName("m_Issue");
        writer.Write(m_Issue);

        writer.WritePropertyName("m_Date");
        writer.Write(m_Date.ToString());

        writer.WritePropertyName("m_RandNumbers");
        writer.Write(JsonHelper.Array2String<int>(m_RandNumbers));

        writer.WritePropertyName("m_LotteryNumbers");
        writer.Write(JsonHelper.Array2String<int>(m_LotteryNumbers));

        writer.WritePropertyName("m_hasBuy");
        writer.Write(m_hasBuy);

        writer.WritePropertyName("m_HitLevel");
        writer.Write(m_HitLevel);

        writer.WritePropertyName("m_hasResult");
        writer.Write(m_hasResult);

        writer.WriteObjectEnd();
    }

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

            return;
        }


        StreamReader json = File.OpenText(m_filePath);
        string input = json.ReadToEnd();
        JsonData jd = JsonMapper.ToObject(input);

        // 判断 Version
        if ((string)jd["m_AppVersion"] != RichEngine.Instance.m_setting.VERSION)
        { 
            //警告有错 以及 错误处理




            return;
        }


        m_archieve = JsonMapper.ToObject<RichArchieve>(input);

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
    



}
