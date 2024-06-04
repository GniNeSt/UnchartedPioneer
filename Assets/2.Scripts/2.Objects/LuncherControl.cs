using UnityEngine;
using DefineEnums;
public class LuncherControl : MonoBehaviour
{
    PlayerControl _pc;
    GameObject _prefabBullet;
    [SerializeField] GameObject _MuzzleFlash;

    [SerializeField] float _delayShoot = 0.2f;
    [SerializeField] float _MuzzleShowTime = 0.1f;
    /*[SerializeField]*/ float _curTime = 0f;
    float _flashTime = 0f;
    private void Awake()
    {
        _pc = GetComponent<PlayerControl>();
        //_prefabBullet = Resources.Load("Prefabs/Objects/BulletObj") as GameObject;
        _prefabBullet = IngameManager._Instance.GetPrefabFromName(IngamePrefabName.BulletObj);

    }
    private void Update()
    {
        if (_pc._isDie) return; //0604 추가
        if (_pc._isAttacked)
        {
            if (Time.time - _curTime >= _delayShoot)
            {
                //시간 체크 fire실행
                _curTime = Time.time;
                Transform pc = _pc.GetDirectionFirePos();
                //FlickMuzzle(pc);
                Fire(pc);
            }
        }
        if (_MuzzleFlash.activeSelf)
        {
            _flashTime += Time.deltaTime;
            if(_flashTime >= _MuzzleShowTime)
            {
                _MuzzleFlash.SetActive(false);
                _flashTime = 0f;
            }
        }
    }
    public void Fire(Transform pos)
    {
        _MuzzleFlash.transform.position = pos.position;
        _MuzzleFlash.transform.rotation = pos.rotation;
        _MuzzleFlash.SetActive(true);
        GameObject go = Instantiate(_prefabBullet, pos.position, pos.rotation);
        BulletControl bc = go.GetComponent<BulletControl>();
        //Vector2 dir = -pos.right;
        //Debug.Log(dir);
        bc.Shoot(_pc._finalAtt);

    }
    private void FlickMuzzle(Transform pos)
    {
        _MuzzleFlash.transform.position = pos.position;
        _MuzzleFlash.transform.rotation = pos.rotation;
        _MuzzleFlash.SetActive(true);
        //Invoke("MuzzleActiveF", _MuzzleShowTime);
    }
    //private void MuzzleActiveF()
    //{        
    //    _MuzzleFlash.SetActive(false);
    //}
}
