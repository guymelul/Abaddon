﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour 
{
    public GameObject bulletImpactPrefab_Metal;
    public GameObject bulletImpactPrefab_Wood;
    public GameObject bulletImpactPrefab_Concrete;

    private Vector3 PrevPosition;

	// Use this for initialization
	void Start()
    {
		PrevPosition = transform.position;
	}

    void FixedUpdate()
    {
        PrevPosition = transform.position;
        transform.position += transform.forward.normalized * 8;

        Vector3 vec = transform.position - PrevPosition;
        RaycastHit[] rayHits = Physics.RaycastAll(PrevPosition,(vec).normalized,vec.magnitude);

        Debug.DrawLine(PrevPosition,transform.position,Color.red,2,false);

        foreach(RaycastHit rayhit in rayHits)
        {
            GameObject bulletImpactPrefab;
            string tag = rayhit.collider.gameObject.tag.ToLower();

            switch(tag)
            {
                case "metal": {
                    bulletImpactPrefab = bulletImpactPrefab_Metal;
                    break;
                }
                case "wood": {
                    bulletImpactPrefab = bulletImpactPrefab_Wood;
                    break;
                }
                case "concrete": {
                    bulletImpactPrefab = bulletImpactPrefab_Concrete;
                    break;
                }
                case "player":
                    return;
                default: {
                    Destroy(gameObject);
                    return; 
                }
            }

            Debug.Log("BulletCollision: Bullet Hit " + tag);

            var impact = (GameObject) Instantiate(
                        bulletImpactPrefab,
                        rayhit.point,
                        Quaternion.Euler(rayhit.normal) * bulletImpactPrefab.transform.rotation
            );

            // Destroy in 3 sec
            Destroy(impact, 3.0f);

            // Destroy Bullet
            Destroy(gameObject);
        }
    }

}