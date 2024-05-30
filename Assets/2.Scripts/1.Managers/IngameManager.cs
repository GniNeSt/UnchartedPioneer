using DefineEnums;  //class의 enum 사용
using UnityEngine;
public class IngameManager : MonoBehaviour
{
    static IngameManager _uniqueInstance;   // 인스턴스 정적 메모리 변수 (객체 참조)---1

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
    public void StateStart()
    {
        _crrentState = IngameState.Start;

        GameObject prefab = null;
        GameObject go = null;
        //플레이어 생성
        prefab = Resources.Load("Prefabs/Characters/PlayerObj") as GameObject;
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
}
