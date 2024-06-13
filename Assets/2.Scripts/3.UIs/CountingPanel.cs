using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountingPanel : MonoBehaviour
{
    Image _iconTarget;
    Text _txtCount;

    public void InitSet(Sprite icon)
    {
        _iconTarget = transform.GetChild(0).GetComponent<Image>();
        _txtCount = _iconTarget.transform.GetChild(0).GetChild(0).GetComponent<Text>();

        _iconTarget.sprite = icon;
        _txtCount.text = "0";
    }
    public void SetCount(int val)
    {
        _txtCount.text = val.ToString();
    }
}
