using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILotteryResultQuery {
 

    //拉取数据 保存
    bool GetData(string lotteryType);

    //查寻
    void QueryLotteryEntry(RichDataEntry entry);

    //获取最近一期数据
    RichDataEntry GetLatestIssueInfo();

    //获取最近开奖一期期号
    ulong GetLastestIssueNo();

    //返回一个Query实例
    ILotteryResultQuery NewQuery();

}


public class LotteryQueryFactory
{
    static Dictionary<string, ILotteryResultQuery> ms_LotteryQueryMap;

    static LotteryQueryFactory()
    {
        ms_LotteryQueryMap = new Dictionary<string, ILotteryResultQuery>();


        //Toggle

        Query_To_OpenCai.Toggle();
        fakeQueryApi.Toggle();
    }

    public static void Regist(string queryType, ILotteryResultQuery query)
    {
        ms_LotteryQueryMap.Add(queryType, query);
    }

    public static ILotteryResultQuery GetLotteryQuery(string queryType)
    {
        ILotteryResultQuery query;

        if (!ms_LotteryQueryMap.TryGetValue(queryType, out query))
        {
            Debug.LogWarning("不存在 " + queryType + " 接口的数据源！无法做中奖验证！");

            return null;
        }

        return query.NewQuery();
    }






}