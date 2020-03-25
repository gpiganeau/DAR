using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableWeather : MonoBehaviour
{
    /* Ce script est à placer sur le préfab Snow_Particles_New. Il permet de simuler des tempêtes de neige temporaires */
    private ParticleSystem.VelocityOverLifetimeModule vel;
    Vector3 change = new Vector3(15, 15, 15);
    
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        vel = ps.velocityOverLifetime;
        vel.speedModifier = 5f;
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            StartCoroutine(ChangingWind(change, 7));
        }
    }

    public void StartChangingWindCoroutine(Vector3 direction, int value) {
        ChangingWind(direction, value);
    }

    //Couroutine basée sur celle utilisée pour les bourrasques de vent (GustOfWind).
    //Elle utilise un vecteur et une valeur pour changer temporairement la direction et la vitesse des flocons
    IEnumerator ChangingWind(Vector3 direction, int value) {
        float currentValue = value;
        for (int j = 0; j < 4; j++) {
            vel.x = new ParticleSystem.MinMaxCurve(vel.x.constantMin + (direction.x * value / 4), vel.x.constantMax + (direction.x * value / 4));
            vel.y = new ParticleSystem.MinMaxCurve(vel.y.constantMin + (direction.y * value / 4), vel.y.constantMax + (direction.y * value / 4));
            vel.z = new ParticleSystem.MinMaxCurve(vel.z.constantMin + (direction.z * value / 4), vel.z.constantMax + (direction.z * value / 4));
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < 5; i++) { 
            vel.x = new ParticleSystem.MinMaxCurve(vel.x.constantMin - (direction.x * (currentValue / 2)), vel.x.constantMax - (direction.x * (currentValue / 2)));
            vel.y = new ParticleSystem.MinMaxCurve(vel.y.constantMin - (direction.y * (currentValue / 2)), vel.y.constantMax - (direction.y * (currentValue / 2)));
            vel.z = new ParticleSystem.MinMaxCurve(vel.z.constantMin - (direction.z * (currentValue / 2)), vel.z.constantMax - (direction.z * (currentValue / 2)));
            currentValue -= currentValue / 2;
            yield return new WaitForSeconds(.25f);
        }
        vel.x = new ParticleSystem.MinMaxCurve(vel.x.constantMin - (direction.x * currentValue), vel.x.constantMax - (direction.x * currentValue));
        vel.y = new ParticleSystem.MinMaxCurve(vel.y.constantMin - (direction.y * currentValue), vel.y.constantMax - (direction.y * currentValue));
        vel.z = new ParticleSystem.MinMaxCurve(vel.z.constantMin - (direction.z * currentValue), vel.z.constantMax - (direction.z * currentValue));
    }
}
