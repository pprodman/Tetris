using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Pieces
    public GameObject[] piecePrefabs;
    private List<GameObject> piecesPool = new List<GameObject>();
    public GameObject blockPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject prefab in piecePrefabs)
        {
            GameObject piece = Instantiate(prefab, transform.position, Quaternion.identity);
            piece.SetActive(false);
            piecesPool.Add(piece);
        }

        Board.InitializeGrid(blockPrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnNext()
    {
        //Random index
        int i = Random.Range(0, piecePrefabs.Length);

        //Spawn Group at current Position
        Instantiate(piecePrefabs[i], transform.position, Quaternion.identity);
    }

    public void ActivateNextPiece()
    {
        int randomIndex = Random.Range(0, piecesPool.Count);
        GameObject piece = piecesPool[randomIndex];

        while (piece.activeInHierarchy)
        {
            randomIndex = Random.Range(0, piecesPool.Count);
            piece = piecesPool[randomIndex];
        }

        piece.transform.position = new Vector3(5, 16, 0);
        piece.SetActive(true);
        piece.GetComponent<Piece>().enabled = true;
    }
}