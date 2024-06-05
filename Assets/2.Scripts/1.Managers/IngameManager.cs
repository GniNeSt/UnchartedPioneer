using DefineEnums;  //class�� enum ���
using DefineUtility;
using System.Collections.Generic;
using UnityEngine;
public class IngameManager : MonoBehaviour
{
    static IngameManager _uniqueInstance;   // �ν��Ͻ� ���� �޸� ���� (��ü ����)---1

    Dictionary<string, GameObject> _prefabPool;

    Dictionary<UIWndName, GameObject> _prefabUIWnd;

    //�⺻ ���� ����
    const float _readyTime = 3;


    //���� ����
    Transform _uiMainFrameRoot;
    Vector3 _posSpawnPlayer;

    //���� ����
    IngameState _crrentState;       //DefineEnums���� enum-����
    float _checkTime;
    PlayerControl _myPlayer;
    FollowCamera _myCam;

    //UI
    TitleMessageBox _msgTBox;
    [SerializeField]InfoMessageBox _msgInfoBox;


    //===

    //test
    [SerializeField] float _guiTime;
    MessageType _guiType;
    Color _guiColor;
    public IngameState _nowState    //������Ƽ current State ����
    {
        get { return _crrentState; }
    }

    public static IngameManager _Instance   // ������Ƽ��(����) ��ü ����           ---1
    {
        get { return _uniqueInstance; }
    }
    private void Awake()
    {
        _uniqueInstance = this;     //��ü ���� Singleton                           ---1
        _guiColor = Color.white;
    }
    private void Start()
    {
        //�ӽ�
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
    void InituiPrefabs()
    {
        _prefabUIWnd = new Dictionary<UIWndName, GameObject>();

        int count = (int)UIWndName.max;
        string path = "Prefabs/UIs/";
        for(int n =0; n < count; n++)
        {
            UIWndName name = (UIWndName)n;
            GameObject prefab = Resources.Load(path + name.ToString()) as GameObject;
            _prefabUIWnd.Add(name, prefab);
        }
    }

    public void StateReady()
    {
        _crrentState = IngameState.Ready;

        GameObject prefab = null;
        GameObject go = null;

        SaveUseIngamePrefabs();
        InituiPrefabs();

        //����
        _myCam = Camera.main.GetComponent<FollowCamera>();

        prefab = _prefabUIWnd[UIWndName.TitleMessageBox];
        go = GameObject.FindGameObjectWithTag("UIMainFrame");//GameObject.Find("IngameMainUI");
        _uiMainFrameRoot = go.transform;
        go = GameObject.FindGameObjectWithTag("PlayerSpawnPos");
        _posSpawnPlayer = go.transform.position;
        go = Instantiate(prefab, _uiMainFrameRoot);

        _msgTBox = go.GetComponent<TitleMessageBox>();

        prefab = _prefabUIWnd[(UIWndName)UIWndName.InfoMessageBox];



        //�ʱ�ȭ
        _checkTime = 0;
        _msgTBox.OpenBox("����~");
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
        //�÷��̾� ����
        //prefab = Resources.Load("Prefabs/Characters/PlayerObj") as GameObject;
        prefab = GetPrefabFromName(IngamePrefabName.PlayerObj);
        go = Instantiate(prefab, _posSpawnPlayer, Quaternion.identity);
        _myPlayer = go.GetComponent<PlayerControl>();
        _myPlayer.InitSet("��ô��", 1, 3, 100);

        _checkTime = 0;
        _msgTBox.OpenBox("��ŸƮ~");
        _myCam.StartFollow(_myPlayer.transform);
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

    //UI��
    public void OpenInfoMsgBox(string msg, Color color = new Color(),
        MessageType type = MessageType.Standard, float delay = 2.0f)
    {
        if(_msgInfoBox == null)
        {
            GameObject prefab = _prefabUIWnd[UIWndName.InfoMessageBox];
            GameObject go = Instantiate(prefab, _uiMainFrameRoot);
            _msgInfoBox = go.GetComponent<InfoMessageBox>();
        }
        _msgInfoBox.OpenBox(msg, color, type, delay);
    }
    public void CloseInfoMsgBox()
    {
        if(_msgInfoBox != null)
            _msgInfoBox.CloseBox();
    }

    private void OnGUI()
    {//d,u,l,r �ִϸ��̼�
        if (GUI.Button(new Rect(0, 0, 120, 40), "Standard, 2.0f"))
        {   //animator �������� ���� ��õ
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
