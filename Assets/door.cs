using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class door : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)


    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("TitleScene"); // "NextScene"을 실제 다음 씬의 이름으로 변경하세요.

        }
    }


}