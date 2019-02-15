using System;
using System.Collections;
using System.Collections.Generic;


public class WengerTenMillionRandom : IRandom
{
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
                result[i] = ran.Next(min, max);
        }
        
        Array.Sort(result);

        return result;
    }

}
