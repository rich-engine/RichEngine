using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface ILotteryRule
{
    string GetLotteryType();    //彩票类型
    void Compare(RichDataEntry data );   //对比是否获奖

    RichDataEntry GetNextIssue(RichDataEntry data);  //获得下一期数据  
}


public class LotteryRuleFactory  {

    static Dictionary<string, ILotteryRule> ms_LotteryRuleMap;

    static LotteryRuleFactory()
    {
        ms_LotteryRuleMap = new Dictionary<string, ILotteryRule>();


        //Toggle

        SuperBigLotteryRule.Toggle();
        DoubleColorBallRule.Toggle();
    }

    public static void Regist(string lotteryType, ILotteryRule rule)
    {
        ms_LotteryRuleMap.Add(lotteryType, rule);
    }

    public static ILotteryRule GetLotteryRule(string lotteryType)
    {
        ILotteryRule rule;

        if (!ms_LotteryRuleMap.TryGetValue(lotteryType, out rule))
        {
            Debug.LogWarning("不存在 " + lotteryType + " 类型的对比方法！无法做中奖验证！");
        }

        return rule;
    }
}
