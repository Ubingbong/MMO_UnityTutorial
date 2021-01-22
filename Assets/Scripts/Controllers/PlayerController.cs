using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 20.0f;

    bool _moveToDest = false;
    Vector3 _destPos;

    void Start()
    {
        // 키보드 이벤트에 따른 동작 초기화
        Managers.Input.KeyAction -= OnKeyboard;
        // 키보드 이벤트에 따른 동작 추가
        Managers.Input.KeyAction += OnKeyboard;

        // 마우스 클릭에 따른 동작 초기화
        Managers.Input.MouseAction -= OnMouseClicked;
        // 마우스 클릭에 따른 동작 추가
        Managers.Input.MouseAction += OnMouseClicked;
    }
    void Update()
    {
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;

            if (dir.magnitude < 0.0001f) // 목적지에 도착하면
            {
                _moveToDest = false;
            }
            else
            {
                // 목적지 근처에서 플레이어가 부들거리는 현상 해결
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);

                // 캐릭터가 목적지를 바라보며 목적지로 이동
                transform.position += dir.normalized * moveDist;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
        }
    }

    void OnKeyboard()
    {
        // 캐릭터 이동
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);  // world 좌표계 기준으로 북쪽을 처다봄
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);  // world 좌표계 기준으로 남쪽을 처다봄
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f); ;  // world 좌표계 기준으로 동쪽을 처다봄
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);  // world 좌표계 기준으로 서쪽을 처다봄
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        //_moveToDest = false;

    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * Camera.main.farClipPlane, Color.red, 1.0f);

        // 벽 레이어만 사용
        LayerMask layermask = LayerMask.GetMask("Wall");

        // 카메라에서 카메라-> mousePos 방향으로 광선을 쏘아 맞는 물체의 이름 로그로 출력
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, layermask))
        {
            // ray에 부딪힌 위치를 목적지로 설정
            _destPos = hit.point;
            _moveToDest = true;
        }
    }

}