using UnityEngine;
using System.Collections;
using TMPro;

public class CountdownTimer : MonoBehaviour {

    public delegate void CountDownTimerDelegate(MsgType msgType);
    public static event CountDownTimerDelegate OnCountDownTimerMessage;

    [SerializeField]
    private float totalTime = 10f; // Total time in seconds

    [SerializeField]
    private TMP_Text timerVal;

    private float currentTime;

    private void Start(){
        
    }

    public void InitAndStart(){
        currentTime = totalTime;
        StartTimer();

        timerVal.text = currentTime.ToString();

        OnCountDownTimerMessage(MsgType.ON_TIMER_START);
    }

    private void StartTimer(){
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine() {

        while (currentTime > 0f) {
            yield return new WaitForSeconds(1f);
            currentTime--;
            
            timerVal.text = currentTime.ToString();
        }

        //Debug.Log("Countdown Complete!");
        OnCountDownTimerMessage(MsgType.ON_TIMER_END);
    }
}

public enum MsgType {
    ON_TIMER_START,
    ON_TIMER_END,
    ON_ABOUT_TO_COMPLETE
}

