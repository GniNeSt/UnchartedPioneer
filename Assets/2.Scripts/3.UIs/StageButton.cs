using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageButton : MonoBehaviour
{
    Image _buttonBG;
    Image _lock;
    Text _txtTitle;

    StagePopWnd _stagePopWnd;

    int _stageID;
    bool _isSelected;
    public void InitSetData(int id, int clearStage, int nowStage)
    {
        _buttonBG = transform.GetChild(0).GetComponent<Image>();
        _lock = transform.GetChild(1).GetComponent<Image>();
        _txtTitle = transform.GetChild(2).GetComponent<Text>();

        _stageID = id;
        if(_stageID > clearStage + 1)
            _lock.gameObject.SetActive(true);
        else
            _lock.gameObject.SetActive(false);
        EnableSelected(_stageID == nowStage);

        _txtTitle.text = "Stage " + _stageID;
    }
    public void EnableSelected(bool isEnable)
    {
        if (isEnable)
            _buttonBG.color = new Color(255, 0, 230);
        else
            _buttonBG.color = Color.white;

        _isSelected = isEnable;
    }
    public void ClickOpenButton()
    {
        if (_lock.gameObject.activeSelf)
        {
            HomeManager._instance.CheckSelectStage(_stageID);

            //popup
            if(_stagePopWnd == null)
                _stagePopWnd = HomeManager._instance._stgPopWnd;
            _stagePopWnd.InitSet();
            _stagePopWnd.OpenPopupWnd(gameObject.GetComponent<RectTransform>().anchoredPosition);
        }
    }
}
