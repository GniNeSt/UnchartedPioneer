using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] float _followSpeed = 6f;
    Transform _player;


    [SerializeField] Rect _mapRange;//아래 변수들 Rect 적용해서 수정하기!!!

    [SerializeField]Transform _centerTf;
    [SerializeField]Vector2 _clapmRange;
    [SerializeField]Vector2 _clampMaxPoint, _clampMinPoint;
    private void Awake()
    {
        _clampMaxPoint = new Vector2(_centerTf.position.x + _clapmRange.x, _centerTf.position.y + _clapmRange.y);
        _clampMinPoint = new Vector2(_centerTf.position.x - _clapmRange.x, _centerTf.position.y - _clapmRange.y);
    }
    private void Update()
    {
        if( _player != null)
        {
            //transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
            Vector3 target = new Vector3(_player.position.x, _player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, _followSpeed*Time.deltaTime);

            if(transform.position.x >= _clampMaxPoint.x) transform.position = new Vector3(_clampMaxPoint.x , transform.position.y, transform.position.z);
            else if (transform.position.x <= _clampMinPoint.x) transform.position = new Vector3(_clampMinPoint.x, transform.position.y, transform.position.z);
            if (transform.position.y >= _clampMaxPoint.y) transform.position = new Vector3(transform.position.x, _clampMaxPoint.y, transform.position.z);
            else if (transform.position.y <= _clampMinPoint.y) transform.position = new Vector3(transform.position.x, _clampMinPoint.y, transform.position.z);

        }

    }
    public void StartFollow(Transform pc)
    {
        _player = pc;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_centerTf.position, (Vector3)_clapmRange*2);
    }
}
