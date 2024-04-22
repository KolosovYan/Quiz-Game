using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void LoadSceneByName(string sceneName) => SceneManager.LoadScene(sceneName);
}
