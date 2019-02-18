using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichEngine : MonoBehaviour {

    static RichEngine msRichEngine;

    static public RichEngine Instance
    {
        get { return msRichEngine; }
    }



    //engine 实体
    public Setting m_setting;
    public RichDataManager m_dataCenter;

    RichArchieve m_data;
    ILotteryResultQuery m_query;


    void Awake()
    {
        msRichEngine = this;

        m_setting.Load();

        m_dataCenter = new RichDataManager();
        m_dataCenter.LoadData();
        
        m_data = m_dataCenter.GetQueryArchieve();

        m_query = LotteryQueryFactory.GetLotteryQuery(m_setting.UseQueryAPI);

    }


    float m_timeCount = 0;
    //设置成 5分钟执行一次 project setting
    void FixedUpdate()
    {
        m_timeCount += Time.fixedDeltaTime;
        if (m_timeCount < m_setting.m_QueryInterval) return;
        m_timeCount -= m_setting.m_QueryInterval;


        foreach (var record in m_data.m_RecordsList)
        {            
            bool bGetOK = m_query.GetData(record.m_LotteryType);

            if (!bGetOK) continue;
            //执行比较现有数据
            CompareLottery(m_query, record);
        }

        m_dataCenter.SaveData();
    }

    //单条查询
    public void Query(string lotteryType)
    {
        var record = m_dataCenter.GetRecordOf(lotteryType);
        bool bGetOK = m_query.GetData(record.m_LotteryType);
        if (!bGetOK) return;
        //执行比较现有数据
        CompareLottery(m_query, record);
    }


    void CompareLottery(ILotteryResultQuery query,RichLotteryRecord record)
    {
        List<RichDataEntry> removeList = new List<RichDataEntry>();
        bool hasNew = false;
        foreach(var entry in record.m_RichList)
        {
            //没有买 
            if(!entry.m_hasBuy )
            {
                //并且不是最新一期时候 删除数据
                if(entry.m_Issue <= query.GetLastestIssueNo())
                {
                    removeList.Add(entry);
                }
                //否则记录一下 有最新
                else
                {
                    hasNew = true;
                }
                continue;
            }

            //没有超期 并且 没有结果 就查询
            if (!entry.m_isExpired && !entry.m_hasResult)
            {
                query.QueryLotteryEntry(entry);
            }

        }
        //删除需要删除的
        foreach(var rm in removeList)
        {
            record.m_RichList.Remove(rm);
        }

        //没有最新，要创建最新
        if(!hasNew)
        {
            RichDataEntry lastestEntry = query.GetLatestIssueInfo();

            var rule = LotteryRuleFactory.GetLotteryRule(record.m_LotteryType);

            var newEntry = rule.GetNextIssue(lastestEntry);

            record.m_RichList.Add(newEntry);
        }

    }


    //根据lotteryType 获取 lotteryLink
    public string GetLotteryQueryLink(string lotteryType,string queryType)
    {
        LotteryTypeSetting set = m_setting.m_LottryTypes[lotteryType];

        foreach(var link in set.queryMaps)
        {
            if(link.QueryAPI == queryType)
            {
                return link.LotteryLink;
            }
        }

        return string.Empty;
    }
    

    //随机列表

    //获取随机
    public int[] GetRandNumbers(string lotteryType,ulong issue,string randType)
    {
        RichLotteryRecord record = m_dataCenter.GetRecordOf(lotteryType);
        RichDataEntry entry = m_dataCenter.GetEntryOf(record, issue);

        //找到随机
        IRandom rand = RandomFactory.GetRandom(randType);
        rand.SetRandomSeed(entry);

        //获取规则
        int count = m_setting.m_LottryTypes[lotteryType].totalNum;
        var seg = m_setting.m_LottryTypes[lotteryType].segments;
        int[] result = new int[count];
        int pos = 0;
        foreach(var s in seg)
        {
            int[] r = rand.GetRandNum(s.max, s.min, s.count);
            r.CopyTo(result, pos);
            pos += s.count;
        }

        return result;
    }
}
