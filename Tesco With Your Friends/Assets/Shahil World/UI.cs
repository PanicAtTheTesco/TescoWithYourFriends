using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;


public enum CanvasType
{
    NONE,
    PAUSE,
    LEADERBOARD
}
public class UI : MonoBehaviour
{
    public UI_Controller _uiController;
    public float AnimationTime = 0.5f;

    public CanvasGroup Pause;
    public CanvasGroup LeaderBoard;
    
    public bool canEnablePause;
    
    public bool toPause;
    public bool toShowLeaderboard;
    
    public SortedList<int, RecordInfo> records;

    private void OnEnable()
    {
        toPause = false;
        toShowLeaderboard = false;
        canEnablePause = true;
        
        Default_UI();

        _uiController = new UI_Controller();
        _uiController.Enable();
        
        _uiController.UI.Pause.performed += OnPauseEnabled;
        _uiController.UI.LeaderBoard.performed += OnLeaderboardEnabled;
    }
    private void OnDisable()
    {
        _uiController.Disable();
    }


    public void ShowCanvas(CanvasType canvasToShow)
    {
        float target = 1.0f;
        switch (canvasToShow)
        {
            case CanvasType.PAUSE:
                Pause.gameObject.SetActive(true);
                StartCoroutine(CanvasControl(Pause, target));
                break;
            case CanvasType.LEADERBOARD:
                LeaderBoard.gameObject.SetActive(true);
                StartCoroutine(CanvasControl(LeaderBoard, target));
                canEnablePause = false;

                break;
        }
    }
    public void HideCanvas(CanvasType canvasToShow)
    {
        float target = 0.0f;
        switch (canvasToShow)
        {
            case CanvasType.PAUSE:
                StartCoroutine(CanvasControl(Pause, target));
                Pause.gameObject.SetActive(false);
                break;
            case CanvasType.LEADERBOARD:
                StartCoroutine(CanvasControl(LeaderBoard, target));
                LeaderBoard.gameObject.SetActive(false);
                canEnablePause = true;
                break;
        }
    }
    
    private IEnumerator CanvasControl(CanvasGroup group, float target)
    {
        if (group != null)
        {
            float startAlpha = group.alpha;
            float t = 0.0f;

            group.interactable = target >= 1.0f;
            group.blocksRaycasts = target >= 1.0f;

            while (t < AnimationTime)
            {
                t = Mathf.Clamp(t + Time.deltaTime, 0.0f, AnimationTime);
                group.alpha = Mathf.SmoothStep(startAlpha, target, t / AnimationTime);
                yield return null;
            }
        }
    }

    
    // This code to manage the UIs
    public void Default_UI()
    {
        HideCanvas(CanvasType.PAUSE);
        HideCanvas(CanvasType.LEADERBOARD);
    }
    public void Pause_UI(bool pause)
    {
        if (!canEnablePause)
        {
            Debug.Log("You cannot pause the game now");
            return;
        }

        if (pause)
        {
            ShowCanvas(CanvasType.PAUSE);
        }
        else
        {
            HideCanvas(CanvasType.PAUSE);
        }
    }
    public void LeaderBoard_UI(bool showLeaderboard)
    {
        if (showLeaderboard)
        {
            ShowCanvas(CanvasType.LEADERBOARD);
        }
        else
        {
            HideCanvas(CanvasType.LEADERBOARD);
        }

    }
    
    
    // The code to Control the UI
    
    // Press "P" for enabling and disabling Pause UI
    private void OnPauseEnabled(InputAction.CallbackContext obj)
    {
        toPause = (toPause == true) ? false : true;
        Pause_UI(toPause);
    }
    
     /*
     Press "L" for enabling and disabling Leaderboard UI
     This function as of now is adding the records 
     just for the purpose of testing whether its working as expected. 
     the variable "records" is suggested to be updated from the script where the match ends methods are written
     
     One approach can be:
     1. Assign an ID and a name to each of the pawns/actors in their respective scripts
     2. Now when the match ends... add those values along with the scores to the RecordInfo struct
        and update the SortedList
     */
    private void OnLeaderboardEnabled(InputAction.CallbackContext obj)
    {
        if (toShowLeaderboard)
        {
            // UPDATING THE RECORDS START
            records = new SortedList<int, RecordInfo>();

            RecordInfo info1 = new RecordInfo();
            info1.txt_ID = 1; info1.txt_Player = "Player"; info1.txt_Score = 10;
            RecordInfo info2 = new RecordInfo();
            info2.txt_ID = 12; info2.txt_Player = "Enemy"; info2.txt_Score = 5;
        
            records.Add(2, info2);
            records.Add(1, info1);
            // UPDATING THE RECORDS END
            
            UpdateLeaderBoardInfo();
            toShowLeaderboard = false;
        }
        else
        {
            toShowLeaderboard = true;
        }
        
        LeaderBoard_UI(toShowLeaderboard);
    }
    public void UpdateLeaderBoardInfo()
    {
        Leaderboard_Script l_Script = LeaderBoard.GetComponent<Leaderboard_Script>();
        l_Script.arr_Record = new List<RecordInfo>();
        
        for (int i = 0; i < records.Count; i++)
        {
            RecordInfo r = records[i+1];
            l_Script.arr_Record.Add(r);
        }
        Debug.Log("Updated leaderboard");   
    }
    
}
