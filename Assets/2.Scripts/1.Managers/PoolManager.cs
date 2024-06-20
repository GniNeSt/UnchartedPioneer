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
        //�ӽ�
        InitLoadData(SceneType.HomeScene);
    }
    public void InitLoadData(SceneType scene)
    {
        switch (scene)
        {
            case SceneType.HomeScene:
                Debug.Log("HomeScene ����!");
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

        _tipDatas.Add("��ð� ������ �Ƿ� ��� ������ �˴ϴ�...");
        _tipDatas.Add("Shift�� ���� �޸��⸦ ����� �� �ֽ��ϴ�..");
        _tipDatas.Add("������ �Ÿ��� �����Ͽ� ���� �����ϼ���.");
        _tipDatas.Add("���δ� ���ݺ��� ȸ�ǰ� �� ���� �� �ֽ��ϴ�.");
        _tipDatas.Add("���� ��Ʈ��Ī�� �ǰ��� �̷ӽ��ϴ�.");
        _tipDatas.Add("�Ϳ��� ���Ͷ�� �ٰ����� ũ�� ��ġ�� �˴ϴ�.");
        _tipDatas.Add("�ְ��� ���� ���� �����߸��� ��");
    }
    #region[Home ��]
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
            Debug.LogFormat("{0} �ε� ����!!", prefab.name);
            _prefabUIWnd.Add(name, prefab);
        }

    }
    #endregion[Home ��]
    #region[Ingame��]
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
    #endregion[Ingame��]
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
            Debug.Log("�� ��� ���� ��..." + kind.ToString());
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
    /// //////////////////////////////���� ������////////////////////////////
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
            Debug.Log("null ����...");
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
