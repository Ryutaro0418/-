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

    [Header("���ːݒ�")]
    public LaunchDirection direction = LaunchDirection.Up;
    public GameObject targetPrefab;
    public float launchForce = 10f;

    [Header("�A�����ːݒ�")]
    public int launchCount = 15;
    public float launchInterval = 3f; // �b�Ԋu

    private bool isLaunching = false;

    void Update()
    {
        // �X�y�[�X�L�[�Ŕ��ˊJ�n
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

        // ���˕���������
        Vector3 dir = Vector3.up;

        switch (direction)
        {
            case LaunchDirection.Up: dir = Vector3.up; break;
            case LaunchDirection.Down: dir = Vector3.down; break;
            case LaunchDirection.Left: dir = Vector3.left; break;
            case LaunchDirection.Right: dir = Vector3.right; break;
        }

        // �I�𐶐����Ĕ���
        GameObject target = Instantiate(targetPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(dir * launchForce, ForceMode.Impulse);
        }
    }
}
