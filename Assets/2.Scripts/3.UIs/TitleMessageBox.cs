using DefineEnums;
using UnityEngine;
using UnityEngine.UI;

//좌하단 character, 상단 모드 시간(시계 이미지 + 초 : 백분 초 + 파란색은 증가, 빨간색은 감소), 우상단 몬스터 종류에 따른 처치 수

public class TitleMessageBox : MonoBehaviour
{
    const float _allowable = 1.5f;  //허용오차---------
    //참조
    [SerializeField] Text _txtMessage;
    [SerializeField] RectTransform _scrollObj;

    //변수
    [SerializeField] float _gap;    //이격 거리
    [SerializeField] float _scrollSpeed = 0.2f;
    float _curTime;
    float _delayTime;

    //Type 체크
    //bool _isScroll;
    //bool _isTimer;
    bool _midCheck;

    Vector3 _startPos;
    Vector3 _midPos;
    Vector3 _endPos;
    MessageType curType;
    private void Awake()
    {
        InitOption();           //변수 초기화----------1
        _gap += _scrollObj.rect.width/2; // /2를 붙여도 될듯?
    }
    public void InitOption()    //변수 초기화----------1
    {
        Vector3 scrollRange = new Vector3(_gap, 0, 0);  //이격 거리
        _startPos = _scrollObj.position + scrollRange;   //시작
        _midPos = gameObject.transform.position;   //중간
        _endPos = _scrollObj.position - scrollRange; //도착
        CloseBox(); //Clear
    }
    private void Update()
    {
        if (gameObject.activeSelf)//필요 없을 듯?
        {
            switch (curType)    //type 체크
            {
                case MessageType.Timer:
                    _curTime += Time.deltaTime;
                    if (_curTime >= _delayTime)
                    {   //타이머 시간이 다 되면 제거/초기화(CloseBox)
                        CloseBox();
                    }
                    break;
                case MessageType.Scroll:
                    if (!_midCheck) //midPos에 도착하지 못했다면
                    {
                        if ((_scrollObj.position.x - _midPos.x) < _allowable)   //현재 위치 목표(mid) 위치 오차 체크
                            _midCheck = true;   //도착
                        else     //이동
                            _scrollObj.position = Vector3.Lerp(_scrollObj.position, _midPos, _scrollSpeed * Time.deltaTime);
                    }
                    else
                    {
                        _curTime += Time.deltaTime; //midPos 도착 후, 딜레이 체크
                        if (_curTime >= _delayTime) //현재 시간이 딜레이 시간을 넘으면
                        {   //이동
                            _scrollObj.position = Vector3.Lerp(_scrollObj.position, _endPos, _scrollSpeed * Time.deltaTime);
                        }
                        if ((_scrollObj.position.x - _endPos.x) < _allowable)   //현재 위치 목표(end) 위치 오차 체크
                            CloseBox();
                    }
                    break;
                case MessageType.Standard:  //null, default?
                    break;
            }
        }
    }
    public void OpenBox(string message, MessageType type = MessageType.Standard, float delay = 1f)//delay는 멈춰있는 시간
    {
        //_txtMessage = transform.GetChild(0).GetComponent<Text>();

        gameObject.SetActive(true);
        _txtMessage.text = message;
        //---------------과제---------------
        _delayTime = delay; //delay 넘겨주기
        curType = type; //type 넘겨주기
        _curTime = 0;   //시간 초기화 <- OpenBox가 중복 호출된 후 위험 방지(ex. CloseBox가 호출되지 않음)
        //
        if (type == MessageType.Scroll) 
        {//Scroll타입이라면
            _midCheck = false;  //중간 체크 bool 초기화
            _scrollObj.position = _startPos;    //화면 밖으로 (startPos)이동
        }
        else
            _scrollObj.position = _midPos;      //중앙(midPos)으로 이동
        //---------------------------------
    }
    public void CloseBox()
    {
        gameObject.SetActive(false);

        //---------------과제---------------
        _midCheck = false;  //초기화----------------정상 작동되면 모두 초기화된다
        _curTime = 0;
        curType = MessageType.Standard;
        //---------------------------------

    }

    private void OnGUI()    //디버그용
    {
        GUI.Box(new Rect(0, 160, 120, 40), "time : " + _curTime);
    }
}
