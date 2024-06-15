using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
using UnityEngine.UI;
public class ResultWnd : MonoBehaviour
{
    [SerializeField] Text _titleText, _killText,_playTime;
    [SerializeField] Transform _scrollObj;
    
    public void OpenWnd(bool isSuccess, GameObject prefabox, Dictionary<MonsterKindName,int>killcounts, float playTime)
    {
        int killResult = 0;
        if (isSuccess)
        {
            _titleText.text = "You Win";
            _titleText.color = Color.cyan;
        }
        else
        {
            _titleText.text = "You Bad";
            _titleText.color = Color.gray;
        }
        foreach(MonsterKindName kind in killcounts.Keys)
        {
            GameObject go = Instantiate(prefabox);
            go.transform.SetParent(_scrollObj, false);

            CountInfoBox cb = go.GetComponent<CountInfoBox>();

            cb.InitSet(PoolManager._instance.GetIconFromName(kind), killcounts[kind]);

            killResult += killcounts[kind];
        }
        _killText.text = killResult.ToString();
        string time = string.Format("{0:000}:{1:00}", playTime, (int)((playTime - (int)playTime) * 100));
        _playTime.text = time;



        gameObject.SetActive(true);
    }
    public void CloseWnd()
    {
        gameObject.SetActive(false);
    }

    public void ButtonDown(RectTransform text)
    {
        text.anchoredPosition += Vector2.down * 20;
    }
    public void ButtonUp(RectTransform text)
    {
        text.anchoredPosition += Vector2.up * 20;

    }
    public void ClickHomeButton()
    {

    }
    public void ClickRegameButton()
    {

    }
    public void ClickNextButton()
    {

    }
}
