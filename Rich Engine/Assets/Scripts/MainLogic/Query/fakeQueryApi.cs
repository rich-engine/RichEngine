using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
        
    }

    public string GetApiType()
    {
        return "Faker";
    }

    public ILotteryResultQuery NewQuery()
    {
        return new fakeQueryApi();
    }


    FakeQueryData.FakeLotteryResult m_data;
    public bool GetData(string lotteryType)
    {
        foreach(var lottery in FakeQueryData.Instance.lotteries)
        {
            if(lottery.type == lotteryType)
            {
                m_data = lottery;
                return true;
            }
        }
        return false;
    }

    public void QueryLotteryEntry(RichDataEntry entry)
    {
        if(m_data.lastestIssue < entry.m_Issue)
        {
            return;
        }


       foreach(var e in m_data.results)
       {
            if(e.Issue == entry.m_Issue)
            {
                entry.m_LotteryNumbers = e.Result;
                entry.m_Date = DateTime.Parse(e.Time);
                entry.m_hasResult = true;
                return;
            }
       }

        entry.m_isExpired = true;
        return;
    }

    public RichDataEntry GetLatestIssueInfo()
    {
        RichDataEntry entry = new RichDataEntry();

        foreach (var e in m_data.results)
        {
            if (e.Issue == m_data.lastestIssue)
            {
                entry.m_Issue = e.Issue;
                entry.m_LotteryNumbers = e.Result;
                entry.m_Date = DateTime.Parse(e.Time);
                entry.m_hasResult = true;
                break;
            }
        }

        return entry;
    }

    public ulong GetLastestIssueNo()
    {
        return m_data.lastestIssue;
    }
}
