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

    private void Awake()
    {
        _collider2D = GetComponent<CircleCollider2D>();
        _collider2D.radius = _radius;
        _isWarning = false;
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
                // È°¼ºÈ­
                fac.InitData(collision.GetComponent<PlayerControl>());
            }
        }
        _collider2D.enabled = false;
        _isWarning = true;
        IngameManager._Instance.OpenInfoMsgBox("Warning!!!", Color.red, MessageType.Blink, 4.0f);
    }
}
