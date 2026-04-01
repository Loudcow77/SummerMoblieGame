using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyBlocks : MonoBehaviour
{
    public ParticleSystem particleSystem;

    // Destroys the blocks when the hit the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            FindObjectOfType<Score>().increaseScore();

            destroy();
        }
    }

    public void destroy()
    {
        Instantiate(particleSystem, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
