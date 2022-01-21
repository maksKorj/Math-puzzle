using System;
using UnityEngine;
using TMPro;

public class LifeTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerDisplay;

    private float _remainingTime = 1;
    private TimeSpan _oneSecond = new TimeSpan(0, 0, 1);
    private TimeSpan _remainingTimeToGiveLife;

    public TimeSpan RemainingTimeToGiveLife => _remainingTimeToGiveLife;
    private bool TimerIsRunning { get; set; }
    public event Action<int> OnTimerEnd;

    void Update()
    {
        if (TimerIsRunning)
        {
            if (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
            }
            else
            {
                ProcessTime();
                _remainingTime = 1;
            }
        }
    }

    private float _seconds = -1;
    private void ProcessTime()
    {
        _remainingTimeToGiveLife -= _oneSecond;
        float seconds = _remainingTimeToGiveLife.Seconds;

        if (_seconds != seconds)
        {
            UpdateTimer(_remainingTimeToGiveLife);
            _seconds = seconds;
        }

        if (_remainingTimeToGiveLife <= TimeSpan.Zero)
        {
            TimerIsRunning = false;
            OnTimerEnd?.Invoke(1);
        }
    }

    public void UpdateTimer(TimeSpan timeSpan)
    {
        if (_timerDisplay.gameObject.activeInHierarchy)
            _timerDisplay.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

    public void StartTimer(TimeSpan timeSpan)
    {
        UpdateTimer(timeSpan);
        TimerIsRunning = true;
        _remainingTimeToGiveLife = timeSpan;
    }

    public void StartTimerAfterChange(TimeSpan timeSpan)
    {
        TimerIsRunning = true;
        _remainingTimeToGiveLife = timeSpan;
        UpdateTimer(_remainingTimeToGiveLife);
    }

    public void StopTimer()
    {
        TimerIsRunning = false;
        _timerDisplay.text = "FULL";
    }
}
