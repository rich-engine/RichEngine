using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListItemUI : MonoBehaviour
{

	private Text Text_Name;
	private Text Text_Day;
	private Text Text_Content;
	private Text Text_Title;
	private Image Image_RedPoint;
	private Image Image_flag;

	public GameObject inbox_email;

	private int m_flag = -1;

	public int m_index = -1;
	void Awake()
	{
		Text_Name = transform.Find ("txt_No").GetComponent<Text>();
		//Text_Day = transform.Find ("Text_Day").GetComponent<Text>();
		//Text_Content = transform.Find ("Text_Content").GetComponent<Text>();
		//Text_Title = transform.Find ("Text_Title").GetComponent<Text>();
		//Image_RedPoint = transform.Find ("Image_RedPoint").GetComponent<Image>();
		//Image_flag = transform.Find ("Image_flag").GetComponent<Image>();
	}

    public void SetItemData(GameObject item, int index)
    {
        //itemData = item;
        m_index = index;


        Text_Name.text = index.ToString();

        ////		string content = line_email ["Content"].ToString ();
        //int messageID = int.Parse(line_email["MessageID"].ToString());
        //Debug.Log("messageID  " + messageID);
        //string content = LoadJson.GetLine(messageID, GameControl.instance.table_message)["Condition"].ToString();
        //Debug.Log("content  " + content);

        //Text_Content.text = content;
        //Text_Day.text = GameControl.instance.getGameTime(itemData.time);
        //Text_Title.text = line_email["Title"].ToString();



    }
}
