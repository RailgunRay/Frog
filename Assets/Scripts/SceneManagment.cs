using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        StopAllCoroutines();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
