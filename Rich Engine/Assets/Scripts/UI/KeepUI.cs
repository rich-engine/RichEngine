using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


public class KeepUI : MonoBehaviour
{
	private Button btn_Sure;
	private Button btn_Cancel;
	private InputField input_Keep;

	void Awake()
	{
        btn_Sure = transform.Find("img_bg/btn_Sure").GetComponent<Button>();
        btn_Cancel = transform.Find("img_bg/btn_Cancel").GetComponent<Button>();
        input_Keep = transform.Find("img_bg/input_Keep").GetComponent<InputField>();

        btn_Sure.onClick.AddListener(btnSureClick);
        btn_Cancel.onClick.AddListener(btnCloseClick);
        input_Keep.onValueChanged.AddListener(btnKeepClick);
        transform.GetComponent<Button>().onClick.AddListener(btnCloseClick);
        
    }

    private void btnSureClick()
    {

    }

    public void btnKeepClick(string arg)
    {
        
    }

    public void btnCloseClick()
    {
        Destroy(gameObject);
    }
}
