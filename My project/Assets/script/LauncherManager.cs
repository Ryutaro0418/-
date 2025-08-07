using UnityEngine;

public class LauncherManager : MonoBehaviour
{
    public TargetLauncher[] launchers;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var launcher in launchers)
            {
                launcher.StartLaunching(); // Å© í«â¡Ç≈åƒÇ◊ÇÈÇÊÇ§Ç…Ç∑ÇÈ
            }
        }
    }
}
