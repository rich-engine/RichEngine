using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return "Carrion的百万随机";
    }

    public void SetRandomSeed(RichDataEntry seed)
    {
        
    }

    public int[] GetRandNum(int max, int min, int num)
    {


        return null;
    }


}
