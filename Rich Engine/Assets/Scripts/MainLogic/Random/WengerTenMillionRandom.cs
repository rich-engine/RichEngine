using System;
using System.Collections;
using System.Collections.Generic;

public class WengerTenMillionRandom : IRandom
{


    public static void Toggle()
    {

    }

    static WengerTenMillionRandom()
    {
        WengerTenMillionRandom rand = new WengerTenMillionRandom();
        RandomFactory.Regist(rand.GetRandType(), rand);
    }

    private WengerTenMillionRandom()
    {

    }

    //获取随机方法名称
    public string GetRandType()
    {
        return "Wenger的百万随机";
    }





    public void SetRandomSeed(RichDataEntry seed)
    {

    }

    public int[] GetRandNum(int max, int min, int num)
    {
        int[] result = new int[num];
        Random ran = new Random((int)DateTime.Now.Ticks);
        for (int i = 0; i < result.Length; i++)
        {
            int randomNum = ran.Next(min, max);


            if (Array.IndexOf(result, randomNum) == -1)
            {
                result[i] = randomNum;
            }
            else
                i--;
               
        }
        
        Array.Sort(result);

        return result;
    }

}
