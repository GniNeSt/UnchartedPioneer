using DefineEnums;
using System.Collections.Generic;
using UnityEngine;
public class KillLogBox : MonoBehaviour
{
    [SerializeField] Sprite[] IconMons;
    RectTransform _myTransform;

    List<CountingPanel> _monPanel;
    public void OpenBox(Dictionary<MonsterKindName, int> monKindList, CountingPanel cp)
    {
        gameObject.SetActive(true);
        _myTransform = GetComponent<RectTransform>();
        //_myTransform.localScale = new Vector3(_myTransform.localScale.x, _myTransform.localScale.y * monKindList.Count, _myTransform.localScale.z);

        int count = 0;
        _monPanel = new List<CountingPanel>();
        foreach (MonsterKindName monKind in monKindList.Keys)
        {
            CountingPanel newPanel = Instantiate(cp, _myTransform);
            _monPanel.Add(newPanel);
            newPanel.GetComponent<CountingPanel>().InitSet(IconMons[(int)monKind]);
            RectTransform panelTransform = newPanel.GetComponent<RectTransform>();
            panelTransform.localPosition = _myTransform.anchoredPosition3D - new Vector3(0, panelTransform.sizeDelta.y * count++, 0);
        }

    }
    public void CloseBox()
    {
        gameObject.SetActive(false);
    }
    public void SetKillCount(MonsterKindName monKind, int count)
    {
        _monPanel[(int)monKind].SetCount(count);
    }
}
