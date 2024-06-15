using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
using DefineUtility;
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
    PlayerControl _target;
    PlayerControl _pc;
    
    [SerializeField]float _checkTime;
    [SerializeField] int _genCount;

    float totalPrice;
    public bool _isStart
    {
        get; set;
    }

    public MonsterKindName _generateMonsterKind
    {
        get { return _spawnMon; }
    }
    public void Restore()
    {
        _generateObjs.Clear();
        _genCount = 0;
        _checkTime = 0;
    }
    public void DeleteEnemy()
    {
        foreach(var obj in _generateObjs)
        {
            obj.GetComponent<MonsterControl>().PhaseEnd();  //die
        }
    }
    //추가 스크립트
    private void RandomizeSpawnMon()
    {
        totalPrice = 0;
        for (int i = 0; i < (int)MonsterKindName.Count; i++)    //사용하는 몬스터 종류의 수를 받아오기
        {
            GameObject go = PoolManager._instance.GetPrefabFromName(((MonsterKindName)i).ToString());
            MonsterRank mr = go.GetComponent<MonsterControl>()._getRank;
            totalPrice += RarePrice.GetPriceOfMonPrefab(mr);
        }
    }
    //
    public void InitData(PlayerControl target)
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        _pc = go.GetComponent<PlayerControl>();
        _prefabSpawnObject = PoolManager._instance.GetPrefabFromName(_spawnMon.ToString());
        _generateObjs = new List<GameObject>();
        _isStart = true;
        _target = target;
    }
    private void Update()
    {
        //if(IngameManager._Instance._nowState == IngameState.Play && !_doOnce)
        //{
        //    _doOnce = true;
        //    InitData();
        //}
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

                int gapX = 0, gapY = 0, distance = 1;
                int x = _genPerCount / 4;
                for (int i = 1; i <= x; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_generateObjs.Count >= _limitLiveCount) break;
                        switch (j)
                        {
                            case (int)CharDirection.DOWN:
                                gapX = 0; gapY = -distance;
                                break;
                            case (int)CharDirection.UP:
                                gapX = 0; gapY = distance;
                                break;
                            case (int)CharDirection.LEFT:
                                gapX = -distance; gapY = 0;
                                break;
                            case (int)CharDirection.RIGHT:
                                gapX = distance; gapY = 0;
                                break;
                        }

                        Vector3 pos = transform.position + new Vector3(gapX, gapY, 0);
                        GameObject go = Instantiate(_prefabSpawnObject, pos, Quaternion.identity);

                        ///
                        MonsterControl mc = go.GetComponent<MonsterControl>();
                        mc.InitSet("방사능 젤리", 4, 0, 6, MonsterRank.Normal, _target);
                        //mc.InitSet

                        _generateObjs.Add(go);

                        _genCount++;
                        _checkTime = 0;
                    }
                    distance += 1;
                }
                //마지막 사이클의 나머지
                for (int j = 0; j < _genPerCount % 4; j++)
                {
                    if (_generateObjs.Count >= _limitLiveCount) break;
                    switch (j)
                    {
                        case (int)CharDirection.DOWN:
                            gapX = 0; gapY = -distance;
                            break;
                        case (int)CharDirection.UP:
                            gapX = 0; gapY = distance;
                            break;
                        case (int)CharDirection.LEFT:
                            gapX = -distance; gapY = 0;
                            break;
                        case (int)CharDirection.RIGHT:
                            gapX = distance; gapY = 0;
                            break;
                    }

                    Vector3 pos = transform.position + new Vector3(gapX, gapY, 0);
                    GameObject go = Instantiate(_prefabSpawnObject, pos, Quaternion.identity);


                    ///
                    MonsterControl mc = go.GetComponent<MonsterControl>();
                    mc.InitSet("방사능 젤리", 4, 0, 6, MonsterRank.Normal, _target);
                    //mc.InitSet

                    _generateObjs.Add(go);

                    _genCount++;
                    _checkTime = 0;
                }



                //for (int i = 0; i < _genPerCount; i++)//주기당 생성 개수
                //{
                //    //다중 생성 위치 차이 추가
                //    GameObject go = Instantiate(_prefabSpawnObject, transform.position, Quaternion.identity);
                //    _generateObjs.Add(go);

                //    _genCount++;
                //    if (_generateObjs.Count >= _limitLiveCount)break;//한계 생존 개수
                //}
            }
        }
    }
    private void LateUpdate()
    {
        if (!_isStart) return;
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
