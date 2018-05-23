using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A script that handles the checking if the player has won and returns to the main menu
 */
public class WinScript : MonoBehaviour
{

    [SerializeField] List<Rigidbody> enemies;

    [SerializeField] GameObject winPackage;

    void Start()
    {
        StartCoroutine(CheckWin());
    }

    public void Kill(Rigidbody enemy)
    {
        enemies.Remove(enemy);
    }

    void Win()
    {
        Instantiate(winPackage, Vector3.zero, Quaternion.identity);
    }

    IEnumerator CheckWin()
    {
        while (true)
        {

            if (enemies.Count <= 0)
            {
                Win();
                break;
            }

            yield return new WaitForSeconds(2f);
        }
    }

}
