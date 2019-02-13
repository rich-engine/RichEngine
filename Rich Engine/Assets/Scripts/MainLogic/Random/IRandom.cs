using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRandom {

    //设置随机种子
    void SetRandomSeed(RichDataEntry seed);


    //取随机数值  范围 取值个数
    int[] GetRandNum(int max, int min, int num);
     
}
