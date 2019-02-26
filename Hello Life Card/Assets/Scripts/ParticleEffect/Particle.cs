using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 rotateSpeed;
    public float baseWidth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity;
        transform.Rotate(rotateSpeed);
    }

    public void Move(Vector3 velocity, Vector3 rotateSpeed, float lifeTime)
    {
        //vlocity and size shoule be scale by windows
        float scale = Screen.width / baseWidth;
        transform.localScale *= scale;
        this.velocity = velocity * scale;
        this.rotateSpeed = rotateSpeed;
        StartCoroutine(Die(lifeTime, 0.02f));
    }

    //become smaller and destroy itself after life time
    public IEnumerator Die(float totalLifeTime, float timeInterval)
    {
        float totalX = transform.localScale.x;
        float totalY = transform.localScale.y;
        float xInterval = totalX * timeInterval/totalLifeTime;
        float yInterval = totalY * timeInterval/totalLifeTime;
        float x = 0;
        while(x < totalLifeTime)
        {
            x += timeInterval;
            transform.localScale -= new Vector3(xInterval, yInterval, 0);
            yield return new WaitForSeconds(timeInterval);
        }
        Destroy(gameObject);
    }

}
