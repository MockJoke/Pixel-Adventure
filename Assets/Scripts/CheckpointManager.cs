using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform InitCheckpoint;
    [SerializeField] private Transform[] CheckPoints;
    private int latestCheckpointIndex;

    private void Awake()
    {
        if (CheckPoints == null)
            CheckPoints = GetComponentsInChildren<Transform>();
        
        latestCheckpointIndex = PlayerPrefs.GetInt("LatestCheckpointIndex", 0);
    }

    public void SetLatestCheckpoint(Transform point)
    {
        latestCheckpointIndex = Array.IndexOf(CheckPoints, point);
        PlayerPrefs.SetInt("LatestCheckpointIndex", latestCheckpointIndex);
    }

    public Transform GetLatestCheckPoint()
    {
        return CheckPoints[latestCheckpointIndex];
    }

    public bool hasPassedAnyCheckPoints()
    {
        return latestCheckpointIndex > 0;
    }
}
