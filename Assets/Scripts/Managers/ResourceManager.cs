using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // 프리팹 리소스 파일에서 가져오기
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    // 프리팹 인스턴스 생성하기
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // Load를 통해 프리팹을 가져온다.
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        // 만약 프리팹 가져오는 것을 실패하면 디버그 로그를 남겨준다.
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        // 프리팹의 인스턴스를 생성해 반환한다.
        return Object.Instantiate(prefab, parent);
    }

    // 게임 오브젝트 삭제하기
    public void Destoroy(GameObject go, float time = 0.0f)
    {
        if (go == null)
            return;

        Object.Destroy(go, time);
        return;
    }
}