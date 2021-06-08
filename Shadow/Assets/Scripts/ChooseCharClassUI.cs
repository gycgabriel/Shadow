using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseCharClassUI : MonoBehaviour
{
    public GameObject guardianPrefab;

    public void chooseGuardian()
    {
        GameObject player = Instantiate(guardianPrefab, new Vector3(-10, 3, 0), Quaternion.identity);
        player.GetComponent<Player>().chooseCharClass("Guardian");
        bringToScene("hometown");
    }

    public void bringToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
