using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichEngine : MonoBehaviour {

    static RichEngine msRichEngine;

    static public RichEngine Instance
    {
        get { return msRichEngine; }
    }

    void Awake()
    {
        msRichEngine = this;   
    }



    //engine 实体
    public Setting m_setting;
    public RichDataManager m_dataCenter;



    void Start()
    {
        m_dataCenter = new RichDataManager();
        m_dataCenter.LoadData();
    }



    //设置成 1分钟执行一次 project setting
    void FixedUpdate()
    {
        



    }


    //查询
    void QueryLottery(string lotteryType)
    {



    }


}
