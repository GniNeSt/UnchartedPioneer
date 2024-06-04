using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;

public class SpawnFactory : MonoBehaviour
{
    [Header("생성 파파미터")]
    [SerializeField] MonsterKindName _spawnMon;
    [SerializeField] float _generateDelayTime = 5f;     //생성 주기
    [SerializeField, Range(1,5)] int _genPerCount = 1;      //주기당 생성 개수
    [SerializeField] int _maximumGenCount = 10; //최대 생성 개수
    [SerializeField] int _limitLiveCount = 3; //한계 생존 개수

    [SerializeField]public List<GameObject> _generateObjs;
    GameObject _prefabSpawnObject;
    PlayerControl _pc;
    
    [SerializeField]float _checkTime;
    [SerializeField] int _genCount;
    bool _doOnce;
    public bool _isStart
    {
        get; set;
    }

    public void InitData()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        _pc = go.GetComponent<PlayerControl>();
        _prefabSpawnObject = IngameManager._Instance.GetPrefabFromName(_spawnMon.ToString());
        _generateObjs = new List<GameObject>();
        _isStart = true;
    }
    private void Update()
    {
        if(IngameManager._Instance._nowState == IngameState.Play && !_doOnce)
        {
            _doOnce = true;
            InitData();
        }
        if (_pc == null) return;
        else if (_pc._isDie) return;

        if (_isStart && _genCount < _maximumGenCount)//최대 생성 개수
        {
            //_checkTime += Time.deltaTime;
            if (_generateObjs.Count >= _limitLiveCount)
            {   
                return;//한계 생존 개수
            }
            else _checkTime += Time.deltaTime;

            if(_checkTime >= _generateDelayTime)//생성 주기
            {
                _checkTime = 0;
                for (int i = 0; i < _genPerCount; i++)//주기당 생성 개수
                {
                    //다중 생성 위치 차이 추가
                    GameObject go = Instantiate(_prefabSpawnObject, transform.position, Quaternion.identity);
                    _generateObjs.Add(go);
                    
                    _genCount++;
                    if (_generateObjs.Count >= _limitLiveCount)break;//한계 생존 개수
                }
            }
        }
    }
    private void LateUpdate()
    {
        foreach (GameObject go in _generateObjs)
        {//list null 제거
            if (go == null)
            {
                _generateObjs.Remove(go);
                break;
            }
        }
    }
}
