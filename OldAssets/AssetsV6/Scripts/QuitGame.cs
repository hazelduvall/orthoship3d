using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            quit();
        }
    }

    public void quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        if(Application.isWebPlayer)
        {
            Application.OpenURL("https://jediguy13.github.io/orthoship3d/");
        }
    }
}
