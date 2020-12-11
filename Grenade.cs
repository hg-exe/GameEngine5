using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        float explosionRadius = 2000.0f;

        Collider[] objects = UnityEngine.Physics.OverlapSphere(gameObject.transform.position, explosionRadius);
        foreach (Collider h in objects)
        {
            Rigidbody r = h.GetComponent<Rigidbody>();
            if (r != null)
            {
                r.AddExplosionForce(2000.0f, gameObject.transform.position, explosionRadius);
            }
        }

        Destroy(gameObject);

    }
}
