using DefineEnums;
using UnityEngine;
public class PlayerControl : MonoBehaviour
{
    const float _stdWeoponPosX = 0.5f;
    const float _stdWeoponPosY = -0f;

    [Header("Stat Param")]
    [SerializeField] float _walkSpeed = 1f;
    [SerializeField] float _runSpeed = 3f;
    [SerializeField] SpriteRenderer _spriteRenderer;

    //���� ����
    Animator _aniController;
    SpriteRenderer _body;
    SpriteRenderer _arm;
    SpriteRenderer _weapon;
    //���� ����
    bool _isRun;
    bool _isAttack;
    float _moveSpeed = 0;
    CharActionState _currentState;
    CharDirection _currentDir;

    void Awake()
    {
        //�ӽ�
        InitSet();
        //==
    }
    private void Update()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        {
            //float mx = 0;
            //float my = 0;
            //if (Input.GetKey(KeyCode.A))
            //    mx -= 1;
            //if (Input.GetKey(KeyCode.D))
            //    mx += 1;
            //if (Input.GetKey(KeyCode.S))
            //    my -= 1;
            //if (Input.GetKey(KeyCode.W))
            //    my += 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isRun)//Run ���� �߰�
        {
            _isAttack = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isAttack = false;
        }
        if (!_isAttack)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _isRun = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _isRun = false;
            }
        }


        Vector3 dir = new Vector3(mx, my).normalized;
        transform.Translate(dir * Time.deltaTime * _moveSpeed);

        SetAnimation(dir);

    }

    void SetAnimation(Vector3 dirV)
    {
        if (_isAttack)
        {
            if (dirV.magnitude == 0)
                ExchangeActionToAni(CharActionState.Attack, _currentDir);
            else if (dirV.x < 0)
            {
                ExchangeActionToAni(CharActionState.AttackWalk, CharDirection.LEFT);
            }
            else if (dirV.x > 0)
            {
                ExchangeActionToAni(CharActionState.AttackWalk, CharDirection.RIGHT);
            }
            else if (dirV.y > 0)
            {
                ExchangeActionToAni(CharActionState.AttackWalk, CharDirection.UP);
            }
            else if (dirV.y < 0)
            {
                ExchangeActionToAni(CharActionState.AttackWalk, CharDirection.DOWN);
            }

        }
        else
        {

            if (dirV.magnitude == 0)
                ExchangeActionToAni(CharActionState.Idle, _currentDir);
            else if (dirV.x < 0)
            {
                if (_isRun)
                {
                    ExchangeActionToAni(CharActionState.Run, CharDirection.LEFT);
                }
                else
                {
                    ExchangeActionToAni(CharActionState.Walk, CharDirection.LEFT);
                }
            }
            else if (dirV.x > 0)
            {
                if (_isRun)
                {
                    ExchangeActionToAni(CharActionState.Run, CharDirection.RIGHT);
                }
                else
                {
                    ExchangeActionToAni(CharActionState.Walk, CharDirection.RIGHT);
                }
            }
            else if (dirV.y > 0)
            {
                if (_isRun)
                {
                    ExchangeActionToAni(CharActionState.Run, CharDirection.UP);
                }
                else
                {
                    ExchangeActionToAni(CharActionState.Walk, CharDirection.UP);
                }

            }
            else if (dirV.y < 0)
            {
                if (_isRun)
                {
                    ExchangeActionToAni(CharActionState.Run, CharDirection.DOWN);
                }
                else
                {
                    ExchangeActionToAni(CharActionState.Walk, CharDirection.DOWN);
                }
            }
        }
    }

    void ExchangeActionToAni(CharActionState state, CharDirection dir)
    {
        if (_currentState == state && _currentDir == dir) return;

        _aniController.SetTrigger("Changed");

        switch (state)
        {
            case CharActionState.Attack:
            case CharActionState.AttackWalk:
                _weapon.gameObject.SetActive(true);
                if (dir == CharDirection.LEFT || dir == CharDirection.RIGHT)
                    _arm.gameObject.SetActive(true);
                else
                    _arm.gameObject.SetActive(false);
                break;

            case CharActionState.Walk:
                _moveSpeed = _walkSpeed;
                _weapon.gameObject.SetActive(false);
                _arm.gameObject.SetActive(false);
                break;
            case CharActionState.Run:
                _moveSpeed = _runSpeed;
                _weapon.gameObject.SetActive(false);
                _arm.gameObject.SetActive(false);
                break;
            default:
                _weapon.gameObject.SetActive(false);
                _arm.gameObject.SetActive(false);
                break;
        }

        _aniController.SetInteger("Direction", (int)dir);
        _aniController.SetInteger("AniState", (int)state);

        SetSideDirection(dir);

        _currentState = state;
        _currentDir = dir;
    }

    void SetSideDirection(CharDirection dir)
    {
        _body.flipX = _arm.flipX = _weapon.flipX = false;
        switch (dir)
        {
            case CharDirection.RIGHT:
                _weapon.sortingOrder = 1;//order
                _weapon.transform.position = transform.position + new Vector3(_stdWeoponPosX, 0.5f);//weoponPos
                _body.flipX = _arm.flipX = _weapon.flipX = true;
                break;
            case CharDirection.LEFT:
                _weapon.sortingOrder = 1;
                _weapon.transform.position = transform.position + new Vector3(-_stdWeoponPosX, 0.5f);
                break;
            case CharDirection.UP:
                _weapon.sortingOrder = -1;
                _weapon.transform.position = transform.position + new Vector3(0,1.5f);
                break;
            case CharDirection.DOWN:
                _weapon.sortingOrder = 1;
                _weapon.transform.position = transform.position + new Vector3(0, _stdWeoponPosY);
                break;
        }


    }
    public void InitSet()
    {
        _moveSpeed = _walkSpeed;
        _aniController = GetComponent<Animator>();
        _body = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _weapon = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        _arm = transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();

        _weapon.gameObject.SetActive(false);
        _arm.gameObject.SetActive(false);
    }
    //private void OnGUI()
    //{//d,u,l,r �ִϸ��̼�
    //    if (GUI.Button(new Rect(0, 0, 120, 40), "Idle_d"))
    //    {   //animator �������� ���� ��õ
    //        Debug.Log("Idle_d !!");
    //        _aniController.SetInteger("IdleDir", 0);
    //    }
    //    if (GUI.Button(new Rect(0, 40, 120, 40), "Idle_u"))
    //    {
    //        Debug.Log("Idle_u !!");
    //        _aniController.SetInteger("IdleDir", 1);
    //    }
    //    if (GUI.Button(new Rect(0, 80, 120, 40), "Idle_l"))
    //    {
    //        Debug.Log("Idle_l !!");
    //        _spriteRenderer.flipX = false;
    //        _aniController.SetInteger("IdleDir", 2);
    //    }
    //    if (GUI.Button(new Rect(0, 120, 120, 40), "Idle_r"))
    //    {
    //        Debug.Log("Idle_r !!");
    //        _spriteRenderer.flipX = true;
    //        _aniController.SetInteger("IdleDir", 2);
    //    }
    //}
}