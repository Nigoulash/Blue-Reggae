using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{
   
   public float changeTime = 5f;
   public string sceneName = "LinneJacobScene";

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
if(changeTime <- 0)
{SceneManager.LoadScene(sceneName);

}
    }
}
