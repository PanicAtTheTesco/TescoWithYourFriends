using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public struct RecordInfo
{
    public int txt_ID;
    public string txt_Player;
    public int txt_Score;

}

public class Record_Script : MonoBehaviour
{
    public TextMeshProUGUI txt_ID;
    public TextMeshProUGUI txt_Player;
    public TextMeshProUGUI txt_Score;

    public void Init(RecordInfo info)
    {
        txt_ID.text = info.txt_ID.ToString();
        txt_Player.text = info.txt_Player;
        txt_Score.text = info.txt_Score.ToString();
    }
}
