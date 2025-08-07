using UnityEngine;
using System.Collections;

public class TargetLauncher : MonoBehaviour
{
    public enum LaunchDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [Header("発射設定")]
    public LaunchDirection direction = LaunchDirection.Up;
    public GameObject targetPrefab;
    public Transform launchPoint;
    public float launchForce = 10f;

    [Header("連続発射設定")]
    public int launchCount = 15;
    public float launchInterval = 3f;

    [Header("アニメーション設定")]
    public AnimatorOverrideController animUp;
    public AnimatorOverrideController animDown;
    public AnimatorOverrideController animLeft;
    public AnimatorOverrideController animRight;

    private bool isLaunching = false;

    public void StartLaunching()
    {
        if (!isLaunching)
        {
            StartCoroutine(LaunchTargets());
        }
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isLaunching)
        {
            StartCoroutine(LaunchTargets());
        }
    }

    IEnumerator LaunchTargets()
    {
        isLaunching = true;

        for (int i = 0; i < launchCount; i++)
        {
            Launch();
            yield return new WaitForSeconds(launchInterval);
        }

        isLaunching = false;
    }

    void Launch()
    {
        if (targetPrefab == null || launchPoint == null)
        {
            Debug.LogWarning("Prefab または LaunchPoint が設定されていません。");
            return;
        }

        GameObject target = Instantiate(targetPrefab, launchPoint.position, launchPoint.rotation);

        // Animator の取得とアニメーションコントローラーの割り当て
        Animator animator = target.GetComponent<Animator>();
        if (animator != null)
        {
            Debug.Log("Animator found on target. Direction = " + direction);

            switch (direction)
            {
                case LaunchDirection.Up:
                    animator.runtimeAnimatorController = animUp;
                    Debug.Log("animUp controller assigned");
                    break;
                case LaunchDirection.Down:
                    animator.runtimeAnimatorController = animDown;
                    Debug.Log("animDown controller assigned");
                    break;
                case LaunchDirection.Left:
                    animator.runtimeAnimatorController = animLeft;
                    Debug.Log("animLeft controller assigned");
                    break;
                case LaunchDirection.Right:
                    animator.runtimeAnimatorController = animRight;
                    Debug.Log("animRight controller assigned");
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Animator component not found on the instantiated target.");
        }

        // 発射方向に力を加える
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(launchPoint.forward * launchForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Rigidbody not found on the instantiated target.");
        }
    }
}
