﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Road class
/// </summary>
public class Road
{
    /// <summary>
    /// Points of the road
    /// </summary>
    public Vector3[] RoadPoints, _roadPointsRight, _roadPointsLeft;

    /// <summary>
    /// Road GameObject
    /// </summary>
    private GameObject _road;

    /// <summary>
    /// Road mesh
    /// </summary>
    private RoadMesh _roadMesh;

    /// <summary>
    /// Start and end position of the road
    /// </summary>
    public Vector3 Pos1, Pos2;

    /// <summary>
    /// Length of the road
    /// </summary>
    public float Len;

    /// <summary>
    /// The exact middle position of the road, where the assigned settler stands TODO remove bugs
    /// </summary>
    public Vector3 MiddlePos;

    /// <summary>
    /// The Nodes of the road points
    /// </summary>
    public List<Node> Nodes;


    public Road(Vector3 pos1)
    {
        // Initialisation
        RoadPoints = new[] {pos1};
        Pos1 = pos1; // First Point of the Road
        Pos2 = pos1; // last Point of the Road

        _road = Object.Instantiate(GameHandler.DirtRoadPrefab, Vector3.zero, Quaternion.identity);
        _roadMesh = _road.GetComponent<RoadMesh>();
        Nodes = new List<Node> {Grid.NodeFromWorldPoint(Pos1)};
        Nodes[0].Buildable = false;
        Nodes[0].Type = "Road";
    }


    /// <summary>
    /// Adds a point to the road
    /// </summary>
    /// <param name="point"></param>
    public bool add_point(Vector3 point)
    {
        List<Node> path = RoadPathfinding.FindPath(Pos2, point);

        if (path.Count == 0)
        {
            return false;
        }

        for (int i = 1; i < path.Count; i++)
        {
            path[i].Buildable = false;
            path[i].Type = "Road";
            Nodes.Add(path[i]);
        }
        
        
        Nodes[0].RoadTo.Add(Nodes[1]);
        Nodes[Nodes.Count - 1].RoadTo.Add(Nodes[Nodes.Count - 2]);
        for (int i = 1; i < Nodes.Count - 1; i++)
        {
            Nodes[i].RoadTo.Add(Nodes[i - 1]);
            Nodes[i].RoadTo.Add(Nodes[i + 1]);
        }


        Vector3[] temp = new Vector3[path.Count - 1];


        for (int i = 1; i < path.Count; i++)
        {
            temp[i - 1] = path[i].WorldPosition;
            Len += Vector3.Distance(path[i - 1].WorldPosition, path[i].WorldPosition);
        }

        RoadPoints = RoadPoints.Concat(temp).ToArray();

        Pos2 = point;
        MiddlePos = Vector3.Lerp(RoadPoints[Mathf.FloorToInt((RoadPoints.Length - 1) / 2)],
            RoadPoints[Mathf.CeilToInt((RoadPoints.Length - 1) / 2)], 0.5f);

        draw_road();
        return true;
    }

    /// <summary>
    /// Lays a smooth curve through the road points, offsets the points to both sides and applies them to the mesh
    /// </summary>
    private void draw_road()
    {
        Vector3[] tempRoadPoints = GameHandler.MakeSmoothCurve(RoadPoints);
        _roadPointsLeft = new Vector3[tempRoadPoints.Length];
        _roadPointsRight = new Vector3[tempRoadPoints.Length];

        float angle = (Mathf.Rad2Deg * Mathf.Atan2(tempRoadPoints[0].x - tempRoadPoints[1].x,
            tempRoadPoints[0].z - tempRoadPoints[1].z));

        Vector3 pos1 = tempRoadPoints[0] +
                       Quaternion.AngleAxis(angle + 90, Vector3.up) * Vector3.forward *
                       GameHandler.RoadWidth / 2;
        Vector3 pos2 = tempRoadPoints[0] +
                       Quaternion.AngleAxis(angle + 90, Vector3.up) * Vector3.forward *
                       GameHandler.RoadWidth / -2;
        _roadPointsLeft[0] = new Vector3(pos1.x, GameHandler.ActiveTerrain.SampleHeight(pos1) + 0.1f, pos1.z);
        _roadPointsRight[0] = new Vector3(pos2.x, GameHandler.ActiveTerrain.SampleHeight(pos2) + 0.1f, pos2.z);

        for (int i = 1; i < tempRoadPoints.Length; i++)
        {
            angle = (Mathf.Rad2Deg * Mathf.Atan2(tempRoadPoints[i - 1].x - tempRoadPoints[i].x,
                tempRoadPoints[i - 1].z - tempRoadPoints[i].z));

            pos1 = tempRoadPoints[i] +
                   Quaternion.AngleAxis(angle + 90, Vector3.up) * Vector3.forward *
                   GameHandler.RoadWidth / 2;
            pos2 = tempRoadPoints[i] +
                   Quaternion.AngleAxis(angle + 90, Vector3.up) * Vector3.forward *
                   GameHandler.RoadWidth / -2;


            _roadPointsLeft[i] = new Vector3(pos1.x, GameHandler.ActiveTerrain.SampleHeight(pos1) + 0.1f, pos1.z);
            _roadPointsRight[i] = new Vector3(pos2.x, GameHandler.ActiveTerrain.SampleHeight(pos2) + 0.1f, pos2.z);
        }

        _roadMesh.SetVertices(_roadPointsLeft, _roadPointsRight);
    }

    /// <summary>
    /// Destroys the road and the corresponding mesh
    /// </summary>
    public void destroy()
    {
        foreach (var n in Nodes)
        {
            n.Buildable = false;
            n.RoadTo.Clear();
        }

        _roadMesh.destroy();
    }
}