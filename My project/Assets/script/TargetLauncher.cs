using System.Collections;
using UnityEngine;

public class TargetLauncher : MonoBehaviour
{
    [Header("ターゲットの設定")]
    public GameObject targetPrefab;         // 発射するターゲットのプレハブ
    public Transform launchPoint;           // 発射位置

    [Header("発射設定")]
    public int numberOfTargets = 15;        // 発射する数
    public float launchInterval = 1f;       // 各発射の間隔（秒）
    public float launchForce = 50f;         // 弾に加える力

    private bool hasLaunched = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !hasLaunched)
        {
            hasLaunched = true;
            StartCoroutine(LaunchTargets());
        }
    }

    IEnumerator LaunchTargets()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            GameObject target = Instantiate(targetPrefab, launchPoint.position, launchPoint.rotation);
            Rigidbody rb = target.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(launchPoint.forward * launchForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(launchInterval);
        }
    }
}
