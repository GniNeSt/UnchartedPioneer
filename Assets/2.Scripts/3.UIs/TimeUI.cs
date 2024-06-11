using UnityEngine;
using UnityEngine.UI;
using DefineEnums;
public class TimeUI : MonoBehaviour
{
    ClearType _gameType;

    const float _floatingTime = 2.0f;
    float _visibleTime, _curTime;

    //참조
    GameObject _timeBG;
    [SerializeField] Text _timeText; 

    private void Awake()
    {
        _curTime = 0;
        _timeBG = gameObject.GetComponentInChildren<Image>().gameObject;
        gameObject.SetActive(false);
    }
    //private void LateUpdate()
    //{
    //    switch (_gameType)  //시간에 대한 계산, 즉 게임의 클리어 판정은 IngameManager에서 관리하도록 하자.
    //    {
    //        case ClearType.KillCount:
    //            _curTime += Time.deltaTime;
    //            break;
    //        case ClearType.Survive:
    //            _curTime -= Time.deltaTime;
    //            break;
    //        case ClearType.end:
    //            break;
    //    }


    //    string time = string.Format("{0:000}:{1:00}", _curTime, (int)((_curTime-(int)_curTime)*100));
    //    //강사님은 {0:000}부분 삼항연산자로 string 초기화 하심
    //    _timeText.text = time;


    //    //_visibleTime+=Time.deltaTime;
    //    //if (_visibleTime >= _floatingTime)
    //    //{
    //    //    _timeBG.SetActive(false);
    //    //    _visibleTime = 0;
    //    //}
    //}
    public void SetTimer(float _checkTime)
    {
        string time = string.Format("{0:000}:{1:00}", _checkTime, (int)((_checkTime - (int)_checkTime) * 100));
        _timeText.text = time;
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
