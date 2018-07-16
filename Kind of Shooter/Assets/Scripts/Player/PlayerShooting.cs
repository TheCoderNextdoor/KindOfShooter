using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
	public Slider bulletSlider;
    public float range = 100f;
	public int magazineCapacity = 30;
	public float reloadTime = 1.5f;
	public AudioSource gunAudio;
	public AudioSource reloadAudio;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
	int shootCounter = 0;
	bool shootEnabled = true;
	bool isReloading = false;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunLight = GetComponent<Light> ();
    }

	public void UpdateCapacity(int amount){
		bulletSlider.maxValue += amount;
	}


    void Update ()
    {
        timer += Time.deltaTime;

		if (isReloading )
			Reload ();

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && shootEnabled)
        {
            Shoot ();
        }

		if(Input.GetKey ("r"))
		{
			isReloading = true;
			timer = 0f;
			shootEnabled = false;
			Reload ();
		}

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

	void Reload(){
		if(!reloadAudio.isPlaying)
			reloadAudio.Play ();
		
		if (timer >= reloadTime) {
			
			shootEnabled = true;
			bulletSlider.value = magazineCapacity;
			shootCounter = 0;
			Debug.Log ("reloading");
			isReloading = false;
		}
	}


    void Shoot ()
    {
		if (shootCounter <= magazineCapacity) {

			shootCounter += 1;
			timer = 0f;

			gunAudio.Play ();

			gunLight.enabled = true;

			gunParticles.Stop ();
			gunParticles.Play ();

			gunLine.enabled = true;
			gunLine.SetPosition (0, transform.position);

			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;

			//if raycast hits something, draw a line from origin to that point
			if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
				EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
				if (enemyHealth != null) {
					enemyHealth.TakeDamage (damagePerShot, shootHit.point);
				}
				//set the 2nd point of the line to where it hits the enemy
				gunLine.SetPosition (1, shootHit.point);
			} 
			//else draw a line of 100 units from origin towards the direction
			else {		
				gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
			}
			//update the slider to show the correct amount of bullets left
			bulletSlider.value = magazineCapacity - shootCounter;
		}

		else {
			timer = 0f;
			shootEnabled = false;
		}
    }
}
