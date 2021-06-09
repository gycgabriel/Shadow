using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBehaviour : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void load(int saveNum)
    {
        PlayerData data = SaveSystem.loadPlayer(saveNum);

        player.stats = data.stats;
        player.skills = data.skills;
        player.statModifiers = data.statModifiers;
        player.currentHP = data.currentHP;
        player.currentMP = data.currentMP;
        player.currentLevel = data.currentLevel;
        player.isDead = data.isDead;
        player.charclass = data.charclass;
        player.currentExp = data.currentExp;
        player.expToNextLevel = data.expToNextLevel;


        SceneManager.LoadScene(data.sceneName);
        this.player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
}
