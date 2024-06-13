using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
using UnityEngine.UI;
public class CountInfoBox : MonoBehaviour
{
    [SerializeField] Image _iconImage;
    [SerializeField] Text _killCount, _score;
    float _point = 1;
    public void InitSet(Sprite sprite, int count)
    {
        _iconImage.sprite = sprite;
        _killCount.text = count.ToString();
        _score.text = (count * _point).ToString();
    }
}
