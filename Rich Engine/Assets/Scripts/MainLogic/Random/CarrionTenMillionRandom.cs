using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarrionTenMillionRandom : IRandom {


    public static void Toggle()
    {

    }

    static CarrionTenMillionRandom()
    {
        CarrionTenMillionRandom rand = new CarrionTenMillionRandom();
        RandomFactory.Regist(rand.GetRandType(), rand);
    }

    private CarrionTenMillionRandom()
    {

    }

    //获取随机方法名称
    public string GetRandType()
    {
        return "Carrion的千万随机";
    }

    //中间变量
    Byte m_seed;
    System.Random m_rnd;

    public void SetRandomSeed(RichDataEntry seed)
    {
        TimeSpan deltaT = seed.m_Date - DateTime.Now;
        double var_T = Mathf.Pow((deltaT.Seconds + 1) * (deltaT.Minutes + 1), (deltaT.Hours + 1) * deltaT.Days);

        double ms = deltaT.TotalMilliseconds * seed.m_Issue;

        var byte_T = BitConverter.GetBytes(var_T);
        var byte_ms = BitConverter.GetBytes(ms);

        byte t = 0;
        byte m = 0;

        foreach(var b in byte_T)
        {
            t ^= b;  
        }
        foreach (var b in byte_ms)
        {
            m ^= b;
        }

        m_seed = (byte)(m & t);

        m_rnd = new System.Random(m_seed);
    }

    public int[] GetRandNum(int max, int min, int num)
    {
        int[] result = new int[num];

        for(int i = 0;i <num;i++)
        {

            int randomNum;
            randomNum = m_rnd.Next(min, max);

            while (Array.IndexOf(result, randomNum) != -1)
            {
                randomNum = m_rnd.Next(min, max);
            }

            result[i] = randomNum;

        }

        Array.Sort(result);

        return result;
    }


}
