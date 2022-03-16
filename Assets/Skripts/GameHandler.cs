using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class GameHandler : MonoBehaviour
{
    // Variables to set in the untiy editor
    // Buildable Prefabs
    public GameObject buildableFlag;
    public GameObject buildableHouse1;
    public GameObject buildableHouse2;
    public GameObject buildableHouse3;
    public GameObject buildableHarbour;

    // Building Prefabs
    public GameObject flagPrefab;
    public  GameObject roadPrefab;


    // Terrain
    public Terrain activeTerrain;

    // Materials
    public Material roadMaterial;
    
    // Building Paramters
    public float flagBuildableYOffset;
    public float roadWidth;
    
    // GUI Prefabs
    public GameObject fahnenerzeugungGUIPrefab;
    public GameObject straßenbaumenuGUIPrefab;
    public GameObject fahenmenuPrefab;
    
    // GUI
    public Canvas mainCanvas;

    // static global variables
    
    // Buildable Prefabs
    public static GameObject BuildableFlag;
    public static GameObject BuildableHouse1;
    public static GameObject BuildableHouse2;
    public static GameObject BuildableHouse3;
    public static GameObject BuildableHarbour;
    
    // Building Prefabs
    public static GameObject FlagPrefab;
    public static GameObject RoadPrefab;

    // Terrain
    public static Terrain ActiveTerrain;
    public static TerrainData ActiveTerrainTerrainData;

    // Materials
    public static Material RoadMaterial;
    
    // Building Parameters
    public static float FlagBuildableYOffset;
    public static float RoadWidth;
    
    // GUI Prefabs
    public static GameObject FahnenerzeugungGUIPrefab;
    public static GameObject StraßenbaumenuGUIPrefab;
    public static GameObject FahnenmenuPrefab;
    
    // GUI
    public static Canvas MainCanvas;
    public static bool GUIActive;
    
    // Variables
    public static GameObject ClickedBuildableFlag;
    

    private void Awake()
    {
        // Buildable Prefabs
        BuildableFlag = buildableFlag;
        BuildableHouse1 = buildableHouse1;
        BuildableHouse2 = buildableHouse2;
        BuildableHouse3 = buildableHouse3;
        BuildableHarbour = buildableHarbour;

        // Building Prefabs
        FlagPrefab = flagPrefab;
        
        // Terrain
        ActiveTerrain = activeTerrain;
        ActiveTerrainTerrainData = ActiveTerrain.terrainData;

        // Materials
        RoadMaterial = roadMaterial;
        
        // Building Parameters
        FlagBuildableYOffset = flagBuildableYOffset;
        RoadWidth = roadWidth;
        
        // GUI Prefabs
        FahnenerzeugungGUIPrefab = fahnenerzeugungGUIPrefab;
        StraßenbaumenuGUIPrefab = straßenbaumenuGUIPrefab;
        FahnenmenuPrefab = fahenmenuPrefab;
        
        // GUI
        MainCanvas = mainCanvas;
        GUIActive = false;
    }
}