using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int speed;
    float xaccel = 0.0f;
    float zaccel = 0.0f;
    public float accel;
    float timer = 0.0f;
    public float maxTime;
    public float sizeIncrease;
    public float maxSize;
    public float sizeDecrease;
    public float minSize;
    GameObject previousWall = null;
    public List<GameObject> obstacleList;
    public GameObject cam;
    public GameObject winMenu;
    //public GameObject particleMaker;
    // Start is called before the first frame update
    void Start()
    {
        winMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, speed/2);
            xaccel = 0.0f;
            zaccel = accel;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //this.GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0, 0);
            this.GetComponent<Rigidbody>().velocity = new Vector3(-speed/2, 0.0f, 0.0f);
            xaccel = -accel;
            zaccel = 0.0f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);
            this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, -speed/2);
            xaccel = 0.0f;
            zaccel = -accel;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //this.GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
            this.GetComponent<Rigidbody>().velocity = new Vector3(speed/2, 0.0f, 0.0f);
            xaccel = accel;
            zaccel = 0.0f;
        }
        bool paused = cam.GetComponent<Pause>().isPaused;
        if(!paused){
        timer -= Time.deltaTime;
        }
    }

    void FixedUpdate(){
        this.GetComponent<Rigidbody>().velocity += new Vector3(xaccel, 0.0f, zaccel);
    }

    void OnCollisionEnter(Collision col)
    {
        //shake & shrink if wall, maybe particles
        //if size is <= x then explode and win
        if(col.gameObject.tag == "Wall" && col.gameObject != previousWall){
            previousWall = col.gameObject;
            this.gameObject.transform.localScale -= new Vector3(sizeDecrease, sizeDecrease, sizeDecrease);
            foreach(GameObject obj in obstacleList){
                obj.GetComponent<ObstacleController>().Fling();
            }
                        col.gameObject.GetComponent<AudioSource>().Play();
            if(this.gameObject.transform.localScale.x < minSize){
            this.gameObject.transform.localScale = new Vector3(minSize, minSize, minSize);
            ParticleSystem part = GetComponent<ParticleSystem>();
            cam.GetComponent<screenShake>().Stop(2.0f, 0.25f);
            GetComponent<AudioSource>().Play();
            GetComponent<exploder>().PlayAndDestroy(part);
            winMenu.SetActive(true);
            //explosion
        }
        else{
            cam.GetComponent<screenShake>().Stop(0.2f, 0.05f);
            cam.GetComponent<screenShake>().start = true;
        }
            
            //screen shake
        }
            xaccel = 0.0f;
            zaccel = 0.0f;
        if(col.gameObject.tag == "Obstacle"){
            //ignore collision
        }
    }
    void OnTriggerEnter(Collider col)
    {
        //particles & grow if obstacle
        //hit stop?
        //if size is >= x then dont change
        if(col.gameObject.tag == "Obstacle" && timer <= 0.0f){
            this.gameObject.transform.localScale += new Vector3(sizeIncrease, 0.0f, sizeIncrease);
            timer = maxTime;
            cam.GetComponent<screenShake>().Stop(0.2f, 0.0f);
            col.gameObject.GetComponent<AudioSource>().Play();
            //particleMaker.GetComponent<ParticleSystem>().Play();
        }
        if(this.gameObject.transform.localScale.x > maxSize){
            this.gameObject.transform.localScale = new Vector3(maxSize, 1.0f, maxSize);
        }
    }

    
}