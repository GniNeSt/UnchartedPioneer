using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
public class HomeManager : MonoBehaviour
{
    static HomeManager _uniqueInstance;

    //참조변수
    Transform _rootPos;
    Transform _rootStage;

    //정보변수

    //임시
    int _clearStage = 1;
    int _selectStage = 2;
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
        GameObject go = GameObject.FindGameObjectWithTag("RootPos");
        _rootPos = go.transform;
        go = GameObject.FindGameObjectWithTag("RootStage");
        _rootStage = go.transform;
        GameObject prefab = PoolManager._instance.GetUIPrefabFromName(UIWndName.StageButton);

        foreach(RectTransform childPos in _rootPos.GetComponentsInChildren<RectTransform>())    //child 바꾸기 , 현재 스테이지색 변경
        {
            GameObject stageOb = Instantiate(prefab, childPos);
            stageOb.transform.parent = _rootStage;            
        }
    }
}
