using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
using UnityEngine.UI;
using DefineUtility;
public class HomeManager : MonoBehaviour
{
    static HomeManager _uniqueInstance;

    //참조변수
    Transform _rootPos;
    Transform _rootStage;
    Dictionary<int, StageButton> _stageButtons;

    Dictionary<int, StageInfo> _stageInfos;

    StagePopWnd _stagePopWnd;
    [SerializeField]OptionPopWnd _optionPopWnd;
    public StagePopWnd _stgPopWnd
    {
        get { return _stagePopWnd; }
    }
    public Dictionary<int, StageInfo> StageInfos
    {
        get { return _stageInfos; }
    }
    //정보변수
    public int _currSelectedStageID
    {
        get { return _selectStage; }
    }
    //임시
    int _clearStage = 1;
    int _selectStage = 2;//id
    //==
    public static HomeManager _instance
    {
        get { return _uniqueInstance; }
    }
    private void Awake()
    {
        _uniqueInstance = this;
    }
    public void InitsetData()
    {
        _stageButtons = new Dictionary<int, StageButton>();
        Debug.Log("InitsetData");
        GameObject go = GameObject.FindGameObjectWithTag("RootPos");
        _rootPos = go.transform;
        go = GameObject.FindGameObjectWithTag("RootStage");
        _rootStage = go.transform;
        GameObject prefab = PoolManager._instance.GetUIPrefabFromName(UIWndName.StageButton);


        for(int n = 0; n < _rootPos.childCount; n++)
        {
            GameObject stageOb = Instantiate(prefab, _rootPos.GetChild(n).GetComponent<RectTransform>());
            Image img = stageOb.transform.GetChild(0).GetComponent<Image>();
            _stageButtons.Add(n+1, stageOb.GetComponent<StageButton>());

            //if (_clearStage == n) img.color = Color.cyan;
            //else if (_selectStage == n) img.color = Color.red;
            stageOb.transform.parent = _rootStage;

            //
           // RectTransform rtf = (RectTransform)_rootPos.GetChild(n);


            //
            stageOb.GetComponent<StageButton>().InitSetData(n+1, _clearStage, _selectStage);
        }
        InitSampleStageInfo();
        //팝업wind
        Debug.Log("wind 생성");
        prefab = PoolManager._instance.GetUIPrefabFromName(UIWndName.StagePopupWindow);
        go = Instantiate(prefab);
        _stagePopWnd = go.GetComponent<StagePopWnd>();
        _stagePopWnd.ClosePopupWnd();
    }
    public void CheckSelectStage(int id)
    {
        //전부끄기
        foreach(StageButton btn in _stageButtons.Values)
        {
            btn.EnableSelected(false);
        }
        _stageButtons[id].EnableSelected(true);
        _selectStage = id;
    }
    public void ScrollReaction()
    {
        _stagePopWnd.ClosePopupWnd();
    }
    public void InitSampleStageInfo()
    {
        _stageInfos = new Dictionary<int, StageInfo>();

        //stage1
        MonsterKindName[] name = new MonsterKindName[1];
        name[0] = MonsterKindName.SlimeObj;
        StageInfo info = new StageInfo(0, "모험의 날~", StageMapName.Stage1, name);
        _stageInfos.Add(1, info);
        //stage2
        name = new MonsterKindName[1];
        name[0] = MonsterKindName.WeakBatObj;
        info = new StageInfo(1, "한걸음을..", StageMapName.Stage2, name);
        _stageInfos.Add(2, info);
        //stage3
        name = new MonsterKindName[2];
        name[0] = MonsterKindName.WeakBatObj;
        name[1] = MonsterKindName.ModifyAlienObj;
        info = new StageInfo(2, "갈길이 멀다..", StageMapName.Stage3, name);
        _stageInfos.Add(3, info);
        //stage4
        name = new MonsterKindName[3];
        name[0] = MonsterKindName.SlimeObj;
        name[1] = MonsterKindName.WeakBatObj;
        name[2] = MonsterKindName.ModifyAlienObj;
        info = new StageInfo(3, "위기 발생!!", StageMapName.Stage4, name);
        _stageInfos.Add(4, info);
        //stage5
        name = new MonsterKindName[1];
        name[0] = MonsterKindName.WeakBatObj;
        info = new StageInfo(4, "이어지는 모험", StageMapName.Stage5, name);
        _stageInfos.Add(5, info);
    }
    public void OptionBtnClick()
    {
        Debug.Log("BtnClick");
        if(_optionPopWnd == null)
        {
            GameObject go = PoolManager._instance.GetUIPrefabFromName(UIWndName.OptionWindow);
            Instantiate(go);
            
            _optionPopWnd = go.GetComponent<OptionPopWnd>();
            _optionPopWnd.OptionWndClose();
        }

        _optionPopWnd.OpenWindow();
    }
}
