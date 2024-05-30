using DefineEnums;
using UnityEngine;
using UnityEngine.UI;

public class TitleMessageBox : MonoBehaviour
{
    //참조
    [SerializeField] Text _txtMessage;    
    [SerializeField] Transform _scrollObj;

    //변수
    [SerializeField] float _gap;
    [SerializeField] float _scrollSpeed = 0.2f;    
    float _curTime;
    float _delayTime;

    //Type 체크
    bool _isScroll;
    bool _isTimer;
    bool _midCheck;

    Vector3 _startPos;
    Vector3 _midPos;
    Vector3 _endPos;
    private void Awake()
    {
        InitOption();
    }
    public void InitOption()
    {
        Vector3 scrollRange = new Vector3(_gap, 0, 0);
        _startPos = _scrollObj.position + scrollRange;   //시작
        _midPos = gameObject.transform.position;   //중간
        _endPos = _scrollObj.position - scrollRange; //도착
        CloseBox();
    }
    private void Update()
    {
        if (gameObject.activeSelf)//?
        {
            if (_isScroll)
            {
                if (!_midCheck)
                {
                    if ((_scrollObj.position.x - _midPos.x) < 0.5f)
                        _midCheck = true;
                    else
                        _scrollObj.position = Vector3.Lerp(_scrollObj.position, _midPos, _scrollSpeed * Time.deltaTime);
                }
                else
                {
                    _curTime += Time.deltaTime;
                    if (_curTime >= _delayTime)
                    {
                        _scrollObj.position = Vector3.Lerp(_scrollObj.position, _endPos, _scrollSpeed * Time.deltaTime);
                    }
                    if ((_scrollObj.position.x - _endPos.x) < 0.5f)
                        CloseBox();
                }
            }
            else if(_isTimer)
            {
                _curTime += Time.deltaTime;
                if (_curTime >= _delayTime)
                {
                    CloseBox();
                }
            }
        }
    }
    public void OpenBox(string message, MessageType type = MessageType.Standard, float delay = 1f)//delay는 멈춰있는 시간
    {
        //_txtMessage = transform.GetChild(0).GetComponent<Text>();

        gameObject.SetActive(true);
        _delayTime = delay;
        _txtMessage.text = message;
        //
        switch (type)
        {
            case MessageType.Standard:
                _scrollObj.position = _midPos;
                break;
            case MessageType.Timer:
                _scrollObj.position = _midPos;
                _isTimer = true;
                break;
            case MessageType.Scroll:
                _curTime = 0;
                _midCheck = false;
                _scrollObj.position = _startPos;
                _isScroll = true;
                break;
        }

    }
    public void CloseBox()
    {
        gameObject.SetActive(false);

        _isScroll = false;
        _isTimer =false;
        _midCheck = false;
        _curTime = 0;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 160, 120, 40), "time : "+_curTime);
    }
}
