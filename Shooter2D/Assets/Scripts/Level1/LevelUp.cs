using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private int currentExperienceAtLevel;
    private int maxCurrentExperienceAtLevel;
    private int level = 0;
    private int experience = 0;
    [SerializeField] int nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        currentExperienceAtLevel = 0;
        maxCurrentExperienceAtLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current - " + currentExperienceAtLevel);
        Debug.Log("max - " + maxCurrentExperienceAtLevel);
        Debug.Log("level - " + level);
    }

    public void PlayerLevelUp(int experience)
    {
        currentExperienceAtLevel += experience;
        experience = currentExperienceAtLevel;
        if (currentExperienceAtLevel >= maxCurrentExperienceAtLevel)
        {
            level++;
            currentExperienceAtLevel = experience;
            maxCurrentExperienceAtLevel += nextLevel;
        }
    }
}
