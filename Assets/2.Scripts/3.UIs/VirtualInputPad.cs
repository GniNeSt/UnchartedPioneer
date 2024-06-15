using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class VirtualInputPad : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    Image _stickBG;
    Image _stick;
    Vector3 _inputVector;
    bool _isRun;
    bool _isFire;
    public int _horizValue
    {
        get {
            if (_inputVector.x > 0.2f)
                return 1;
            else if(_inputVector.x < -0.2f)
                return -1;
            else
                return 0;
        }
    }
    public int _vertValue
    {
        get
        {
            if (_inputVector.y > 0.2f)
                return 1;
            else if (_inputVector.y < -0.2f)
                return -1;
            else
                return 0;
        }
    }
    public bool _running
    {
        get { return _isRun; }
    }
    public bool _attack
    {
        get { return _isFire; }
    }

    

    public void InitSet()
    {
        _stickBG = transform.GetChild(0).GetComponent<Image>();
        _stick = _stickBG.transform.GetChild(0).GetComponent<Image>();

        gameObject.SetActive(true);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_stickBG.rectTransform,
            eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _stickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _stickBG.rectTransform.sizeDelta.y);

            pos.x = (pos.x > 1) ? 1 : pos.x;
            pos.y = (pos.y > 1) ? 1 : pos.y;
            
            _inputVector = new Vector3(pos.x, pos.y, 0);

            Vector3 drawVector = (_inputVector.magnitude > 1) ? _inputVector.normalized : _inputVector;

            //스틱의 위치 처리
            _stick.rectTransform.anchoredPosition = new Vector3(drawVector.x * (_stickBG.rectTransform.sizeDelta.x/3),
                drawVector.y * (_stickBG.rectTransform.sizeDelta.y / 3));

            //재계산
            float dot = Vector3.Dot(Vector3.up, _inputVector);
            float anlge = Mathf.Acos(dot);
            float deg = anlge * Mathf.Rad2Deg;
            if(deg < 15 || deg > 165) _inputVector.x = 0;
            Debug.Log(deg);
            dot = Vector3.Dot(Vector3.right, _inputVector);
            anlge = Mathf.Acos(dot);
            deg = anlge * Mathf.Rad2Deg;
            if (deg < 15 || deg > 165) _inputVector.y = 0;


            //float anlge = Mathf.Acos(Vector3.Dot(Vector3.down, drawVector));
            //angle *= Mathf.Rad2Deg;
            
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector3.zero;
        _stick.rectTransform.anchoredPosition = _inputVector;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnRunButtonDown()
    {
        _isRun = true;
    }
    public void OnRunButtonUp()
    {
        _isRun = false;
    }
    public void OnAttackButtonDown()
    {
        _isFire = true;
    }
    public void OnAttackButtonUp()
    {
        _isFire= false;
    }
}
