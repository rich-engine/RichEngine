using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//临时的配置保存项   后面会改成 从 ini 读取
public class Setting : MonoBehaviour {



    public string VERSION;  //APP 版本

    public string DataSaveFileName; //数据保存路径

    //查询网址  //
    public string UseQueryAPI;

    //
    public Dictionary<string, LotteryTypeSetting> m_LottryTypes = new Dictionary<string, LotteryTypeSetting>();


    void Start()
    {
        var lts = GetComponentsInChildren<LotteryTypeSetting>();

        foreach(var set in lts)
        {
            m_LottryTypes.Add(set.LotteryType, set);
        }
    }
}




