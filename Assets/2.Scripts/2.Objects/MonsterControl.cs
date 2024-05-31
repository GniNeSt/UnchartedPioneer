using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
public class MonsterControl : MonoBehaviour
{
    [Header("stat Parma")]
    [SerializeField] float _moveSpeed = 4f;
    //참조 변수
    Animator _aniController;
    SpriteRenderer _model;
    //정보 변수
    CharActionState _currentState;
    CharDirection _currentDir;
    bool _isDead;

    //gui용 변수
    CharActionState _guiCurrentState;
    CharDirection _guiCurrentDir;

    private void Awake()
    {
        //임시
        InitSet();
    }
    void ExchangeActionToAni(CharActionState state, CharDirection dir)
    {
        if (_isDead) return;

        if (_currentState == state && _currentDir == dir) return;

        switch (state)
        {
            case CharActionState.Attack:
                break;
            case CharActionState.Run:
                break;
            case CharActionState.Die:
                _isDead = true;
                break;
            default:
                break;
        }

        _aniController.SetInteger("Direction", (int)dir);
        _aniController.SetInteger("AniState", (int)state);

        SetSideDirection(dir);

        _aniController.SetTrigger("Changed");

        _currentState = state;
        _currentDir = dir;

    }

    void SetSideDirection(CharDirection dir)
    {
        _model.flipX = false;
        switch (dir)
        {
            case CharDirection.RIGHT:
                _model.flipX = true;
                break;
            case CharDirection.LEFT:
                break;
            case CharDirection.UP:
                break;
            case CharDirection.DOWN:
                break;
        }


    }
    public void InitSet()
    {
        _aniController = GetComponent<Animator>();
        _model = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void OnGUI()
    {
        //idle

        float x = 120;
        if (GUI.Button(new Rect(x, 0, 120, 40), "Idle"))
        {
            _guiCurrentState = CharActionState.Idle;
        }
        if (GUI.Button(new Rect(x, 40, 120, 40), "Run"))
        {
            _guiCurrentState = CharActionState.Run;
        }
        if (GUI.Button(new Rect(x, 80, 120, 40), "Hit"))
        {
            _guiCurrentState = CharActionState.Hit;
        }
        if (GUI.Button(new Rect(x, 120, 120, 40), "Attack"))
        {
            _guiCurrentState = CharActionState.Attack;
        }
        if (GUI.Button(new Rect(x, 160, 120, 40), "Die"))
        {
            _guiCurrentState = CharActionState.Die;
        }
        //방향
        x = 240;
        if (GUI.Button(new Rect(x, 0, 120, 40), "Down"))
        {
            _guiCurrentDir = CharDirection.DOWN;
        }
        if (GUI.Button(new Rect(x, 40, 120, 40), "Up"))
        {
            _guiCurrentDir = CharDirection.UP;
        }
        if (GUI.Button(new Rect(x, 80, 120, 40), "Left"))
        {
            _guiCurrentDir = CharDirection.LEFT;
        }
        if (GUI.Button(new Rect(x, 120, 120, 40), "Right"))
        {
            _guiCurrentDir = CharDirection.RIGHT;
        }
        if (GUI.Button(new Rect(360, 120, 160, 40), _guiCurrentState.ToString()+", "+ _guiCurrentDir.ToString()))
        {
            _isDead = false;
            ExchangeActionToAni(_guiCurrentState, _guiCurrentDir);
        }
    }
}
