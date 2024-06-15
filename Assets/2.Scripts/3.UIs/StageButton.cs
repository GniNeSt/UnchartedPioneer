using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageButton : MonoBehaviour
{
    Image _buttonBG;
    Image _lock;
    Text _txtTitle;

    int _stageID;

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
        if (_stageID == nowStage)
            _buttonBG.color = new Color(255, 0, 230);
    }
}
