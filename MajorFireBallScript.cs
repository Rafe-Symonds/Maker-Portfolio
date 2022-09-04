using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorFireBallScript : MonoBehaviour
{
    public GameObject fireBall;
    // Start is called before the first frame update
    void Start()
    {
        GameObject fireBall1 = Instantiate(fireBall, new Vector3(-11f, 3f, 0), Quaternion.identity);
        GameObject fireBall2 = Instantiate(fireBall, new Vector3(-12f, 4.5f, 0), Quaternion.identity);
        GameObject fireBall3 = Instantiate(fireBall, new Vector3(-13f, 6f, 0), Quaternion.identity);
        if(gameObject.tag == "team2")
        {
            fireBall1.transform.position = new Vector3(11f, 3f, 0);
            fireBall2.transform.position = new Vector3(12f, 4.5f, 0);
            fireBall3.transform.position = new Vector3(13f, 6f, 0);
        }
        Vector3 fireBall2Target = new Vector3(gameObject.transform.position.x - 1.5f, gameObject.transform.position.y, 0);
        Vector3 fireBall3Target = new Vector3(gameObject.transform.position.x + 1.5f, gameObject.transform.position.y, 0);
        fireBall1.GetComponent<Projectile>().MoveProjectile(gameObject);
        fireBall2.GetComponent<Projectile>().MoveProjectile(fireBall2Target);
        fireBall3.GetComponent<Projectile>().MoveProjectile(fireBall3Target);
        fireBall1.tag = gameObject.tag;
        fireBall2.tag = gameObject.tag;
        fireBall3.tag = gameObject.tag;
        Destroy(gameObject, 1f);
    }

   
}
