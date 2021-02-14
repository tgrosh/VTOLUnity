using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Explodable: MonoBehaviour {
    public Explosion explosionPrefab;
    public float explosionForce;
    public float explosionRadius;

    protected bool exploded;
        
    public virtual void Explode()
    {
        GameObject obj = Instantiate(explosionPrefab.gameObject, transform.position, Quaternion.identity);
        Explosion objExplosion = obj.GetComponent<Explosion>();

        objExplosion.Explode(explosionForce, explosionRadius);
        Destroy(obj, objExplosion.duration);
        exploded = true;
    }
}
