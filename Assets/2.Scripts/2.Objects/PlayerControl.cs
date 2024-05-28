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

    //참조 변수
    Animator _aniController;
    SpriteRenderer _body;
    SpriteRenderer _arm;
    SpriteRenderer _weapon;
    Transform _rootBone;
    //정보 변수
    bool _isRun;
    bool _isAttack;
    bool _isDead;
    float _moveSpeed = 0;
    CharActionState _currentState;
    CharDirection _currentDir;

    public bool _isAttacked
    {
        get { return _isAttack; }
    }

    void Awake()
    {
        //임시
        InitSet();
        //==
    }
    private void Update()
    {
        if (_isDead) return;

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

        if (Input.GetKeyDown(KeyCode.Space))
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
            _moveSpeed = _walkSpeed;
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
        if (_isDead) return;

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
            case CharActionState.Die:
                _isDead = true;
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
                _weapon.transform.position = transform.position + new Vector3(-_stdWeoponPosX, 0.5f);   //강사님은 다르게 하심
                break;
            case CharDirection.UP:
                _weapon.sortingOrder = -1;
                _weapon.transform.position = transform.position + new Vector3(0, 1.5f);
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
        _rootBone = transform.GetChild(1);
        _weapon.gameObject.SetActive(false);
        _arm.gameObject.SetActive(false);
    }

    public Transform GetDirectionFirePos()
    {
        return _rootBone.GetChild((int)_currentDir);
    }

    //private void OnGUI()
    //{//d,u,l,r 애니메이션
    //    if (GUI.Button(new Rect(0, 0, 120, 40), "Idle_d"))
    //    {   //animator 구조결합 동작 추천
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
