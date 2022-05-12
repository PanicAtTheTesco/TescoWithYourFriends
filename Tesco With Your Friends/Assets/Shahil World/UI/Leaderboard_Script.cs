using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard_Script : MonoBehaviour
{
   public UI ui;
   
   public Transform trans_Board;
   
   public Record_Script ref_Record;
   public Record_Script i_Record;
   
   public List<RecordInfo> arr_Record;
   public List<Record_Script> current_Records;

   private void OnEnable()
   {
      //ui.UpdateLeaderBoardInfo();
      
      foreach (var r in arr_Record)
      {
         i_Record = Instantiate(ref_Record, trans_Board);
         i_Record.Init(r);
         current_Records.Add(i_Record);
      }
   }

   private void OnDisable()
   {
      for (int i = 0; i < current_Records.Count; i++)
      {
         Destroy(current_Records[i].gameObject);
      }
      current_Records.Clear();
      arr_Record.Clear();
   }
}
