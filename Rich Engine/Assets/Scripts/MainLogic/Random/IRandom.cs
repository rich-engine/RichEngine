using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRandom {

    //获取随机方法名称
    string GetRandType();

    //设置随机种子
    void SetRandomSeed(RichDataEntry seed);


    //取随机数值  范围 取值个数
    int[] GetRandNum(int max, int min, int num);
     
}



public class RandomFactory
{
    static Dictionary<string, IRandom> ms_randMap;

    static RandomFactory()
    {
        ms_randMap = new Dictionary<string, IRandom>();
    }

    public static void Regist(string queryType, IRandom query)
    {
        ms_randMap.Add(queryType, query);
    }

    public static ILotteryResultQuery GetLotteryQuery(string queryType)
    {
        ILotteryResultQuery query;

        if (!ms_randMap.TryGetValue(queryType, out query))
        {
            Debug.LogWarning("不存在 " + queryType + " 接口的数据源！无法做中奖验证！");

            return null;
        }

        return query.NewQuery();
    }

}