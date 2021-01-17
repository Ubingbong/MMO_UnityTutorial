using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // static 변수 : 유일성이 보장된다.
    public static Managers Instance { get { Init(); return s_instance; } } // 누군가 Manager를 사용하고 싶을 때 사용. 유일한 매니저를 가져온다.

    // Start is called before the first frame update
    void Start()
    {
        //instance 초기화
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    static void Init()
    {
        if (s_instance == null)
        {
            // Managers를 컴포넌트로 가지고 있는 유일한 GameObject가 있는지 확인
            GameObject go = GameObject.Find("@Managers");

            // 만약 없다면
            if (go == null)
            {
                // Managers를 컴포넌트로 가질 GameObject를 생성
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            // Managers를 Component로 갖는 유일한 객체인 go는 사라지면 안되는 중요한 객체이므로
            DontDestroyOnLoad(go);

            //instance 초기화
            s_instance = go.GetComponent<Managers>();
        }
    }
}
