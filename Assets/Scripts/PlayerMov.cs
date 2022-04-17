using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{   
    public Vector2 MaxPower;
    public Vector2 MinPower;
    public float Speed = 10f;
    Rigidbody2D rb;
    Camera cam;
    Vector2 Force;
    Vector3 StartPos;
    Vector3 EndPos;
    Vector3 LastVel;
    public float SlowDownFactor = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
         
        if(StartPos.x-EndPos.x > 0)
            {
              transform.localRotation = Quaternion.Euler(0f,0f,0f);
            }
            if(StartPos.x - EndPos.x < 0)
            {
              transform.localRotation = Quaternion.Euler(0f,180f,0f);
            }    
     Inputs();
    }
    
    void Inputs()
    {
       if(Input.GetMouseButtonDown(0))
        {   Time.timeScale = SlowDownFactor;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            
            StartPos =  cam.ScreenToWorldPoint(Input.mousePosition);
            StartPos.z = 0;
            
           
        }
          if(Input.GetMouseButtonUp(0))
        {   
           
            EndPos =  cam.ScreenToWorldPoint(Input.mousePosition);
            EndPos.z = 0;
            Force = new Vector2(Mathf.Clamp(StartPos.x  -EndPos.x ,MinPower.x , MaxPower.x) ,Mathf.Clamp(StartPos.y-EndPos.y,MinPower.y, MaxPower.y));
            rb.AddForce(Force*Speed,ForceMode2D.Impulse);
            
             LastVel = rb.velocity;
            Time.timeScale = 1f;
             Time.fixedDeltaTime = 0.01f;
       }

      
    }
    void FixedUpdate()
    {
       
       
    }
    void OnCollisionEnter2D(Collision2D col)
    {
       ContactPoint2D contact = col.contacts[0];
       if(col.gameObject.tag == "Wall")
       {
        var Direction = Vector3.Reflect(LastVel.normalized,contact.normal);
        rb.velocity = Direction*Mathf.Max(Speed*1.5f,0f);
       }
    }
}
