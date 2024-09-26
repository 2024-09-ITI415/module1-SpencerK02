using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Set in Inspector")]
   //prefab for instantiating apples
   public GameObject applePrefab;
   
   //speed at which the apple moves
   public float speed = 1f;

   //distance where AppleTree turns around
   public float leftAndRightEdge = 10f;

   //chance that the AppleTree will change directions
   public float chanceToChangeDirections = 0.1f;

   //rate at whuch Apples will be instantiated
   public float appleDropDelay = 1f;
   
    
    void Start()
    {
        //Dropping apples every second
        Invoke("DropApple", 2f);
    }

    void DropApple(){
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", appleDropDelay);
    }
    
    void Update()
    {
        //Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        //Change Direction
        if (pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed);  //move right
        } 
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed);  //move left
        }
       // else if (Random.value < chanceToChangeDirections){
       //     speed *= -1; //change direction
        }
    
    void FixedUpdate(){
        if (Random.value < chanceToChangeDirections){
            speed *= -1;
        }
}
}
