using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
public class BulletControl : MonoBehaviour
{
    Rigidbody2D _rgbd2D;    //프리팹의 rigidbody
    [SerializeField] float force = 150f;    //사격 관련 변수                      ------1
    public void Shoot()
    {
        _rgbd2D = GetComponent<Rigidbody2D>();  //참조
        _rgbd2D.AddForce(-transform.right * force); //사격 rigidbody 물리 적용    ------1
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))   //충돌한 collision의 게임오브젝트 태그가 "Wall" 이라면
        {
            GameObject effect = Resources.Load("Prefabs/Effects/Explosion") as GameObject;  //파일 접근 GameObject 참조  ---2
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);   //참조한 GObj 생성 및 참조   ---2, 3
            Animator aniControl = go.GetComponent<Animator>();  //GObj의 Animator 컴포넌트 참조                          ---3  
            ExplosionObject obj = go.GetComponent<ExplosionObject>();      //GObj
            obj.Explosion(ExplosionType.NonBreake);
            Destroy(gameObject);
        }
    }
}
