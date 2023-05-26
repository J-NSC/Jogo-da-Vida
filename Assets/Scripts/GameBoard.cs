using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.Tilemaps;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Tilemap currentState;
    [SerializeField] private Tilemap nexState; 
    [SerializeField] private Tile aliveTile;
    [SerializeField] private Tile deadTile; 
    [SerializeField] private Pattern pattern; 
    [SerializeField] private float updateInterval = 0.05f;

    private HashSet<Vector3Int> aliveCells;
    private HashSet<Vector3Int> cellsToCheck;


    private void Awake() {
        aliveCells = new HashSet<Vector3Int>();    
        cellsToCheck = new HashSet<Vector3Int>();
    }

    private void Start() {
      SetPattern(pattern);  
    }

    private void SetPattern(Pattern pattern){
        clear();

        Vector2Int center = pattern.GetCenter();

        for(int i = 0; i < pattern.cells.Length; i++) {
            Vector3Int cell = (Vector3Int) (pattern.cells[i] - center);
            currentState.SetTile(cell, aliveTile);
            aliveCells.Add(cell);
        }
    }


    private void clear(){
        currentState.ClearAllTiles();
        nexState.ClearAllTiles();
    }

    private void OnEnable() {
        StartCoroutine(Simulate());
    }

    private IEnumerator Simulate(){
        var interval = new WaitForSeconds(updateInterval);
        while(enabled){
            UpdateState();
            yield return interval;
        }
    }

    private void UpdateState(){
        cellsToCheck.Clear();

        foreach(Vector3Int cell in aliveCells){
            
            for(int x = -1; x <= 1; x++) {
                for(int y = -1; y < 1; y++) {
                    cellsToCheck.Add(cell + new Vector3Int(x,y,0));
                }
            }
        }

        foreach(Vector3Int cell in cellsToCheck) {
            
        }
    }   
}
