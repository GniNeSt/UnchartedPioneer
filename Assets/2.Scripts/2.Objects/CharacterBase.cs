using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
public class CharacterBase : MonoBehaviour
{
    protected bool _isDead;

    string _name;
    int _att;
    int _def;

    protected int _baseAtt { get { return _att; } }
    protected int _baseDef { get { return _def; } }

    public string _myName { get { return _name; } }
    protected int _hp { get; set; }
    protected int _maxHp { get; set; }

    public float _hpRate { get { return (float)_hp/ _maxHp; } }

    protected void InitBase(string name, int att, int def, int hp)
    {
        _name = name;
        _att = att;
        _def = def;
        _hp = _maxHp = hp;
    }

}
