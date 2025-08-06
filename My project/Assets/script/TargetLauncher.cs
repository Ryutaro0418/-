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
    public float launchForce = 10f;

    [Header("連続発射設定")]
    public int launchCount = 15;
    public float launchInterval = 3f; // 秒間隔

    private bool isLaunching = false;

    void Update()
    {
        // スペースキーで発射開始
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

    public void Launch()
    {
        if (targetPrefab == null) return;

        // 発射方向を決定
        Vector3 dir = Vector3.up;

        switch (direction)
        {
            case LaunchDirection.Up: dir = Vector3.up; break;
            case LaunchDirection.Down: dir = Vector3.down; break;
            case LaunchDirection.Left: dir = Vector3.left; break;
            case LaunchDirection.Right: dir = Vector3.right; break;
        }

        // 的を生成して発射
        GameObject target = Instantiate(targetPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(dir * launchForce, ForceMode.Impulse);
        }
    }
}
