using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackInTheBox : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startpos;
    private Vector3 nextpos;

    void Start()
    {
        nextpos = startpos.position;
        Debug.Log("salgo");
    }
    void Update()
    {
        if(transform.position != nextpos)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextpos, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage (1);
        }
    }

    public IEnumerator ActiveTrap()
    {
        yield return new WaitForSeconds(0.5f);
        nextpos = pos2.position;
        yield return new WaitUntil(()=> transform.position == nextpos);
        yield return new WaitForSeconds (0.2f);
        nextpos = pos1.position;
    }

}
