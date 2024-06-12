using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
using UnityEngine.UI;
public class ResultWnd : MonoBehaviour
{
    [SerializeField] Text _titleText;
    [SerializeField] Transform _scrollObj;
    public void OpenWnd(bool isSuccess, GameObject prefabox, Dictionary<MonsterKindName,int>killcounts, float playTime)
    {
        int killResult = 0;
        if (isSuccess)
        {
            _titleText.text = "You Win";
        }
        else
        {
            _titleText.text = "You Bad";
        }
        foreach(MonsterKindName kind in killcounts.Keys)
        {
            GameObject go = Instantiate(prefabox);
            go.transform.SetParent(_scrollObj, false);

            CountInfoBox cb = go.GetComponent<CountInfoBox>();

            cb.InitSet(kind, killcounts[kind]);

            killResult += killcounts[kind];
        }




        gameObject.SetActive(true);
    }
    public void CloseWnd()
    {
        gameObject.SetActive(false);
    }
}
