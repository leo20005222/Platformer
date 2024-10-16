using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject GameStopCanvas;
    public GameObject GameOverCanvas;
    private float timeLimit;
    public Text uiText;
    public float TimeLimit
    {
        get { return timeLimit; }
        set { timeLimit = Mathf.Max(10, value); } // 최소 10초로 제한
    }

    private float remainingTime;
    private bool isTimerRunning = false;

    //  타이머 시작
    public void StartTimer()
    {
        remainingTime = timeLimit;
        isTimerRunning = true;
    }

    //   프래임당 시간 업데이트
    void Update()
    {
        if (isTimerRunning && remainingTime > 0)
        {
            //  Time.deltaTime (프래임 사이의 시간)을 사용해 시간 감소
            remainingTime -= Time.deltaTime;

            // 남은시간 표시
            UpdateRemaining(remainingTime);

            // 시간이 0 이하가 되면 타이머 종료
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                isTimerRunning = false;
                TimeEnd();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale == 0)
                {
                    GameStopCanvas.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                
                    GameStopCanvas.SetActive(true);
                    Time.timeScale = 0;
                
                }
            }
        }
    }

    // 시간 종료시 호출할 메서드
    private void TimeEnd()
    {
        Debug.Log("Player Death"); // 플래이어 사망
        Debug.Log("============"); // 선택창
    }

    //  남은시간 리턴
    public float GetRemainingTime()
    {
        return remainingTime;
    }

    // 남은시간 출력
    private void UpdateRemaining(float time)
    {
        string timetext = (Mathf.Round(time * 100f) / 100f).ToString();
        Debug.Log("Remaining Time: " +timetext);
        uiText.text = timetext;
    }
}
