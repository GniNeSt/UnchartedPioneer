using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefineEnums;
public class StagePopWnd : MonoBehaviour
{
    [SerializeField] Vector2 _offSet = new Vector2(30, 0);
    [SerializeField] Image _mapImg;
    private void Awake()
    {
        gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    public void InitSet()
    {
        HomeManager homeInstance = HomeManager._instance;
        int _id = homeInstance._currSelectedStageID;

        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = homeInstance.StageInfos[_id]._stageName;
        _mapImg.sprite = PoolManager._instance.GetMapFromName((StageMapName)_id);
    }
    public void OpenPopupWnd(Vector2 buttonPos)
    {
        gameObject.SetActive(true);
        RectTransform rct = transform.GetChild(0).GetComponent<RectTransform>();
        rct.anchoredPosition = buttonPos + new Vector2(400 + rct.sizeDelta.x/2, 100);
    }
    public void ClosePopupWnd()
    {
        gameObject.SetActive(false);
    }
    public void onClickStart()
    {
        SceneControlManager._instance.LoadIngameScene();//int
    }
}
