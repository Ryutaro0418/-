using System.Collections;
using UnityEngine;

public class TargetLauncher : MonoBehaviour
{
    [Header("�^�[�Q�b�g�̐ݒ�")]
    public GameObject targetPrefab;         // ���˂���^�[�Q�b�g�̃v���n�u
    public Transform launchPoint;           // ���ˈʒu

    [Header("���ːݒ�")]
    public int numberOfTargets = 15;        // ���˂��鐔
    public float launchInterval = 1f;       // �e���˂̊Ԋu�i�b�j
    public float launchForce = 50f;         // �e�ɉ������

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
