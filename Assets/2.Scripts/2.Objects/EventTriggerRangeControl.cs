using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
public class EventTriggerRangeControl : MonoBehaviour
{
    [SerializeField] float _radius = 10;
    [SerializeField] SpawnFactory[] _factorys;

    CircleCollider2D _collider2D;
    bool _isWarning;
    public void ResetFactory()
    {
        foreach (SpawnFactory sf in _factorys)
        {
            //팩토리를 멈춘다.
            sf._isStart = false;
            //몬스터를 모두 지운다.
            sf.DeleteEnemy();
            //팩토리 리스토어를 진행.
            sf.Restore();
        }
    }
    public MonsterKindName[] GetGenMonKinds()
    {
        List<MonsterKindName>kinds = new List<MonsterKindName>(); 
        for(int i = 0; i < _factorys.Length; i++)
        {
            if(!kinds.Contains(_factorys[i]._generateMonsterKind))
                kinds.Add(_factorys[i]._generateMonsterKind);
        }
        return kinds.ToArray();
    }
    private void Awake()
    {
        _collider2D = GetComponent<CircleCollider2D>();
        _collider2D.radius = _radius;
        _isWarning = false;
        _collider2D.enabled = false;
    }
    public void CheckStart()
    {
        _isWarning = true;
        _collider2D.enabled = true;
    }

    private void Update()
    {
        if (_isWarning)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(SpawnFactory fac in _factorys)
            {
                // 활성화
                fac.InitData(collision.GetComponent<PlayerControl>());
            }
        }
        _collider2D.enabled = false;
        _isWarning = true;
        IngameManager._Instance.OpenInfoMsgBox("Warning!!!", Color.red, MessageType.Blink, 4.0f);
    }
}
