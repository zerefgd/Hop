using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField]
    private Mesh diamond;

    [SerializeField]
    private Material diamondMaterial;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void SetAsDiamond()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystemRenderer psr = GetComponent<ParticleSystemRenderer>();
        ps.startSize = 25f;
        psr.mesh = diamond;
        psr.material = diamondMaterial;
    }
}
