using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ButtonPressed(string sceneName) => SceneLoader.LoadSceneByName(sceneName);
}
