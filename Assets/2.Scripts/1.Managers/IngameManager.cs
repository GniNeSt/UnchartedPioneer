using DefineEnums;  //class�� enum ���
using UnityEngine;
public class IngameManager : MonoBehaviour
{
    static IngameManager _uniqueInstance;   // �ν��Ͻ� ���� �޸� ���� (��ü ����)---1

    //�⺻ ���� ����
    const float _readyTime = 3;


    //���� ����
    Transform _uiMainFrameRoot;
    Vector3 _posSpawnPlayer;

    //���� ����
    IngameState _crrentState;       //DefineEnums���� enum-����
    float _checkTime;

    //�ӽ� ����
    TitleMessageBox _msgTBox;
    PlayerControl _myPlayer;
    //===

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

    public void StateReady()
    {
        _crrentState = IngameState.Ready;

        GameObject prefab = null;
        GameObject go = null;
        //����
        prefab = Resources.Load("Prefabs/UIs/TitleMessageBox") as GameObject;
        go = GameObject.FindGameObjectWithTag("UIMainFrame");//GameObject.Find("IngameMainUI");
        _uiMainFrameRoot = go.transform;
        go = GameObject.FindGameObjectWithTag("PlayerSpawnPos");
        _posSpawnPlayer = go.transform.position;
        go = Instantiate(prefab, _uiMainFrameRoot);

        _msgTBox = go.GetComponent<TitleMessageBox>();


        //�ʱ�ȭ
        _checkTime = 0;
        _msgTBox.OpenBox("����~");
    }
    public void StateStart()
    {
        _crrentState = IngameState.Start;

        GameObject prefab = null;
        GameObject go = null;
        //�÷��̾� ����
        prefab = Resources.Load("Prefabs/Characters/PlayerObj") as GameObject;
        go = Instantiate(prefab, _posSpawnPlayer, Quaternion.identity);
        _myPlayer = go.GetComponent<PlayerControl>();

        _checkTime = 0;
        _msgTBox.OpenBox("��ŸƮ~");
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
