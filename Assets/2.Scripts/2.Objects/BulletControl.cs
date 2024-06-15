using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
public class BulletControl : MonoBehaviour
{
    Rigidbody2D _rgbd2D;    //프리팹의 rigidbody
    [SerializeField] float force = 150f;    //사격 관련 변수                      ------1
    int _finishDamage;
    GameObject _effectPrefab;
    public void Shoot(int finish)
    {
        _rgbd2D = GetComponent<Rigidbody2D>();  //참조
        _rgbd2D.AddForce(-transform.right * force); //사격 rigidbody 물리 적용    ------1
        _finishDamage = finish;

        _effectPrefab = PoolManager._instance.GetPrefabFromName(IngamePrefabName.Explosion);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))   //충돌한 collision의 게임오브젝트 태그가 "Wall" 이라면
        {
            //GameObject effect = Resources.Load("Prefabs/Effects/Explosion") as GameObject;  //파일 접근 GameObject 참조  ---2
            
            GameObject go = Instantiate(_effectPrefab, transform.position, Quaternion.identity);   //참조한 GObj 생성 및 참조   ---2, 3
            Animator aniControl = go.GetComponent<Animator>();  //GObj의 Animator 컴포넌트 참조                          ---3  
            ExplosionObject eo = go.GetComponent<ExplosionObject>();      //GObj
            eo.Explosion(ExplosionType.NonBreake);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Monster"))
        {
            MonsterControl mc = collision.GetComponent<MonsterControl>();
            mc.OnHitting(_finishDamage);
            //이펙트
            GameObject go = Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            ExplosionObject eo = go.GetComponent<ExplosionObject>();
            eo.Explosion(ExplosionType.Monster);


            Destroy(gameObject);
        }
    }
}
