using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collecte : MonoBehaviour
{
	[Header("Collecter la neige")]
	private bool[,] playerHasPassed;
	private Vector2Int playerPositionOnTerrain;
	private Terrain terr;
	private int hmWidth;
	private int hmHeight;
	private int mapResolution;
	private int posXInTerrain;
	private int posYInTerrain;
	private TerrainData td;
	public float collect = 0;

	[Space]
	[Header("Variation")]
	public float poids = 0;
	public float poidsMin = 100;
	public float poidsMax = 200;
	public float collectBig = 0;
	public PlayerMovement mouvement;

	[Space]
	[Header("Lancer la neige")]
	public Transform firePoint;
	public GameObject canonBall;
	public float speed = 5f;
	public float canonBallLifeTime = 10f;
	private GameObject firedBall;


    // Start is called before the first frame update
    void Start()
    {
		mouvement = GetComponent<PlayerMovement> ();
		terr =  GameObject.Find("Terrain").GetComponent<Terrain>();
		td = terr.terrainData;
		hmWidth = td.heightmapWidth;
		hmHeight = td.heightmapHeight;
		mapResolution = td.heightmapResolution;
		playerHasPassed = new bool[mapResolution, mapResolution];
	}

    // Update is called once per frame
    void Update()
    {
		playerPositionOnTerrain = FindPlayerPosition();
		Collect ();
		Velocity ();
		if (poids > 10 && Input.GetMouseButtonDown (0))
		{
			poids = poids -10;
			collect = collect - 10;
		}
    }

	/*void Fire()
	{
		firedBall = Instantiate(canonBall, firePoint.position, firePoint.rotation);
		StartCoroutine(firedBall.GetComponent<Spawner>().destroyBallAfterTime(canonBallLifeTime));
		firedBall.GetComponent<Rigidbody> ().velocity = firePoint.transform.forward * speed;
	}*/

	private Vector2Int FindPlayerPosition()
	{
		// get the normalized position of this game object relative to the terrain

		Vector3 tempCoord = (gameObject.transform.position - terr.gameObject.transform.position);
		Vector3 coord;
		coord.x = tempCoord.x / terr.terrainData.size.x;
		coord.y = tempCoord.y / terr.terrainData.size.y;
		coord.z = tempCoord.z / terr.terrainData.size.z;

		// get the position of the terrain heightmap where this game object is
		posXInTerrain = (int)(coord.x * hmWidth);
		posYInTerrain = (int)(coord.z * hmHeight);
		return new Vector2Int(posXInTerrain, posYInTerrain);
	}

	void Collect()
	{
		if (!playerHasPassed[playerPositionOnTerrain.x, playerPositionOnTerrain.y])
		{
			if(this.GetComponent<PlayerMovement>().AmIGrounded())
			{
				playerHasPassed[playerPositionOnTerrain.x, playerPositionOnTerrain.y] = true;
				collect++;
				poids++;
			}
		}
	}

	void Velocity()
	{
		if (poids >= poidsMin && poids <= poidsMax) {
			mouvement.speed = 6;
		}
		else if (poids > poidsMax)
		{
			mouvement.speed = 0;
		}
		else
		{
			mouvement.speed = 12;
		}
	}
}
