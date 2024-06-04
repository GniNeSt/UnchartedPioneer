using DefineEnums;  //class의 enum 사용
using DefineUtility;
using System.Collections.Generic;
using UnityEngine;
public class IngameManager : MonoBehaviour
{
    static IngameManager _uniqueInstance;   // 인스턴스 정적 메모리 변수 (객체 참조)---1

    Dictionary<string, GameObject> _prefabPool;

    //기본 정보 변수
    const float _readyTime = 3;


    //참조 변수
    Transform _uiMainFrameRoot;
    Vector3 _posSpawnPlayer;

    //정보 변수
    IngameState _crrentState;       //DefineEnums에서 enum-상태
    float _checkTime;

    //임시 변수
    TitleMessageBox _msgTBox;
    PlayerControl _myPlayer;
    //===

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
        }
    }

    public void StateReady()
    {
        _crrentState = IngameState.Ready;

        GameObject prefab = null;
        GameObject go = null;

        SaveUseIngamePrefabs();
        //참조
        prefab = Resources.Load("Prefabs/UIs/TitleMessageBox") as GameObject;
        go = GameObject.FindGameObjectWithTag("UIMainFrame");//GameObject.Find("IngameMainUI");
        _uiMainFrameRoot = go.transform;
        go = GameObject.FindGameObjectWithTag("PlayerSpawnPos");
        _posSpawnPlayer = go.transform.position;
        go = Instantiate(prefab, _uiMainFrameRoot);

        _msgTBox = go.GetComponent<TitleMessageBox>();


        //초기화
        _checkTime = 0;
        _msgTBox.OpenBox("레디~");
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

        _checkTime = 0;
        _msgTBox.OpenBox("스타트~");
    }
    public void StatePlay()
    {
        _crrentState = IngameState.Play;
    }
    public void StateEnd()
    {
        _crrentState = IngameState.End;
    }
    public void StateResult()
    {
        _crrentState = IngameState.Result;
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
    }
}
