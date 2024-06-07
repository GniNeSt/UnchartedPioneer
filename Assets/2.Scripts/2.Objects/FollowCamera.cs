using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] float _followSpeed = 6f;
    Transform _player;

    bool _isFollow;
    Vector3 _goalPos;

    [SerializeField] Rect _mapRange;//아래 변수들 Rect 적용해서 수정하기!!!

    [SerializeField] Transform _centerTf;
    [SerializeField] Vector2 _clapmRange;
    [SerializeField] Vector2 _clampMaxPoint, _clampMinPoint;
    private void Awake()
    {
        _clampMaxPoint = new Vector2(_centerTf.position.x + _clapmRange.x, _centerTf.position.y + _clapmRange.y);
        _clampMinPoint = new Vector2(_centerTf.position.x - _clapmRange.x, _centerTf.position.y - _clapmRange.y);
    }
    private void Update()
    {
        if (_player != null)
        {
            if (_isFollow)
            {
                //transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
                Vector3 target = new Vector3(_player.position.x, _player.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, target, _followSpeed * Time.deltaTime);

                if (transform.position.x >= _clampMaxPoint.x) transform.position = new Vector3(_clampMaxPoint.x, transform.position.y, transform.position.z);
                else if (transform.position.x <= _clampMinPoint.x) transform.position = new Vector3(_clampMinPoint.x, transform.position.y, transform.position.z);
                if (transform.position.y >= _clampMaxPoint.y) transform.position = new Vector3(transform.position.x, _clampMaxPoint.y, transform.position.z);
                else if (transform.position.y <= _clampMinPoint.y) transform.position = new Vector3(transform.position.x, _clampMinPoint.y, transform.position.z);

            }
            else
            {

                transform.position = Vector3.Lerp(transform.position, _goalPos, _followSpeed * Time.deltaTime * 2);

                if (transform.position.x >= _clampMaxPoint.x) transform.position = new Vector3(_clampMaxPoint.x, transform.position.y, transform.position.z);
                else if (transform.position.x <= _clampMinPoint.x) transform.position = new Vector3(_clampMinPoint.x, transform.position.y, transform.position.z);
                if (transform.position.y >= _clampMaxPoint.y) transform.position = new Vector3(transform.position.x, _clampMaxPoint.y, transform.position.z);
                else if (transform.position.y <= _clampMinPoint.y) transform.position = new Vector3(transform.position.x, _clampMinPoint.y, transform.position.z);

                if(Vector3.Distance(transform.position, _goalPos) <= 0.1f)
                {
                    transform.position = _goalPos;
                    _isFollow = true;
                    IngameManager._Instance.StatePlay();
                }
            }
        }
    }
    public void StartFollow(Transform pc)
    {
        _isFollow = false;
        _player = pc;
        _goalPos = new Vector3(pc.position.x, pc.position.y, transform.position.z);
    }
    public void InitPosCam(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_centerTf.position, (Vector3)_clapmRange * 2);
    }
}
