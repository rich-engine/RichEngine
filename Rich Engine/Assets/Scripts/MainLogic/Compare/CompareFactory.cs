using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICompare
{
    void ProcLotteryResult(RichDataEntry data);
}



public class CompareFactory  {

    static Dictionary<string, Action<RichDataEntry>> ms_CompareFuncMap;


    static CompareFactory()
    {
        ms_CompareFuncMap = new Dictionary<string, Action<RichDataEntry>>();
    }

    public static void RegistFunc(string lotteryType, Action<RichDataEntry> func)
    {
        ms_CompareFuncMap.Add(lotteryType, func);
    }
    
    //返回false 有报错
    public static bool CompareLotteryResult(RichDataEntry data)
    {
        string lotteryType = data.m_LotteryType;

        Action<RichDataEntry> func;

        if(!ms_CompareFuncMap.TryGetValue(lotteryType,out func))
        {
            Debug.LogWarning("不存在 " + lotteryType + " 类型的对比方法！无法做中奖验证！");
            return false;
        }

        if(func != null)
        {
            func(data);
        }

        return true;
    }

}
