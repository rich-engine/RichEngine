using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeQueryData : MonoBehaviour {

     static FakeQueryData ms_fake;
    static public FakeQueryData Instance
    {
        get { return ms_fake; }
    }

    //数据
    [System.Serializable]
    public class FakeLotteryResult
    {
        public string type;
        public ulong lastestIssue;
        public FakeEntry[] results;
    }
    [System.Serializable]
    public class FakeEntry
    {
        public ulong Issue;
        public int[] Result;
        public string Time;
    }

    public FakeLotteryResult[] lotteries;
    // Use this for initialization
    void Start () {
        ms_fake = this;
	}
	
}
