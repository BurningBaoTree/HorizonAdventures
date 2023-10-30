using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBase : MonoBehaviour
{
    TestInputActions inputActions;

    protected virtual void Awake()
    {
        inputActions = new TestInputActions();
    }

    protected virtual void OnEnable()
    {
        inputActions.Test.Enable();
        inputActions.Test.Test1.performed += Test1;
        inputActions.Test.Test2.performed += Test2;
        inputActions.Test.Test3.performed += Test3;
        inputActions.Test.Test4.performed += Test4;
        inputActions.Test.Test5.performed += Test5;
        inputActions.Test.Test6.performed += Test6;
        inputActions.Test.Test7.performed += Test7;
        inputActions.Test.Test8.performed += Test8;
        inputActions.Test.Test9.performed += Test9;
        inputActions.Test.Test10.performed += Test0;
        inputActions.Test.TestClick.performed += TestClick;
    }

    protected virtual void OnDisable()
    {
        inputActions.Test.TestClick.performed += TestClick;
        inputActions.Test.Test10.performed += Test0;
        inputActions.Test.Test9.performed += Test9;
        inputActions.Test.Test8.performed += Test8;
        inputActions.Test.Test7.performed += Test7;
        inputActions.Test.Test6.performed += Test6;
        inputActions.Test.Test5.performed += Test5;
        inputActions.Test.Test4.performed += Test4;
        inputActions.Test.Test3.performed += Test3;
        inputActions.Test.Test2.performed += Test2;
        inputActions.Test.Test1.performed += Test1;
        inputActions.Test.Disable();
    }

    /// <summary>
    /// 1번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test1(InputAction.CallbackContext context)
    {
        
    }

    /// <summary>
    /// 2번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test2(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 3번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test3(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 4번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test4(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 5번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test5(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 6번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test6(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 7번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test7(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 8번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test8(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 9번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test9(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 0번 누르면 실행할 함수
    /// </summary>
    protected virtual void Test0(InputAction.CallbackContext context)
    {

    }

    /// <summary>
    /// 마우스 클릭을 하면 실행할 함수
    /// </summary>
    protected virtual void TestClick(InputAction.CallbackContext context)
    {

    }
}
