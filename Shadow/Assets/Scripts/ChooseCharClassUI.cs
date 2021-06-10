using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseCharClassUI : MonoBehaviour
{
    public GameObject guardianPrefab;
    public GameObject sorcererPrefab;

    public void chooseGuardian()
    {
        GameObject player = Instantiate(guardianPrefab, new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
        player.GetComponent<Player>().chooseCharClass("Guardian");
        bringToScene("hometown");
    }
    public void chooseSorcerer()
    {
        GameObject player = Instantiate(sorcererPrefab, new Vector3(-10.5f, 3.5f, 0f), Quaternion.identity);
        player.GetComponent<Player>().chooseCharClass("Sorcerer");
        bringToScene("hometown");
    }

    public void bringToScene(string name)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }
}
