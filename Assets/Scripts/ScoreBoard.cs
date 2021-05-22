using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private float _timer;
    private float _seconds;
    private float _minutes;
    private float _hours;

    private bool _stopWatchRunning;
    private int _finishedLevels = 0;

    [SerializeField] private TextMeshProUGUI stopWatch;
    [SerializeField] private TextMeshProUGUI levelList;
    [SerializeField] private TextMeshProUGUI levelTimeList;
    [SerializeField] private TextMeshProUGUI heartPieceList;
    [SerializeField] private TextMeshProUGUI heartPieceTimeList;

    void Start()
    {
        _timer = 0;
    }

    void Update()
    {
        if (_stopWatchRunning)
        {
            _timer += Time.deltaTime;
            _seconds = (int)(_timer % 60);
            _minutes = (int)(_timer / 60 % 60);
            _hours = (int)(_timer / 3600);

            stopWatch.text = GetCurrentStopWatchTime();
        }   
    }

    public void StartTime()
    {
        _stopWatchRunning = true;
    }

    public void StopTime()
    {
        _stopWatchRunning = false;
    }

    public void ResetTime()
    {
        _stopWatchRunning = false;
        _timer = 0;
        
        stopWatch.text = "00:00:00";
        heartPieceList.text = "";
        heartPieceTimeList.text = "";
    }

    public void FinishPuzzlePart(string puzzlePart)
    {
        heartPieceList.text = heartPieceList.text + (heartPieceList.text.Length > 0 ? "\n" : "") + puzzlePart;
        heartPieceTimeList.text = heartPieceTimeList.text + (heartPieceTimeList.text.Length > 0 ? "\n" : "") + GetCurrentStopWatchTime();
    }

    public void FinishLevel()
    {
        levelList.text = levelList.text + (levelList.text.Length > 0 ? "\n" : "") + "Level " + (++_finishedLevels);
        levelTimeList.text = levelTimeList.text + (levelTimeList.text.Length > 0 ? "\n" : "") + GetCurrentStopWatchTime();
        ResetTime();
    }

    private string GetCurrentStopWatchTime()
    {
        return _hours.ToString("00") + ":" + _minutes.ToString("00") + ":" + _seconds.ToString("00");
    }
}
