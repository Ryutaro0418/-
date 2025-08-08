using UnityEngine;
using System.Collections;

public class TargetLauncher_TypeB : MonoBehaviour
{
    public enum LaunchDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    [Header("キー設定")]
    public KeyCode triggerKey = KeyCode.Space;

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
        if (Input.GetKeyDown(triggerKey) && !isLaunching)
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
            switch (direction)
            {
                case LaunchDirection.Up:
                    animator.runtimeAnimatorController = animUp;
                    break;
                case LaunchDirection.Down:
                    animator.runtimeAnimatorController = animDown;
                    break;
                case LaunchDirection.Left:
                    animator.runtimeAnimatorController = animLeft;
                    break;
                case LaunchDirection.Right:
                    animator.runtimeAnimatorController = animRight;
                    break;
            }
        }

        // 発射方向のベクトルを取得
        Vector3 forceDir = Vector3.up;
        switch (direction)
        {
            case LaunchDirection.Up: forceDir = Vector3.up; break;
            case LaunchDirection.Down: forceDir = Vector3.down; break;
            case LaunchDirection.Left: forceDir = Vector3.left; break;
            case LaunchDirection.Right: forceDir = Vector3.right; break;
        }

        // Rigidbody に力を加える
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(forceDir * launchForce, ForceMode.Impulse);
        }
    }
}
