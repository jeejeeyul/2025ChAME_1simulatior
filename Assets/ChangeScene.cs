using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
       public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
       
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
void OnCollisionEnter2D(Collision2D other) {
    SceneManager.LoadScene(sceneName);
}
}
