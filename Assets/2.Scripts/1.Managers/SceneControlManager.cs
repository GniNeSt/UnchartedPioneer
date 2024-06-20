using DefineEnums;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControlManager : MonoBehaviour
{
    static SceneControlManager _uniqueInstance;
    SceneType _nowSceneType;
    LoadingState _nowState;

    LoadingWnd _wndLoading;

    ////임시
    //AsyncOperation _nowOper;
    //bool _doOnce;
    ////===
    public static SceneControlManager _instance
    {
        get { return _uniqueInstance; }
    }
    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        LoadHomeScene();
    }
    private void Update()
    {
        //if(_nowOper != null)
        //{
        //    //Debug.Log(_nowOper.progress);
        //    if (!_nowOper.isDone)
        //    {
        //        // 여기서 상태 변화 확인.
        //    }
        //    else
        //    {
        //        _nowOper = null;
        //        switch (_nowSceneType)
        //        {

        //            case SceneType.HomeScene:
        //                HomeManager._instance.InitsetData();
        //                break;
        //            case SceneType.IngameScene:
        //                IngameManager._Instance.StateReady();
        //                break;
        //        }
        //    }

        //}

    }

    public void LoadHomeScene()
    {
        _nowSceneType = SceneType.HomeScene;
        _nowState = LoadingState.Start;
        StartCoroutine(LoadingScene("HomeScene"));
        //----_nowOper = SceneManager.LoadSceneAsync("HomeScene");
    }
    public void LoadIngameScene(int stageNum = 1)
    {
        _nowSceneType = SceneType.IngameScene;
        _nowState = LoadingState.Start;
        //_nowOper = SceneManager.LoadSceneAsync("IngameScene");
        StartCoroutine(LoadingScene("IngameScene", stageNum));
    }

    public IEnumerator LoadingScene(string sceneName, int stageNum = 0)
    {
        OpenLoadingWnd();
        AsyncOperation aOper = SceneManager.LoadSceneAsync(sceneName);
        _nowState = LoadingState.ing;

        while (!aOper.isDone)
        {
            _wndLoading.SetLoaddingRate(aOper.progress);
            yield return null;
        }
        _wndLoading.SetLoaddingRate(aOper.progress);
        yield return new WaitForSeconds(1.5f);
        _nowState = LoadingState.End;
        switch (_nowSceneType)
        {

            case SceneType.HomeScene:
                SoundManager._instance.PlayBGM(BGMClipName.HomeScene);
                PoolManager._instance.InitLoadData(SceneType.HomeScene);
                HomeManager._instance.InitsetData();
                break;
            case SceneType.IngameScene:
                SoundManager._instance.PlayBGM(BGMClipName.Ingame1);
                PoolManager._instance.InitLoadData(SceneType.IngameScene);
                IngameManager._Instance.StateReady();
                break;
        }

        CloseLoadingWnd();
    }

    void OpenLoadingWnd()
    {
        if (_wndLoading == null)
        {
            GameObject prefab = PoolManager._instance.GetUIPrefabFromName(UIWndName.LoadingWindow);
            GameObject go = Instantiate(prefab, transform);
            _wndLoading = go.GetComponent<LoadingWnd>();
        }
        _wndLoading.OpenWindow();
    }
    void CloseLoadingWnd()
    {
        if (_wndLoading != null)
            _wndLoading.CloseWindow();
    }
}
