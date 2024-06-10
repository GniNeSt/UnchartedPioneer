using DefineEnums;
using UnityEngine;
using UnityEngine.UI;

//���ϴ� character, ��� ��� �ð�(�ð� �̹��� + �� : ��� �� + �Ķ����� ����, �������� ����), ���� ���� ������ ���� óġ ��

public class TitleMessageBox : MonoBehaviour
{
    const float _allowable = 1.5f;  //������---------
    //����
    [SerializeField] Text _txtMessage;
    [SerializeField] RectTransform _scrollObj;

    //����
    [SerializeField] float _gap;    //�̰� �Ÿ�
    [SerializeField] float _scrollSpeed = 0.2f;
    float _curTime;
    float _delayTime;

    //Type üũ
    //bool _isScroll;
    //bool _isTimer;
    bool _midCheck;

    Vector3 _startPos;
    Vector3 _midPos;
    Vector3 _endPos;
    MessageType curType;
    private void Awake()
    {
        InitOption();           //���� �ʱ�ȭ----------1
        _gap += _scrollObj.rect.width/2; // /2�� �ٿ��� �ɵ�?
    }
    public void InitOption()    //���� �ʱ�ȭ----------1
    {
        Vector3 scrollRange = new Vector3(_gap, 0, 0);  //�̰� �Ÿ�
        _startPos = _scrollObj.position + scrollRange;   //����
        _midPos = gameObject.transform.position;   //�߰�
        _endPos = _scrollObj.position - scrollRange; //����
        CloseBox(); //Clear
    }
    private void Update()
    {
        if (gameObject.activeSelf)//�ʿ� ���� ��?
        {
            switch (curType)    //type üũ
            {
                case MessageType.Timer:
                    _curTime += Time.deltaTime;
                    if (_curTime >= _delayTime)
                    {   //Ÿ�̸� �ð��� �� �Ǹ� ����/�ʱ�ȭ(CloseBox)
                        CloseBox();
                    }
                    break;
                case MessageType.Scroll:
                    if (!_midCheck) //midPos�� �������� ���ߴٸ�
                    {
                        if ((_scrollObj.position.x - _midPos.x) < _allowable)   //���� ��ġ ��ǥ(mid) ��ġ ���� üũ
                            _midCheck = true;   //����
                        else     //�̵�
                            _scrollObj.position = Vector3.Lerp(_scrollObj.position, _midPos, _scrollSpeed * Time.deltaTime);
                    }
                    else
                    {
                        _curTime += Time.deltaTime; //midPos ���� ��, ������ üũ
                        if (_curTime >= _delayTime) //���� �ð��� ������ �ð��� ������
                        {   //�̵�
                            _scrollObj.position = Vector3.Lerp(_scrollObj.position, _endPos, _scrollSpeed * Time.deltaTime);
                        }
                        if ((_scrollObj.position.x - _endPos.x) < _allowable)   //���� ��ġ ��ǥ(end) ��ġ ���� üũ
                            CloseBox();
                    }
                    break;
                case MessageType.Standard:  //null, default?
                    break;
            }
        }
    }
    public void OpenBox(string message, MessageType type = MessageType.Standard, float delay = 1f)//delay�� �����ִ� �ð�
    {
        //_txtMessage = transform.GetChild(0).GetComponent<Text>();

        gameObject.SetActive(true);
        _txtMessage.text = message;
        //---------------����---------------
        _delayTime = delay; //delay �Ѱ��ֱ�
        curType = type; //type �Ѱ��ֱ�
        _curTime = 0;   //�ð� �ʱ�ȭ <- OpenBox�� �ߺ� ȣ��� �� ���� ����(ex. CloseBox�� ȣ����� ����)
        //
        if (type == MessageType.Scroll) 
        {//ScrollŸ���̶��
            _midCheck = false;  //�߰� üũ bool �ʱ�ȭ
            _scrollObj.position = _startPos;    //ȭ�� ������ (startPos)�̵�
        }
        else
            _scrollObj.position = _midPos;      //�߾�(midPos)���� �̵�
        //---------------------------------
    }
    public void CloseBox()
    {
        gameObject.SetActive(false);

        //---------------����---------------
        _midCheck = false;  //�ʱ�ȭ----------------���� �۵��Ǹ� ��� �ʱ�ȭ�ȴ�
        _curTime = 0;
        curType = MessageType.Standard;
        //---------------------------------

    }

    private void OnGUI()    //����׿�
    {
        GUI.Box(new Rect(0, 160, 120, 40), "time : " + _curTime);
    }
}
