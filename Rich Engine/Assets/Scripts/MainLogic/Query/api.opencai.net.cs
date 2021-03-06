﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using OCAPIDemo;
using LitJson;
using UnityEngine;

namespace OCAPIDemo
{
    public class api
    {
        public static string HttpGet(string url, Encoding enc)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 10000;//设置10秒超时
            request.Proxy = null;
            request.Method = "GET";
            request.ContentType = "application/x-www-from-urlencoded";
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);
            StringBuilder sb = new StringBuilder();
            sb.Append(reader.ReadToEnd());
            reader.Close();
            reader.Dispose();
            response.Close();
            return sb.ToString();
        }
    }
}


public class Query_To_OpenCai :  ILotteryResultQuery
{


    public static void Toggle()
    {

    }

    static Query_To_OpenCai()
    {
        Query_To_OpenCai query = new Query_To_OpenCai();
        LotteryQueryFactory.Regist(query.GetApiType(), query);
    }

    private Query_To_OpenCai()
    {
        m_resultEntryMap = new Dictionary<ulong, JsonData>();
    }

    public string GetApiType()
    {
        return "http://www.opencai.net/";
    }

    public ILotteryResultQuery NewQuery()
    {
        return new Query_To_OpenCai();
    }


    Dictionary<ulong, JsonData> m_resultEntryMap;
    ulong m_lastedIssue;
    string m_curType;
    public bool GetData(string lotteryType)
    {
        try
        {
            m_curType = lotteryType;
            string url = RichEngine.Instance.GetLotteryQueryLink(lotteryType, GetApiType());

            string html = api.HttpGet(url, Encoding.UTF8);

            //Log
            Debug.Log("数据：" + html);

            if (!html.Substring(0, 5).Equals("{\"row", StringComparison.OrdinalIgnoreCase))
                throw new Exception(html);



            JsonData json = JsonMapper.ToObject(html);
            m_resultEntryMap.Clear();
            m_lastedIssue = 0;
            foreach (JsonData row in json["data"])
            {

                ulong issue = ulong.Parse(row["expect"].ToString());

                m_resultEntryMap.Add(issue, row);

                m_lastedIssue = Math.Max(m_lastedIssue, issue);
            }

            return true; 
        }
        catch(Exception ex)
        {
            //获取信息出错
            Debug.LogWarning(GetApiType() + "请求 " + lotteryType + " 数据出错！");
            return false;
        }

    }

    public void QueryLotteryEntry(RichDataEntry entry)
    {

        JsonData result;
        if(!m_resultEntryMap.TryGetValue(entry.m_Issue,out result))
        {
            if(m_lastedIssue > entry.m_Issue)
            {
                entry.m_isExpired = true;
            }
            return;
        }


        string[] strArr = result["opencode"].ToString().Replace('+', ',').Split(',');
        entry.m_LotteryNumbers = new int[strArr.Length];
        for (int i = 0; i < strArr.Length; i++)
        {
            entry.m_LotteryNumbers[i] = int.Parse(strArr[i]);
        }

        //entry.m_Date = JsonMapper.ToObject<DateTime>(result["opentime"].ToJson());
        entry.m_Date = DateTime.Parse(result["opentime"].ToString());


        ILotteryRule rule = LotteryRuleFactory.GetLotteryRule(m_curType);
        rule.Compare(entry);

        entry.m_hasResult = true;

    }

    public RichDataEntry GetLatestIssueInfo()
    {
        RichDataEntry entry = new RichDataEntry();

        JsonData lastedResult = m_resultEntryMap[m_lastedIssue];

        entry.m_Issue = ulong.Parse(lastedResult["expect"].ToString());

        string[] strArr = lastedResult["opencode"].ToString().Replace('+', ',').Split(',');
        entry.m_LotteryNumbers = new int[strArr.Length];
        for(int i = 0; i<strArr.Length;i++)
        {
            entry.m_LotteryNumbers[i] = int.Parse(strArr[i]);
        }

        //entry.m_Date = JsonMapper.ToObject<DateTime>(lastedResult["opentime"].ToJson());
        entry.m_Date = DateTime.Parse(lastedResult["opentime"].ToString());
        entry.m_hasResult = true;

        return entry;
    }


    public ulong GetLastestIssueNo()
    {
        return m_lastedIssue;
    }
}