using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 0;

    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera = null;
    [SerializeField]
    private GameObject player = null;
    [SerializeField]
    private Transform startPos = null;
    [SerializeField]
    private Transform door_1 = null;
    [SerializeField]
    private Text liveText = null;

    private void Start()
    {
        if(liveText != null)
        {
            liveText.text += lives.ToString();
        }
    }

    public void Teleport()
    {
      Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if(player != null)
        player.position = door_1.position;
    }

    public void StartAgain()
    {
        
        lives--;
        liveText.text = "Lives : " + lives.ToString();
        GameObject Player = Instantiate(player, startPos.position, Quaternion.identity) as GameObject;
        cinemachineVirtualCamera.Follow = Player.transform;

       SkeletonBAttack[] skeletonBAttacks = FindObjectsOfType<SkeletonBAttack>();
        
        foreach(SkeletonBAttack skeletonBAttack in skeletonBAttacks)
        {
            skeletonBAttack.SetTarget(Player.transform);
            skeletonBAttack.GetComponentInChildren<AnimationController>().SetTarget(Player.transform);
        }

        ArcherAttack[] archerAttacks = FindObjectsOfType<ArcherAttack>();

        foreach(ArcherAttack archerAttack in archerAttacks)
        {
            archerAttack.SetTarget(Player.transform);
        }

        if (lives == 0)
        {
            //Load End Scene
            SceneManager.LoadScene(2);
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
}
