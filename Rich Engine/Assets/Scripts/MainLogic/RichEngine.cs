using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichEngine : MonoBehaviour {

    static RichEngine msRichEngine;

    static public RichEngine Instance
    {
        get { return msRichEngine; }
    }

    void Awake()
    {
        msRichEngine = this;   
    }



    //engine 实体
    public Setting m_setting;
    public RichDataManager m_dataCenter;

    RichArchieve m_data;
    Dictionary<string,ILotteryResultQuery> m_queriesMap;


    void Start()
    {
        m_dataCenter = new RichDataManager();
        m_dataCenter.LoadData();

        m_queriesMap = new Dictionary<string, ILotteryResultQuery>();

        m_data = m_dataCenter.GetQueryArchieve();

        foreach(var record in m_data.m_RecordsList)
        {
            var query = LotteryQueryFactory.GetLotteryQuery(m_setting.UseQueryAPI);
            m_queriesMap.Add(record.m_LotteryType, query);
        }


    }



    //设置成 5分钟执行一次 project setting
    void FixedUpdate()
    {
        foreach (var record in m_data.m_RecordsList)
        {
            ILotteryResultQuery query = m_queriesMap[record.m_LotteryType];
            bool bGettOK = query.GetData(record.m_LotteryType);

            if (!bGettOK) continue;
            //执行比较现有数据
            CompareLottery(query, record);
        }

        m_dataCenter.SaveData();
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





    //临时做法  根据lotteryType 获取 lotteryLink

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
    
}
