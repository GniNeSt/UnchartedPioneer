using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
using DefineUtility;
public class PoolManager : MonoBehaviour
{
    static PoolManager _uniqueInstance= null;

    Dictionary<string, GameObject> _prefabPool;

    Dictionary<UIWndName, GameObject> _prefabUIWnd;
    Dictionary<MonsterKindName, Sprite> _imgIcons;
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
                LoadInGameUiPrefabs();
                LoadHomeUIPrefabs();
                LoadIngaeCharPrefabs();
                HomeManager._instance.InitsetData();
                break;
            case SceneType.IngameScene:
                break;
        }
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
        for(int n = 0; n <count; n++)
        {
            UIWndName name = (UIWndName)(n + PoolUtils._homeUIOffsetIndex);
            GameObject prefab = Resources.Load(path + name.ToString()) as GameObject;
            _prefabUIWnd.Add(name, prefab);
        }
            
    }
    #endregion[Home 용]
    #region[Ingame용]
    void LoadIngaeCharPrefabs()
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
        if(_imgIcons != null)
            _imgIcons.Clear();
        else
            _imgIcons = new Dictionary<MonsterKindName, Sprite>();
        int count = (int)MonsterKindName.Count;
        string path = "Prefabs/Images/Icon/";
        for(int n = 0; n < count; n++)
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

}
