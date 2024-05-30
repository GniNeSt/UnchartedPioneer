using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefineEnums;
public class ExplosionObject : MonoBehaviour
{
    Animator _aniController;
    [SerializeField]int _type = 0;
    private void Awake()
    {
        _aniController = GetComponent<Animator>();
    }
    public void Explosion(ExplosionType animationType)
    {
        _aniController.SetInteger("Type", ((int)animationType));
        _aniController.SetTrigger("Changed");

        Destroy(gameObject,1);
    }

}
