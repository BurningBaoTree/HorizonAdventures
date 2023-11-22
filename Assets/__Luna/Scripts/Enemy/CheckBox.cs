using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    /// <summary>
    /// CheckBox가 활동중인지 확인하는 변수
    /// </summary>
    private bool isActive = true;

    /// <summary>
    /// Enemy에서 CheckBox를 통해 확인할 델리게이트
    /// </summary>
    public System.Action onFind;

    // 컨포넌트
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.CompareTag("Player"))
            {
                isActive = false;
                StartCoroutine(ShowImage());
                onFind?.Invoke();
            }
        }
    }

    /// <summary>
    /// 플레이어를 찾으면 !를 잠시 보여주는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowImage()
    {
        canvasGroup.alpha = 1;

        yield return new WaitForSeconds(0.5f);

        canvasGroup.alpha = 0;
    }
}
