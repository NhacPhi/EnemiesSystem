using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hp;
    private int expDrop;
    private float maxSpeed;
    private float acceleration;
    private float speed;
    private float timeLine;

    [SerializeField] private EnemiesObject data;

    private bool facingRight;
    public Vector3 direction = Vector3.zero;
    private Vector3 target = Vector3.zero;
    private Vector3 aligment = Vector3.zero;
    private Vector3 separation = Vector3.zero;

    private float perceptionRadius = 1;
    void Start()
    {
        hp = data.hp;
        expDrop = data.expDrop;
        maxSpeed = data.maxSpeed;
        acceleration = data.acceleration;

        speed = 0;
        timeLine = 0;
        facingRight = true;
        perceptionRadius = 3f;
    }

    void Update()
    {
        EnemyMovement();
    }

    public void EnemyMovement()
    {
        if (speed < maxSpeed)
        {
            speed += acceleration * Time.deltaTime;
        }
        else
        {
            speed = maxSpeed;
        }

        bool flig = transform.position.x - target.x < 0;

        //if((flig && !facingRight)  || (!flig && facingRight))
        //{
        //    Vector3 currentScale = transform.localScale;
        //    currentScale.x *= -1;
        //    transform.localScale = currentScale;
        //    this.facingRight = !this.facingRight;
        //}
        Vector3 velocity = this.direction + this.aligment + this.separation;
        gameObject.transform.position += velocity.normalized * speed * Time.deltaTime ;
        this.aligment = Vector3.zero;
        this.direction = Vector3.zero;
        this.separation = Vector3.zero;
    }

    public void SetVelocityOfEnemy(Vector3 direction, Vector3 target)
    {
         this.direction = direction; 
         this.target = target;    
    }

    public void SetEnemiesData(int hp, int expDrop, float maxpSpeed, float acceleratio)
    {
        this.hp = hp;
        this.expDrop = expDrop;
        this.maxSpeed = maxpSpeed;
        this.acceleration = acceleratio;
    }
    public void Alignment(List<GameObject> enemies)
    {
        Vector3 avg = new Vector3();

        int total = 0;

        foreach (GameObject other in enemies)
        {
            Vector3 d = transform.position - other.transform.position;

            float distance = d.magnitude;

            if (other != this && distance < 1 && distance != 0)
            {
                avg += other.GetComponent<Enemy>().direction;
                total++;
            }
        }

        if (total > 0)
        {
            avg /= total;
            this.aligment = avg;
        }
    }

    public void Separation(List<GameObject> enemies)
    {
        Vector3 steering = new Vector3();
        Vector3 diff = new Vector3();

        int total = 0;

        foreach (GameObject other in enemies)
        {
            Vector3 d = transform.position - other.transform.position;

            float distance = d.magnitude;

            if (other != this && distance < perceptionRadius && distance != 0)
            {
                diff = gameObject.transform.position - other.transform.position;
                diff /= distance;
                steering += diff;
                total++;
            }
        }

        if (total > 0)
        {
            steering /= total;
           
            this.separation = steering;
        }
    }
}
