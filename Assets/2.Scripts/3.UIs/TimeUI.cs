using UnityEngine;
using UnityEngine.UI;
using DefineEnums;
public class TimeUI : MonoBehaviour
{
    ClearType _gameType;

    const float _floatingTime = 2.0f;
    float _visibleTime, _curTime;

    //ÂüÁ¶
    GameObject _timeBG;
    [SerializeField] Text _timeText; 

    private void Awake()
    {
        _curTime = 0;
        _timeBG = gameObject.GetComponentInChildren<Image>().gameObject;
        gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        switch (_gameType)
        {
            case ClearType.KillCount:
                _curTime += Time.deltaTime;
                break;
            case ClearType.Survive:
                _curTime -= Time.deltaTime;
                break;
            case ClearType.end:
                break;
        }
        string time = string.Format("{0:0.00}", _curTime);

        _timeText.text = time;
        //_visibleTime+=Time.deltaTime;
        //if (_visibleTime >= _floatingTime)
        //{
        //    _timeBG.SetActive(false);
        //    _visibleTime = 0;
        //}
    }
    public void InitTime(float startTime = 0f, ClearType type = ClearType.KillCount)
    {
        _curTime = startTime;
        _gameType = type;

        switch (_gameType)
        {
            case ClearType.KillCount:
                _timeText.color = Color.blue;
                break;
            case ClearType.Survive:
                _timeText.color = Color.red;
                break;
            case ClearType.end:
                _timeText.color = Color.black;
                break;
        }
    }
    public void GameSet(ClearType type = ClearType.end)
    {
        _gameType=type;
    }
}
