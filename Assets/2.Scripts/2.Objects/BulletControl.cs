using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
public class BulletControl : MonoBehaviour
{
    Rigidbody2D _rgbd2D;    //�������� rigidbody
    [SerializeField] float force = 150f;    //��� ���� ����                      ------1
    int _finishDamage;
    public void Shoot(int finish)
    {
        _rgbd2D = GetComponent<Rigidbody2D>();  //����
        _rgbd2D.AddForce(-transform.right * force); //��� rigidbody ���� ����    ------1
        _finishDamage = finish;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))   //�浹�� collision�� ���ӿ�����Ʈ �±װ� "Wall" �̶��
        {
            GameObject effect = Resources.Load("Prefabs/Effects/Explosion") as GameObject;  //���� ���� GameObject ����  ---2
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);   //������ GObj ���� �� ����   ---2, 3
            Animator aniControl = go.GetComponent<Animator>();  //GObj�� Animator ������Ʈ ����                          ---3  
            ExplosionObject obj = go.GetComponent<ExplosionObject>();      //GObj
            obj.Explosion(ExplosionType.NonBreake);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Monster"))
        {
            MonsterControl mc = collision.GetComponent<MonsterControl>();
            mc.OnHitting(_finishDamage);
        }
    }
}
