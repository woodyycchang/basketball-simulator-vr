using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNormalMode()
    {
        SceneManager.LoadScene("SampleScene");  // 改成你的普通模式场景名
    }

    public void LoadMagicMode()
    {
        SceneManager.LoadScene("MagicScene");  // 改成你的魔法模式场景名
    }
}
