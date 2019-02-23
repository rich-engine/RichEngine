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

        //Toggle 
        WengerTenMillionRandom.Toggle();
        CarrionTenMillionRandom.Toggle();
    }

    public static void Regist(string queryType, IRandom rand)
    {
        ms_randMap.Add(queryType, rand);
    }


    public static Dictionary<string, IRandom>.KeyCollection GetRandomFuncList()
    {
        return ms_randMap.Keys;
    }


    public static IRandom GetRandom(string randType)
    {
        IRandom rand;

        if (!ms_randMap.TryGetValue(randType, out rand))
        {
            Debug.LogWarning("不存在 " + rand + " 随机方法！");

            return null;
        }

        return rand;
    }

}