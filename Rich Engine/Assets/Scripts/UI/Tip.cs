using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Tip : MonoBehaviour {

    Image img_tip;
    Text txt_tip;
    float mDely = 0;


    void Awake()
	{
        txt_tip = transform.Find("txt_tip").GetComponent<Text>();
        img_tip = transform.Find("img_tip").GetComponent<Image>();
    }

	void Update () {
        if (mDely == 999)
            return;

        mDely -= Time.deltaTime;

        if (mDely <= 0)
            btnCloseClick();
	}

	public void setTip(string str ,float dely = 999) //默认999不关闭
	{
        mDely = dely;
        txt_tip.text = str;
		if (txt_tip.preferredWidth >= 500) {
			float line = Mathf.Round (txt_tip.preferredWidth / 500);
            txt_tip.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (500, line * 33);
			img_tip.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (520, txt_tip.preferredHeight + 20);
		} else {
			img_tip.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2(txt_tip.preferredWidth +20, txt_tip.preferredHeight +20);
		}
	}

    public void btnCloseClick()
    {
        Destroy(gameObject);
    }
}