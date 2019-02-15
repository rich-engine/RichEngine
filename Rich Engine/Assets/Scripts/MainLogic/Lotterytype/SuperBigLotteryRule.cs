using System.Collections;
using System;
using System.Linq;

public class SuperBigLotteryRule : ILotteryRule
{
    int NumCount = 7;
    int beforeNum = 5; 
    
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
        //throw new NotImplementedException();
        data.m_HitLevel_Rand = Winning(data.m_RandNumbers, data.m_LotteryNumbers);
        data.m_HitLevel_Keep = Winning(data.m_KeepNumbers, data.m_LotteryNumbers);
    }

    public RichDataEntry GetNextIssue(RichDataEntry data)
    {
        //throw new NotImplementedException();
        RichDataEntry nextData = new RichDataEntry();
        nextData.m_RandNumbers = null;
        nextData.m_KeepNumbers = data.m_KeepNumbers;
        nextData.m_LotteryNumbers = null;
        nextData.m_hasBuy = false;
        nextData.m_HitLevel_Rand = -1;
        nextData.m_HitLevel_Keep = -1;
        nextData.m_hasResult = false;

        if ((int)data.m_Date.DayOfWeek == 3)
        {
            nextData.m_Date = data.m_Date.AddDays(3);
        }
        else
        {
            nextData.m_Date = data.m_Date.AddDays(2);
        }

        if (data.m_Date.Year < nextData.m_Date.Year)
        {
            int _remainder = nextData.m_Date.Year % 100;
            nextData.m_Issue = (ulong)_remainder *1000 + 1;
        }
        else
            nextData.m_Issue = data.m_Issue + 1;

        return nextData;
    }

    private int Winning(int [] compareNumber,int[] lotteryNumbers)
    {
        int[] _beforeCompare = new int[beforeNum];
        int[] _afterCompare = new int[NumCount- beforeNum];
        int[] _beforeLottery = new int[beforeNum];
        int[] _afterLottery = new int[NumCount- beforeNum];

        for (int i=0; i < compareNumber.Length;i++)
        {
            if (i < beforeNum)
            {
                _beforeCompare[i] = compareNumber[i];
                _beforeLottery[i] = lotteryNumbers[i];
            }
            else
            {
                _afterCompare[i- beforeNum] = compareNumber[i];
                _afterLottery[i- beforeNum] = lotteryNumbers[i];
            }
        }

        int _beforeNum = _beforeCompare.Intersect(_afterCompare).ToArray().Length;
        int _afterNum = _afterCompare.Intersect(_afterLottery).ToArray().Length;

        if ((_beforeNum == 0 &&_afterNum == 2)||(_beforeNum == 1 && _afterNum == 2) || (_beforeNum == 2 && _afterNum == 1) || (_beforeNum == 3 && _afterNum == 0))
        {
            return 9;
        }
        else if ((_beforeNum == 2 && _afterNum == 2) || (_beforeNum == 3 && _afterNum == 1))
        {
            return 8;
        }
        else if (_beforeNum == 4 && _afterNum == 0)
        {
            return 7;
        }
        else if (_beforeNum == 3 && _afterNum == 2)
        {
            return 6;
        }
        else if (_beforeNum == 4 && _afterNum == 1)
        {
            return 5;
        }
        else if (_beforeNum == 4 && _afterNum == 2)
        {
            return 4;
        }
        else if (_beforeNum == beforeNum && _afterNum == 0)
        {
            return 3;
        }
        else if (_beforeNum == beforeNum && _afterNum == 1)
        {
            return 2;
        }
        else if (_beforeNum == beforeNum && _afterNum == NumCount - beforeNum)
        {
            return 1;
        }
        return -1;
    }
}
