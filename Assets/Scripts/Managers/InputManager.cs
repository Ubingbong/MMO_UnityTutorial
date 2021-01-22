using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    public void OnUpdate()
    {
        // 어떤 입력이라도 발생하고 키 입력이 있으면
        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButtonDown(0)) // 마우스를 누르고 있는 상황
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else // 마우스 버튼을 눌렀다 뗀 상황
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }

}
