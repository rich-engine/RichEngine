using System.Collections;
using System;
using System.Linq;


public class DoubleColorBallRule : ILotteryRule
{
    int mNumCount = 7;
    int mBeforeNum = 6;


    public static void Toggle()
    {

    }

    static DoubleColorBallRule()
    {
        DoubleColorBallRule rule = new DoubleColorBallRule();
        LotteryRuleFactory.Regist(rule.GetLotteryType(), rule);
    }

    private DoubleColorBallRule()
    {
    }

    public string GetLotteryType()
    {
        return "双色球";
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

        if ((int)data.m_Date.DayOfWeek == 4)
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
            nextData.m_Issue = (ulong)_remainder * 1000 + 1;
        }
        else
            nextData.m_Issue = data.m_Issue + 1;

        return nextData;
    }

    private int Winning(int [] compareNumber,int[] lotteryNumbers)
    {
        int[] _beforeCompare = new int[mBeforeNum];
        int[] _afterCompare = new int[mNumCount - mBeforeNum];
        int[] _beforeLottery = new int[mBeforeNum];
        int[] _afterLottery = new int[mNumCount - mBeforeNum];

        for (int i=0; i < compareNumber.Length;i++)
        {
            if (i < mBeforeNum)
            {
                _beforeCompare[i] = compareNumber[i];
                _beforeLottery[i] = lotteryNumbers[i];
            }
            else
            {
                _afterCompare[i- mBeforeNum] = compareNumber[i];
                _afterLottery[i- mBeforeNum] = lotteryNumbers[i];
            }
        }
        int _beforeNum = _beforeCompare.Intersect(_afterCompare).ToArray().Length;
        int _afterNum = _afterCompare.Intersect(_afterLottery).ToArray().Length;

        if ((_beforeNum == 2 &&_afterNum == 1)||(_beforeNum == 1 && _afterNum == 1) || (_beforeNum == 0 && _afterNum == 1))
        {
            return 6;
        }
        else if ((_beforeNum == 4 && _afterNum == 0) || (_beforeNum == 3 && _afterNum == 1))
        {
            return 5;
        }
        else if ((_beforeNum == 5 && _afterNum == 0)||(_beforeNum == 4 && _afterNum == 1))
        {
            return 4;
        }
        else if (_beforeNum == 5 && _afterNum == 1)
        {
            return 3;
        }
        else if (_beforeNum == mBeforeNum && _afterNum == 0)
        {
            return 2;
        }
        else if (_beforeNum == mBeforeNum && _afterNum == mNumCount - mBeforeNum)
        {
            return 1;
        }
        return -1;
    }

    public int[] CheckNumsAvailable(int[] nums)
    {
        int _beforeMaxNum = RichEngine.Instance.m_setting.m_LottryTypes[GetLotteryType()].segments[0].max;
        int _afterMaxNum = RichEngine.Instance.m_setting.m_LottryTypes[GetLotteryType()].segments[1].max;
        int _beforeMinNum = RichEngine.Instance.m_setting.m_LottryTypes[GetLotteryType()].segments[0].min;
        int _afterMinNum = RichEngine.Instance.m_setting.m_LottryTypes[GetLotteryType()].segments[1].min;


        int[] _beforeLottery = new int[mBeforeNum];
        int[] _afterLottery = new int[mNumCount - mBeforeNum];

        for (int i = 0; i < nums.Length; i++)
        {
            if (i < mBeforeNum)
            {
                _beforeLottery[i] = nums[i];
            }
            else
            {
                _afterLottery[i - mBeforeNum] = nums[i];
            }
        }

        for (int i = 0; i < _beforeLottery.Length; i++)
        {
            if (_beforeLottery[i] < _beforeMinNum && _beforeLottery[i] > _beforeMaxNum)
                return null;
        }

        for (int i = 0; i < _afterLottery.Length; i++)
        {
            if (_afterLottery[i] < _afterMinNum && _afterLottery[i] > _afterMaxNum)
                return null;
        }

        if (UIController.Instance.IsRepeat(_beforeLottery))
            return null;

        if (UIController.Instance.IsRepeat(_afterLottery))
            return null;

        Array.Sort(_beforeLottery);
        Array.Sort(_afterLottery);

        int[] sortNum = _beforeLottery.Concat(_afterLottery).ToArray();

        return sortNum;
    }
}
