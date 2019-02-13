using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichEngine : MonoBehaviour {

    static RichEngine msRichEngine;

    public RichEngine Instance
    {
        get { return msRichEngine; }
    }

    void Awake()
    {
        msRichEngine = this;   
    }



    //engine 实体




    void Update()
    {
        
    }



}
