using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundController : MonoBehaviour
{   
     public GameObject BoundPref,CurrentBound;
     GameObject LastBound,NextBound;
     public Transform Player;
     int x = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        float  m_ColliderSize = CurrentBound.transform.GetChild(0).GetComponent<BoxCollider2D>().size.y;
        if(Player.transform.position.y >= m_ColliderSize && x == 0)
        {   
            Vector3 BoundPos =new Vector3 (0f,m_ColliderSize+CurrentBound.transform.position.y,0f);
            NextBound =  Instantiate(BoundPref,BoundPos,Quaternion.identity);
            x++;
        }
        if(Player.transform.position.y > m_ColliderSize+9f)
        {   
            Debug.Log("Changed");
            LastBound = CurrentBound;
            CurrentBound = NextBound;
            x--;
        }
        
    }
}
