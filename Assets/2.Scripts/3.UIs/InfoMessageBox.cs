using DefineEnums;
using UnityEngine;
using UnityEngine.UI;
public class InfoMessageBox : MonoBehaviour
{
    Text _txtInfo;
    MessageType _nowType;
    [SerializeField]float _delayTime, _curTime, _blinkTime;
    [SerializeField]bool _fadeCheck;

    //�ӽ�====
    
    private void Update()
    {
        _curTime += Time.deltaTime;
        switch (_nowType)
        {
            case MessageType.Timer:
                if (_curTime >= _delayTime)
                {   //Ÿ�̸� �ð��� �� �Ǹ� ����/�ʱ�ȭ(CloseBox)
                    CloseBox();
                }
                break;
            case MessageType.Blink:
                _blinkTime += Time.deltaTime;
                if (_blinkTime >= 0.5f)
                {
                    _txtInfo.enabled = !_txtInfo.enabled;
                    _blinkTime = 0;
                }
                if (_curTime >= _delayTime)
                {
                    CloseBox();
                }
                break;
            case MessageType.Fade:
                if (_fadeCheck)
                {
                    if (_curTime >= 1.5f)
                    {
                        _fadeCheck = false;
                        CloseBox();
                    }
                }
                else if (_curTime >= _delayTime)
                {
                    _txtInfo.CrossFadeAlpha(0, 1, false);
                    _curTime = 0;
                    _fadeCheck = true;
                }
                break;
            default:
                break;
        }

    }

    public void OpenBox(string msg, Color color = new Color(),
        MessageType type = MessageType.Standard, float delay = 2.0f)
    {
        //gamobj.setActiv
        gameObject.SetActive(true);
        if (_txtInfo == null)
            _txtInfo = transform.GetChild(0).GetComponent<Text>();
        _txtInfo.text = msg;
        _txtInfo.color = color;
        _nowType = type;
        _delayTime = delay;

    }
    public void CloseBox()
    {
        _curTime = _blinkTime = 0;
        _txtInfo.gameObject.SetActive(true);
        _txtInfo.CrossFadeAlpha(1, 0, true);
        gameObject.SetActive(false);
    }

}
