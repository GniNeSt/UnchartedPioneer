using DefineEnums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KillLogBox : MonoBehaviour
{
    [SerializeField] Sprite[] IconMons;
    RectTransform _myTransform;
    RectTransform _buttonTrans;
    List<CountingPanel> _monPanel;

    bool _isClicked;
    private void Awake()
    {
        _buttonTrans = GetComponentInChildren<Button>().GetComponent<RectTransform>();
    }
    public void OpenBox(Dictionary<MonsterKindName, int> monKindList, CountingPanel cp)
    {
        gameObject.SetActive(true);
        _myTransform = GetComponent<RectTransform>();
        //_myTransform.localScale = new Vector3(_myTransform.localScale.x, _myTransform.localScale.y * monKindList.Count, _myTransform.localScale.z);
        _myTransform.sizeDelta = new Vector2(_myTransform.sizeDelta.x, _myTransform.sizeDelta.y * monKindList.Count);
        int count = 0;
        _monPanel = new List<CountingPanel>();
        foreach (MonsterKindName monKind in monKindList.Keys)
        {
            CountingPanel newPanel = Instantiate(cp, _myTransform);
            _monPanel.Add(newPanel);
            newPanel.GetComponent<CountingPanel>().InitSet(IconMons[(int)monKind]);
            RectTransform panelTransform = newPanel.GetComponent<RectTransform>();
            panelTransform.localPosition = _myTransform.anchoredPosition3D - new Vector3(0, panelTransform.sizeDelta.y * count++, 0);
        }

    }
    public void CloseBox()
    {
        gameObject.SetActive(false);
    }
    public void SetKillCount(MonsterKindName monKind, int count)
    {
        _monPanel[(int)monKind].SetCount(count);
    }

    public void ClickInOut()
    {
        if (_isClicked)
        {// 들어간 상태
            _isClicked = false;
            _myTransform.anchoredPosition -= new Vector2(_myTransform.sizeDelta.x,0);
            _buttonTrans.localScale = new Vector3(-_buttonTrans.localScale.x, _buttonTrans.localScale.y, _buttonTrans.localScale.z);
        }
        else
        {
            _isClicked = true;
            _myTransform.anchoredPosition += new Vector2(_myTransform.sizeDelta.x, 0);
            _buttonTrans.localScale = new Vector3(-_buttonTrans.localScale.x, _buttonTrans.localScale.y, _buttonTrans.localScale.z);
        }
    }
}
