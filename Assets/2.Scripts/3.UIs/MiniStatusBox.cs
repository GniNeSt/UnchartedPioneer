using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniStatusBox : MonoBehaviour
{
    [SerializeField]Text _name,_att,_def;
    [SerializeField]Slider _hpBar;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void InitStauts(string name, float att, float def, float value)
    {
        _name.text = name;
        _att.text = att.ToString();
        _def.text = def.ToString();
        _hpBar.value = value;
    }

    public void SetHpBar(float value)
    {
        _hpBar.value = value;
    }
    
}
