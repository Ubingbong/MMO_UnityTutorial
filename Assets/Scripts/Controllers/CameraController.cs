using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);

    [SerializeField]
    GameObject _player = null;

    void Start()
    {

    }

    void LateUpdate()
    {
        // 쿼터뷰 모드일 때 _delta의 간격으로 항상 플레이어를 따라다니며 플레이어의 위치를 향함
        if (_mode == Define.CameraMode.QuarterView)
        {
            RaycastHit hitInfo;
            //Debug.DrawRay(_player.transform.position, _delta, Color.red, _delta.magnitude);
            if (Physics.Raycast(_player.transform.position, _delta, out hitInfo, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hitInfo.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView;
        _delta = delta;
    }

}