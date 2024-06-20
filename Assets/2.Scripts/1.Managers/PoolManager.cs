using DefineEnums;
using DefineUtility;
using System.Collections.Generic;
using UnityEngine;
public class PoolManager : MonoBehaviour
{
    static PoolManager _uniqueInstance = null;

    Dictionary<string, GameObject> _prefabPool;

    Dictionary<UIWndName, GameObject> _prefabUIWnd;
    Dictionary<MonsterKindName, Sprite> _imgIcons;
    Dictionary<StageMapName, Sprite> _imgMaps;
    List<string> _tipDatas;

    bool _isFirst = true;
    public List<string> TipDatas
    {
        get { return _tipDatas; }
    }
    public int _tipMaxCount
    {
        get { return _tipDatas.Count; }
    }

    public static PoolManager _instance
    {
        get { return _uniqueInstance; }
    }
    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
        //임시
        InitLoadData(SceneType.HomeScene);
    }
    public void InitLoadData(SceneType scene)
    {
        switch (scene)
        {
            case SceneType.HomeScene:
                Debug.Log("HomeScene 실행!");
                LoadInGameMapImages();
                LoadHomeUIPrefabs();
                //LoadInGamIconImages();
                //LoadIngameCharPrefabs();
                //HomeManager._instance.InitsetData();

                break;
            case SceneType.IngameScene:
                LoadIngameCharPrefabs();
                LoadInGameUiPrefabs();
                break;
        }
        if (_isFirst)
        {
            LoadInGamIconImages();
            LoadTipText();
            _isFirst = false;
        }
    }
    void LoadTipText()
    {
        if(_tipDatas != null)
            _tipDatas.Clear();
        else
            _tipDatas = new List<string>();

        _tipDatas.Add("장시간 게임은 실력 향상에 도움이 됩니다...");
        _tipDatas.Add("Shift를 통해 달리기를 사용할 수 있습니다..");
        _tipDatas.Add("적절한 거리를 유지하여 적을 섬멸하세요.");
        _tipDatas.Add("때로는 공격보다 회피가 더 좋을 수 있습니다.");
        _tipDatas.Add("잦은 스트레칭은 건강에 이롭습니다.");
        _tipDatas.Add("귀여운 몬스터라고 다가가면 크게 다치게 됩니다.");
        _tipDatas.Add("최고의 방어는 적을 쓰러뜨리는 것");
    }
    #region[Home 용]
    void LoadHomeUIPrefabs()
    {
        if (_prefabUIWnd != null)
        {
            _prefabUIWnd.Clear();
        }
        else
            _prefabUIWnd = new Dictionary<UIWndName, GameObject>();

        int count = (int)UIWndName.max2 - PoolUtils._homeUIOffsetIndex;
        string path = "Prefabs/UIs/";
        for (int n = 0; n < count; n++)
        {
            UIWndName name = (UIWndName)(n + PoolUtils._homeUIOffsetIndex);
            GameObject prefab = Resources.Load(path + name.ToString()) as GameObject;
            Debug.LogFormat("{0} 로드 성공!!", prefab.name);
            _prefabUIWnd.Add(name, prefab);
        }

    }
    #endregion[Home 용]
    #region[Ingame용]
    void LoadIngameCharPrefabs()
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
    #endregion[Ingame용]
    void LoadInGameMapImages()
    {
        //if (_imgMaps != null)
        //    _imgMaps.Clear();
        //else
        _imgMaps = new Dictionary<StageMapName, Sprite>();
        int count = (int)StageMapName.count;
        string path = "Images/Maps/";
        for (int n = 0; n < count; n++)
        {
            StageMapName kind = (StageMapName)n;
            Debug.Log("맵 경로 추적 중..." + kind.ToString());
            Sprite icon = Resources.Load<Sprite>(path + kind.ToString());
            //Debug.Log(icon.name);
            _imgMaps.Add(kind, icon);
        }
    }
    void LoadInGameUiPrefabs()
    {

        if (_prefabUIWnd != null)
        {
            _prefabUIWnd.Clear();
        }
        else
            _prefabUIWnd = new Dictionary<UIWndName, GameObject>();

        int count = (int)UIWndName.max1;
        string path = "Prefabs/UIs/";
        for (int n = 0; n < count; n++)
        {
            UIWndName name = (UIWndName)n;
            GameObject prefab = Resources.Load(path + name.ToString()) as GameObject;
            _prefabUIWnd.Add(name, prefab);
        }
    }
    void LoadInGamIconImages()
    {
        if (_imgIcons != null)
            _imgIcons.Clear();
        else
            _imgIcons = new Dictionary<MonsterKindName, Sprite>();
        int count = (int)MonsterKindName.Count;
        string path = "Images/Icon/";
        for (int n = 0; n < count; n++)
        {
            MonsterKindName kind = (MonsterKindName)n;
            Sprite icon = Resources.Load(path + kind.ToString()) as Sprite;
            _imgIcons.Add(kind, icon);
        }
    }
    public GameObject GetPrefabFromName(IngamePrefabName name)
    {
        string str = name.ToString();
        if (!_prefabPool.ContainsKey(str))
            return null;
        return _prefabPool[str];
    }
    /// <summary>
    /// //////////////////////////////지금 오류뜸////////////////////////////
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetPrefabFromName(string name)
    {
        string str = name;
        if (!_prefabPool.ContainsKey(str))
            return null;
        return _prefabPool[str];
    }
    public GameObject GetUIPrefabFromName(UIWndName name)
    {
        if (!_prefabUIWnd.ContainsKey(name))
            return null;
        return _prefabUIWnd[name];
    }
    public Sprite GetIconFromName(MonsterKindName name)
    {
        if (!_imgIcons.ContainsKey(name))
            return null;
        return _imgIcons[name];
    }
    public Sprite GetMapFromName(StageMapName name)
    {
        if (!_imgMaps.ContainsKey(name))
        {
            Debug.Log("null 떴다...");
            return null;

        }
        else
        {
            //Debug.Log(_imgMaps[name].name);
        }
        return
            _imgMaps[name];
    }
    public string GetTipText(int idx)
    {
        return _tipDatas[idx];
    }
}
