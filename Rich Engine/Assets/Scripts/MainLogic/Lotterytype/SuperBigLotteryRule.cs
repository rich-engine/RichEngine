using System.Collections;
using System;


public class SuperBigLotteryRule : ILotteryRule
{

    static SuperBigLotteryRule()
    {
        SuperBigLotteryRule rule = new SuperBigLotteryRule();
        LotteryRuleFactory.Regist(rule.GetLotteryType(), rule);
    }

    private SuperBigLotteryRule()
    {
    }

    public string GetLotteryType()
    {
        return "超级大乐透";
    }


    public void Compare(RichDataEntry data)
    {
        throw new NotImplementedException();
    }

    public RichDataEntry GetNextIssue(RichDataEntry data)
    {
        throw new NotImplementedException();
    }


}
