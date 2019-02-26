using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
public class fakeQueryApi : ILotteryResultQuery {

    public static void Toggle()
    {

    }

    static fakeQueryApi()
    {
        fakeQueryApi query = new fakeQueryApi();
        LotteryQueryFactory.Regist(query.GetApiType(), query);
    }

    private fakeQueryApi()
    {
        m_resultEntryMap = new Dictionary<ulong, JsonData>();
    }

    public string GetApiType()
    {
        return "Faker";
    }

    public ILotteryResultQuery NewQuery()
    {
        return new fakeQueryApi();
    }


    Dictionary<ulong, JsonData> m_resultEntryMap;
    ulong m_lastedIssue;

    public bool GetData(string lotteryType)
    {
        m_resultEntryMap.Clear();
        m_lastedIssue = 0;
        foreach (var lottery in FakeQueryData.Instance.lotteries)
        {
            if(lottery.type == lotteryType)
            {
                foreach(var rst in lottery.results)
                {
                    JsonData json = JsonMapper.ToObject(rst);

                    ulong issue = ulong.Parse(json["expect"].ToString());

                    m_resultEntryMap.Add(issue, json);

                    m_lastedIssue = Math.Max(m_lastedIssue, issue);
                }

                return true;
            }
        }
        return false;
    }

    public void QueryLotteryEntry(RichDataEntry entry)
    {
        JsonData result;
        if (!m_resultEntryMap.TryGetValue(entry.m_Issue, out result))
        {
            if (m_lastedIssue > entry.m_Issue)
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

        entry.m_Date = JsonMapper.ToObject<DateTime>(result["opentime"].ToJson());
        entry.m_hasResult = true;
    }

    public RichDataEntry GetLatestIssueInfo()
    {
        RichDataEntry entry = new RichDataEntry();

        JsonData lastedResult = m_resultEntryMap[m_lastedIssue];

        entry.m_Issue = ulong.Parse(lastedResult["expect"].ToString());

        string[] strArr = lastedResult["opencode"].ToString().Replace('+', ',').Split(',');
        entry.m_LotteryNumbers = new int[strArr.Length];
        for (int i = 0; i < strArr.Length; i++)
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
