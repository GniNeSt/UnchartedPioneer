using DefineEnums;
using UnityEngine;
public class MonsterControl : CharacterBase
{
    [Header("stat Parma")]
    [SerializeField] float _moveSpeed = 4f;
    [SerializeField] float _rangeAtt = 1.5f;
    bool isAttackTime;

    //���� ����
    Animator _aniController;
    SpriteRenderer _model;
    PlayerControl _target;//�ӽ�
    Vector3 _targetDir;
    BoxCollider2D[] _colAtts;



    //���� ����
    MonsterRank _mRank;
    CharActionState _currentState;
    CharDirection _currentDir;

    public int _finalAtt
    {
        get
        {
            float magnifi = VariationToRank((int)_mRank);
            return (int)(_baseAtt * magnifi);
        }
    }
    public int _finalDef
    {
        get
        {
            float magnifi = VariationToRank((int)_mRank);
            return (int)(_baseDef * magnifi);
        }
    }

    //gui�� ����
    CharActionState _guiCurrentState;
    CharDirection _guiCurrentDir;

    private void Awake()
    {
        //�ӽ�
        //InitSet("���� ����", 4, 0, 60, MonsterRank.Normal);
    }
    private void Update()
    {
        if (_isDead || isAttackTime) return;
        if (_target == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go != null)
            {
                _target = go.GetComponent<PlayerControl>();
            }
            else
                ExchangeActionToAni(CharActionState.Idle, _currentDir);
        }
        else
        {
            if (_target._isDie)
            {
                ExchangeActionToAni(CharActionState.Idle, _currentDir);
                return;
            }
            _targetDir = _target.transform.position - transform.position;
            if (Vector3.Distance(transform.position, _target.transform.position) >= _rangeAtt)
            {
                //�̵�
                transform.Translate(_targetDir.normalized * _moveSpeed * Time.deltaTime);
                //����
                _guiCurrentState = CharActionState.Run;
                //����
                //float x = 0f, y = 0f;
                //x = _target.transform.position.x - transform.position.x;
                //y = _target.transform.position.y - transform.position.y;
                //�ִϸ�����
                ExchangeActionToAni(_guiCurrentState, getTargetDir());//---1
            }
            else
            {
                //����
                //����
                _guiCurrentState = CharActionState.Attack;
                //����
                //�ִϸ�����
                ExchangeActionToAni(_guiCurrentState, getTargetDir());//---1

            }
        }
    }
    void setAttackTime()
    {
        isAttackTime = !isAttackTime;
    }
    private CharDirection getTargetDir()    //���� �ʱ�ȭ �Լ�         ----1
    {
        if (_target == null)
        {
            return CharDirection.DOWN;
        }
        //Vector3 targetVec = _target.transform.position - transform.position;
        float x = _targetDir.x;
        float y = _targetDir.y;
        CharDirection dirVertical, dirHorizontal;
        dirHorizontal = x > 0 ? CharDirection.RIGHT : CharDirection.LEFT;
        dirVertical = y > 0 ? CharDirection.UP : CharDirection.DOWN;
        return Mathf.Abs(x) > Mathf.Abs(y) ? dirHorizontal : dirVertical;
        {
            //float verAngle = Vector3.Dot(targetVec, Vector3.up);
            //float horAngle = Vector3.Dot(targetVec, Vector3.right);
            //CharDirection dirVerical, dirHorizontal;
            //if (verAngle < 0)
            //{
            //    dirVerical = CharDirection.DOWN;
            //}
            //else dirVerical = CharDirection.UP;
            //if (horAngle < 0)
            //{
            //    dirHorizontal = CharDirection.LEFT;
            //}
            //else dirHorizontal = CharDirection.RIGHT;
            //_currentDir = verAngle > horAngle ? dirVerical : dirHorizontal;
        }
        //����� ���� ��� �ڵ�
        //Vector3 dir = /*(target - now).normalized;*/ Vector3.zero;
        //Vector3 cross = Vector3.Cross(transform.forward, dir);
        //float dot = Vector3.Dot(Vector3.down, dir);
        //float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        //if (cross.y < 0)
        //    angle *= -1;

        //if (angle > -135 && angle <= -45) return CharDirection.LEFT;
        //if (angle < 135 && angle >= 45) return CharDirection.RIGHT;
        //if (angle > -45 && angle < 45) return CharDirection.DOWN;
        //if (angle < -135 || angle > -45) return CharDirection.UP;
        //return CharDirection.DOWN;
    }
    float VariationToRank(int rank)
    {
        float magnifi = 0;      //int�� �ٲ���ϴ°� ���� -> 0���� ������ ����----------------
        for (int n = 1; n <= rank; n++)
        {
            magnifi += n;
        }
        magnifi = (magnifi > 1) ? (magnifi / 2.0f) : magnifi;
        Debug.Log(magnifi);
        return magnifi;
    }
    void ExchangeActionToAni(CharActionState state, CharDirection dir)
    {
        if (_isDead) return;

        if (_currentState == state && _currentDir == dir) return; //AnyState�� ��쿡�� ���

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
    public void InitSet(string name, int att, int def, int hp, MonsterRank rank, PlayerControl target)
    {
        InitBase(name, att, def, hp);
        _mRank = rank;
        _aniController = GetComponent<Animator>();
        _model = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Transform tf = transform.GetChild(1);
        _colAtts = new BoxCollider2D[tf.childCount];
        for (int n = 0; n < tf.childCount; n++)
        {
            _colAtts[n] = tf.GetChild(n).GetComponent<BoxCollider2D>();
            _colAtts[n].enabled = false;
        }

        _target = target;
    }
    public void EnableAttack(int id)  //-> _currentDir �޾Ƶ� �ɵ�? -> int id = (int)_currentDir;
    {
        if (_currentDir == CharDirection.RIGHT) id++;   //������
        for (int n = 0; n < _colAtts.Length; n++)
        {//�ʱ�ȭ --> �ִϸ��̼� �߰��� ȣ��� ���� DamageZone�� ����ִ� ����
            //if(n != id)   //��� �ɵ� -> ���� �ʱ�ȭ
            _colAtts[n].enabled = false;
        }
        _colAtts[id].enabled = true;

        isAttackTime = true;
    }
    private void DisableAttack(int id)  //-> _currentDir �޾Ƶ� �ɵ�? -> int id = (int)_currentDir;
    {
        if (_currentDir == CharDirection.RIGHT) id++;   //������ 
        _colAtts[id].enabled = false;

        isAttackTime = false;
    }
    public void OnHitting(int Damage)
    {
        int finishDam = Damage - _finalDef;
        finishDam = (finishDam < 1) ? 1: finishDam;
        _hp -= finishDam;
        if(_hp <= 0)
        {
            _hp = 0;

            ExchangeActionToAni(CharActionState.Die, _currentDir);
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject,5);            
        }

    }

    //private void OnGUI()
    //{
    //    //idle

    //    float x = 120;
    //    if (GUI.Button(new Rect(x, 0, 120, 40), "Idle"))
    //    {
    //        _guiCurrentState = CharActionState.Idle;
    //    }
    //    if (GUI.Button(new Rect(x, 40, 120, 40), "Run"))
    //    {
    //        _guiCurrentState = CharActionState.Run;
    //    }
    //    if (GUI.Button(new Rect(x, 80, 120, 40), "Hit"))
    //    {
    //        _guiCurrentState = CharActionState.Hit;
    //    }
    //    if (GUI.Button(new Rect(x, 120, 120, 40), "Attack"))
    //    {
    //        _guiCurrentState = CharActionState.Attack;
    //    }
    //    if (GUI.Button(new Rect(x, 160, 120, 40), "Die"))
    //    {
    //        _guiCurrentState = CharActionState.Die;
    //    }
    //    //����
    //    x = 240;
    //    if (GUI.Button(new Rect(x, 0, 120, 40), "Down"))
    //    {
    //        _guiCurrentDir = CharDirection.DOWN;
    //    }
    //    if (GUI.Button(new Rect(x, 40, 120, 40), "Up"))
    //    {
    //        _guiCurrentDir = CharDirection.UP;
    //    }
    //    if (GUI.Button(new Rect(x, 80, 120, 40), "Left"))
    //    {
    //        _guiCurrentDir = CharDirection.LEFT;
    //    }
    //    if (GUI.Button(new Rect(x, 120, 120, 40), "Right"))
    //    {
    //        _guiCurrentDir = CharDirection.RIGHT;
    //    }
    //    if (GUI.Button(new Rect(360, 120, 160, 40), _guiCurrentState.ToString() + ", " + _guiCurrentDir.ToString()))
    //    {
    //        _isDead = false;
    //        ExchangeActionToAni(_guiCurrentState, _guiCurrentDir);
    //    }
    //}
}
