using System;
using UnityEngine;

public class TimerSaver : MonoBehaviour
{
    private DateTime _currentDate;
    private DateTime _oldDate;
    private readonly string _pastTimePath = "pastTimePath";

    public TimeSpan LoadTime(Type type, bool usePastTime = false)
    {
        var path = type.ToString();

        _currentDate = DateTime.Now;
        if (PlayerPrefs.HasKey(path))
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString(path));
            _oldDate = DateTime.FromBinary(temp);

            return usePastTime ? _currentDate.Subtract(_oldDate) + TimeSpan.Parse(PlayerPrefs.GetString(path + _pastTimePath))
                : _currentDate.Subtract(_oldDate);
        }

        return new TimeSpan(0, 0, 0);
    }

    public void CleatTime(Type type) => PlayerPrefs.DeleteKey(type.ToString());

    public TimeSpan LoadTime(Type type)
    {
        var path = type.ToString();

        _currentDate = DateTime.Now;
        if (PlayerPrefs.HasKey(path))
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString(path));
            _oldDate = DateTime.FromBinary(temp);
            return TimeSpan.Parse(PlayerPrefs.GetString(path + _pastTimePath)) - _currentDate.Subtract(_oldDate);
        }

        return new TimeSpan(0, 0, 0);
    }

    public void SaveCurrentTime(Type type, TimeSpan timeSpan)
    {
        PlayerPrefs.SetString(type.ToString(), DateTime.Now.ToBinary().ToString());
        PlayerPrefs.SetString(type.ToString() + _pastTimePath, timeSpan.ToString());
    }
}
