using DefineEnums;  //class의 enum 사용
using DefineUtility;
using System.Collections.Generic;
using UnityEngine;
public class IngameManager : MonoBehaviour
{
    static IngameManager _uniqueInstance;   // 인스턴스 정적 메모리 변수 (객체 참조)---1

    Dictionary<string, GameObject> _prefabPool;

    Dictionary<UIWndName, GameObject> _prefabUIWnd;

    //기본 정보 변수
    const float _readyTime = 3, _endTime = 3f;


    //참조 변수
    Transform _uiMainFrameRoot;
    Transform _camPos;
    Vector3 _posSpawnPlayer;

    //정보 변수
    List<ClearConditionInfo> _endConditions;
    EventTriggerRangeControl[] _etRangeControls;
    IngameState _crrentState;       //DefineEnums에서 enum-상태
    float _checkTime;
    PlayerControl _myPlayer;
    FollowCamera _myCam;
    ClearConditionInfo _nowCondition;
    [SerializeField]int _conditionIndex;
    bool _isClear;


    //UI
    TitleMessageBox _msgTBox;
    [SerializeField] InfoMessageBox _msgInfoBox;


    //===

    //test
    [SerializeField] float _guiTime;
    MessageType _guiType;
    Color _guiColor;
    public IngameState _nowState    //프로퍼티 current State 참조
    {
        get { return _crrentState; }
    }

    public static IngameManager _Instance   // 프로퍼티로(은닉) 객체 참조           ---1
    {
        get { return _uniqueInstance; }
    }
    private void Awake()
    {
        _uniqueInstance = this;     //객체 잠조 Singleton                           ---1
        _guiColor = Color.white;
    }
    private void Start()
    {
        //임시
        StateReady();
        //===
    }

    private void LateUpdate()
    {
        switch (_crrentState)
        {
            case IngameState.Ready:
                _checkTime += Time.deltaTime;
                if (_checkTime > _readyTime)
                {
                    StateStart();
                }
                break;
            case IngameState.Play:
                if (_myPlayer._isDie)
                {
                    //게임오버
                    StateEnd(true);
                }
                else if (CheckClearCondition())
                {
                    //게임 클리어
                    StateEnd(false);
                }
                break;
            case IngameState.End:
                _checkTime += Time.deltaTime;
                if (_checkTime > _endTime)
                {
                    StateResult();
                }
                break;
        }
    }
    void InitUiPrefabs()
    {
        _prefabUIWnd = new Dictionary<UIWndName, GameObject>();

        int count = (int)UIWndName.max;
        string path = "Prefabs/UIs/";
        for (int n = 0; n < count; n++)
        {
            UIWndName name = (UIWndName)n;
            GameObject prefab = Resources.Load(path + name.ToString()) as GameObject;
            _prefabUIWnd.Add(name, prefab);
        }
    }
    bool CheckClearCondition()
    {
        bool isClear = false;
        switch (_nowCondition._Type)
        {
            
            case ClearType.KillCount:
                if(_nowCondition._endCount <= 0)
                {
                    //_sectionIndex 에 대한 연산 필요
                    _conditionIndex++;///////////
                    if(_conditionIndex >= _endConditions.Count)
                        isClear = true;
                    else
                    {
                        _nowCondition = _endConditions[_conditionIndex];
                    }
                }
                break;

        }

        return isClear;
    }
    public void StateReady()
    {
        _crrentState = IngameState.Ready;

        GameObject prefab = null;
        GameObject go = null;

        SaveUseIngamePrefabs();
        InitUiPrefabs();

        //참조
        _myCam = Camera.main.GetComponent<FollowCamera>();

        go = GameObject.FindGameObjectWithTag("UIMainFrame");//GameObject.Find("IngameMainUI");
        _uiMainFrameRoot = go.transform;
        go = GameObject.FindGameObjectWithTag("PlayerSpawnPos");
        _posSpawnPlayer = go.transform.position;
        go = GameObject.FindGameObjectWithTag("CameraPosition");
        _camPos = go.transform;
        _etRangeControls = GameObject.FindObjectsOfType<EventTriggerRangeControl>();
        //UI
        prefab = _prefabUIWnd[UIWndName.TitleMessageBox];
        go = Instantiate(prefab, _uiMainFrameRoot);

        _msgTBox = go.GetComponent<TitleMessageBox>();

        prefab = _prefabUIWnd[(UIWndName)UIWndName.InfoMessageBox];



        //초기화
        _checkTime = 0;
        _isClear = false;
        _conditionIndex = 0;/////////////////////////
        _myCam.InitPosCam(_camPos.position);
        _msgTBox.OpenBox("레디~");


        //임시
        SettingEndConditions();
    }

    void SaveUseIngamePrefabs()
    {
        _prefabPool = new Dictionary<string, GameObject>();
        GameObject go = null;
        int idNum = 0, folderCount = 0, count = 0;
        string path = string.Empty;
        folderCount = (int)IngameResourceFolderName.count;
        for (int n = 0; n < folderCount; n++)
        {
            count = PoolUtils.GetCountIngamePrefab((IngameResourceFolderName)n);
            for (int m = 0; m < count; m++)
            {
                path = "Prefabs/" + ((IngameResourceFolderName)n).ToString() + "/";
                go = Resources.Load(path + ((IngamePrefabName)idNum).ToString()) as GameObject;
                Debug.LogFormat("path : {0}, gameObjectName : {1}", path, go.name);
                _prefabPool.Add(((IngamePrefabName)idNum).ToString(), go);
                idNum++;
            }
            //
            ////Effects
            //count = (int)IngameResourceFolderName.Effects;
            //for (int m = 0; m < count; m++)
            //{
            //    string path = "Prefabs/" + IngameResourceFolderName.Effects + "/";
            //    go = Resources.Load(path + ((IngamePrefabName)idNum).ToString()) as GameObject;
            //    _prefabPool.Add((IngamePrefabName)idNum, go);
            //    idNum++;
            //}
            ////Objects
            //count = (int)IngameResourceFolderName.Objects;
            //for (int m = 0; m < count; m++)
            //{
            //    string path = "Prefabs/" + IngameResourceFolderName.Objects + "/";
            //    go = Resources.Load(path + ((IngamePrefabName)idNum).ToString()) as GameObject;
            //    _prefabPool.Add((IngamePrefabName)idNum, go);
            //    idNum++;

            //}
        }
    }

    public GameObject GetPrefabFromName(IngamePrefabName name)
    {
        string str = name.ToString();
        if (!_prefabPool.ContainsKey(str))
            return null;
        return _prefabPool[str];
    }
    public GameObject GetPrefabFromName(string name)
    {
        string str = name;
        if (!_prefabPool.ContainsKey(str))
            return null;
        return _prefabPool[str];
    }
    public void SettingEndConditions()
    {
        _endConditions = new List<ClearConditionInfo>();
        ClearConditionInfo info = new ClearConditionInfo(ClearType.KillCount, 10, -1);
        _endConditions.Add(info);
    }
    public void StateStart()
    {
        _crrentState = IngameState.Start;


        GameObject prefab = null;
        GameObject go = null;
        //플레이어 생성
        //prefab = Resources.Load("Prefabs/Characters/PlayerObj") as GameObject;
        prefab = GetPrefabFromName(IngamePrefabName.PlayerObj);
        go = Instantiate(prefab, _posSpawnPlayer, Quaternion.identity);
        _myPlayer = go.GetComponent<PlayerControl>();
        _myPlayer.InitSet("개척자", 4, 3, 100);

        _checkTime = 0;
        _msgTBox.OpenBox("스타트~");
        _myCam.StartFollow(_myPlayer.transform);
        foreach (EventTriggerRangeControl ctrl in _etRangeControls)
        {
            ctrl.CheckStart();
        }

        //임시
        _nowCondition = _endConditions[_conditionIndex];
    }
    public void StatePlay()
    {
        _crrentState = IngameState.Play;

        _msgTBox.CloseBox();
    }
    public void StateEnd(bool dead)
    {
        _crrentState = IngameState.End;
        _isClear = !dead;
        _checkTime = 0;
        if (_isClear)
        {
            _msgTBox.OpenBox("Congratulations!!!", MessageType.Timer, 3);
        }
        else
        {
            _msgTBox.OpenBox("You Bad", MessageType.Timer, 3);
        }



        foreach(EventTriggerRangeControl ctrl in _etRangeControls)
        {   //factory 정지
            ctrl.ResetFactory();
        }


    }
    public void StateResult()
    {
        _crrentState = IngameState.Result;
    }

    public void AddKillCounting()
    {
        if (_nowCondition._Type == ClearType.KillCount)
            _nowCondition._endCount--;
        else
        {
            _nowCondition._endCount++;
        }
        Debug.LogFormat("endCount : {0}",_nowCondition._endCount);
    }

    //UI용
    public void OpenInfoMsgBox(string msg, Color color = new Color(),
        MessageType type = MessageType.Standard, float delay = 2.0f)
    {
        if (_msgInfoBox == null)
        {
            GameObject prefab = _prefabUIWnd[UIWndName.InfoMessageBox];
            GameObject go = Instantiate(prefab, _uiMainFrameRoot);
            _msgInfoBox = go.GetComponent<InfoMessageBox>();
        }
        _msgInfoBox.OpenBox(msg, color, type, delay);
    }
    public void CloseInfoMsgBox()
    {
        if (_msgInfoBox != null)
            _msgInfoBox.CloseBox();
    }

    private void OnGUI()
    {//d,u,l,r 애니메이션
        if (GUI.Button(new Rect(0, 0, 120, 40), "Standard, 2.0f"))
        {   //animator 구조결합 동작 추천
            _msgTBox.OpenBox("Standard, 2.0f", MessageType.Standard, 2.0f);
        }
        if (GUI.Button(new Rect(0, 40, 120, 40), "Timer, 4.0f"))
        {
            _msgTBox.OpenBox("Timer, 4.0f", MessageType.Timer, 4.0f);
        }
        if (GUI.Button(new Rect(0, 80, 120, 40), "Scroll, 3.0f"))
        {
            _msgTBox.OpenBox("Scroll, 3.0f", MessageType.Scroll, 3.0f);
        }
        if (GUI.Button(new Rect(0, 120, 120, 40), "InitOption"))
        {
            _msgTBox.InitOption();
        }
        if (GUI.Button(new Rect(0, 160, 120, 40), "state Play"))
        {
            StatePlay();
        }



        if (GUI.Button(new Rect(160, 0, 120, 40), "Standard"))
        {
            _guiType = MessageType.Standard;
        }
        if (GUI.Button(new Rect(160, 40, 120, 40), "Timer"))
        {
            _guiType = MessageType.Timer;
        }
        if (GUI.Button(new Rect(160, 80, 120, 40), "Blink"))
        {
            _guiType = MessageType.Blink;
        }
        if (GUI.Button(new Rect(160, 120, 120, 40), "Fade"))
        {
            _guiType = MessageType.Fade;
        }
        if (GUI.Button(new Rect(160, 160, 120, 40), "ColorChange"))
        {
            Color c1 = Color.black;
            Color c2 = Color.cyan;
            if (_guiColor == c1) _guiColor = c2;
            else _guiColor = c1;

        }
        if (GUI.Button(new Rect(280, 0, 120, 40), "openBox"))
        {
            _msgInfoBox.OpenBox("Text Mesage" + _guiType.ToString(), _guiColor, _guiType, _guiTime);
        }
        if (GUI.Button(new Rect(280, 40, 120, 40), "closeBox"))
        {
            _msgInfoBox.CloseBox();
        }
    }
}
