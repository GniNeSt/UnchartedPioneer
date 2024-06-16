using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
using UnityEngine.UI;
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
            if (_clearStage == n) img.color = Color.cyan;
            else if (_selectStage == n) img.color = Color.red;
            stageOb.transform.parent = _rootStage;
        }

    }
}
