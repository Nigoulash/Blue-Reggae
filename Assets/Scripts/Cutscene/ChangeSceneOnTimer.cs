using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{
   
   [SerializeField] float changeTime = 5f;
   [SerializeField] string sceneName = "LinneJacobScene";

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <- 0)
        {
            SceneManager.LoadScene(sceneName);

        }
    }
}
