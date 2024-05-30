using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefineEnums;

public class TitleMessageBox : MonoBehaviour
{
    [SerializeField]Text _txtMessage;
    public void OpenBox(string message, MessageType type = MessageType.Standard, float delay = 0)//delay�� �����ִ� �ð�
    {
        //_txtMessage = transform.GetChild(0).GetComponent<Text>();
        gameObject.SetActive(true);

        _txtMessage.text = message;
    }
    public void CloseBox()
    {
        gameObject.SetActive(false);
    }
}
