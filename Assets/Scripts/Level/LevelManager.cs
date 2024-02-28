using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public string[] Levels;

    protected override void Awake()
    {
        base.Awake();
        
        DontDestroyOnLoad(gameObject);
        
        UnlockFirstLevel();
    }

    void UnlockFirstLevel()
    {
        // the very first time the game starts, the first level1 should be unlocked to play 
        if (GetLevelStatus(Levels[0]) == LevelStatus.Locked)
        {
            SetLevelStatus(Levels[0], LevelStatus.Unlocked);
        }
    }

    public void MarkLevelComplete()
    {
        // finding the index of the active level from the array of Levels
        int currentSceneIndex = Array.FindIndex(Levels, level => level == SceneManager.GetActiveScene().name);

        SetLevelStatus(Levels[currentSceneIndex], LevelStatus.Completed);

        // unlock the next level
        if (currentSceneIndex + 1 < Levels.Length)
        {
            SetLevelStatus(Levels[currentSceneIndex + 1], LevelStatus.Unlocked);
        }
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public int GetLatestUnlockedLevelNo()
    {
        int i = 0;
        
        foreach (string level in Levels)
        {
            if (GetLevelStatus(level) == LevelStatus.Unlocked)
            {
                i = Array.IndexOf(Levels, level);
            }
        }

        return i;
    }
    
    public void GoToLatestUnlockedLevel()
    {
        SceneManager.LoadScene(GetLatestUnlockedLevelNo() + 1);
    }
    
    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus levelStatus = (LevelStatus)PlayerPrefs.GetInt(level, 0);
        
        return levelStatus;
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
    }

    public void ResetLevelProgress()
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            SetLevelStatus(Levels[i], i == 0 ? LevelStatus.Unlocked : LevelStatus.Locked);
        }
    }
}

