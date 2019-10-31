using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] ParticleSystem collectedParticleEffect;
    [SerializeField] float powerDownTime = 2.0f;
    [SerializeField] int scoreValue = 0;
    [SerializeField] GameObject destroyParticle;
    [SerializeField] AudioClip collectedSound;
    [SerializeField] bool isArtifact;
    
    private ScoreDisplay scoreDisplay;

    void Awake()
    {
        scoreDisplay = GameObject.Find("Score Display").GetComponent<ScoreDisplay>();
        audioSource = GetComponent<AudioSource>();
        //scoreScreen = GameObject.Find("Score Screen").GetComponent<ScoreScreen>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other != null)
            {
                CollisionBBevahiour();
                StartCoroutine(DestroyParticle());
            }
        }
    }

    private void CollisionBBevahiour()
    {
        audioSource.PlayOneShot(collectedSound);
        scoreDisplay.UpdateScore(scoreValue);

        if (isArtifact)
        {
            scoreDisplay.UpdateArtifactScore(1);
            scoreDisplay.artifactPoints += 1500;
        }
        else
        {
            scoreDisplay.UpdateCollectablesScore(1);
        }
        //scoreDisplay.UpdateCollectablesScore(1);
        destroyParticle.SetActive(false);
        collectedParticleEffect.Play();
    }

    public IEnumerator DestroyParticle()
    {
        GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(powerDownTime);
        Destroy(gameObject);
;    }

}
