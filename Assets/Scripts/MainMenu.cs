using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public SceneFader sceneFader;
    public void gotoScene(string str)
    {
        sceneFader.FadeTo(str);
    }
    public void gotoSceneWithIndex(string str)
    {
        if (SetAllFile.staticCurrentItem > SetAllFile.indexSelected)
            sceneFader.FadeTo(str);
    }
}
