using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int healthCap = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;

	bool hasFullhealth = true;
    float flashSpeed = 5f;
    Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = healthCap;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

		healthSlider.value = currentHealth;
    }

	/// <summary>
	/// chekcs for full health
	/// </summary>
	/// <returns><c>true</c>, if capacity is full, <c>false</c> otherwise.</returns>
	public bool HealthCapacityCheck()
	{
		if (currentHealth >= healthCap) {
			hasFullhealth = true;
			currentHealth = healthCap;
		} else {
			hasFullhealth = false;
		}
			
		return hasFullhealth;
	}

	/// <summary>
	/// Take damage and substract it from the current health
	/// </summary>
	/// <param name="amount">Amount.</param>
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }
}
