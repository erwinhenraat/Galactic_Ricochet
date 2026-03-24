using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    public void SceneChanger()
    {
        SceneManager.LoadScene((int)Scenes.Main);
    }

}
