using System;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    public float SelfDestructDistance = 10; // Is overwritten in PlayerController.Shoot()
    private Vector3 StartPosition;
    private float damage = 1;
    private float speed = 3;
    
    public void Start()
    {
        this.StartPosition = this.transform.position;
    }

    public void Update()
    {
        if (Vector3.Distance(StartPosition, this.transform.position) >= SelfDestructDistance)
        {
            Destroy(this.gameObject);
        }

        this.transform.Translate(new Vector3(0, 12 * Time.deltaTime, 0) * this.speed, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(Enum.GetName(typeof(Tags), Tags.Enemy)))
        {
            collision.gameObject.SendMessage("Damage", damage);
            Destroy(this.gameObject);
        }
    }
}
