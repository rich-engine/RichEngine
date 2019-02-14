using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
public class JsonHelper {


    //数组 转 字符串  用 | 分隔
    public static string Array2String<T>(T[] array)
    {
        StringBuilder sb = new StringBuilder();

        int length = array.Length;

        for(int i = 0;i<length - 1 ;i++)
        {
            sb.Append(array[i].ToString());
            sb.Append("|");
        }

        sb.Append(array[length - 1].ToString());

        return sb.ToString();
    }

    public static T[] String2Array<T>(string str,Func<string,T> trans)
    {
        string[] split = str.Split('|');

        int length = split.Length;

        T[] result = new T[length];

        for (int i = 0; i < length ; i++)
        {
            result[i] = trans(split[i]);
        }

        return result;
    }








    //

    public static int Str2Int(string str)
    {
        return int.Parse(str);
    }
}
