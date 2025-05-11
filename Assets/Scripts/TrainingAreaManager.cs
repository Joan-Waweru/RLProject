using UnityEngine;

public enum DifficultyLevel { Easy, Medium, Hard }

public class TrainingAreaManager : MonoBehaviour
{
    public DifficultyLevel difficulty = DifficultyLevel.Easy;

    // Called by BallAgent to set its parameters based on area difficulty
    public void ConfigureAgent(BallAgent agent)
{
    switch (difficulty)
    {
        case DifficultyLevel.Easy:
            agent.speed = 15f;
            agent.minObstacles = 1;
            agent.maxObstacles = 1;
            break;

        case DifficultyLevel.Medium:
            agent.speed = 20f;
            agent.minObstacles = 2;
            agent.maxObstacles = 2;
            break;

        case DifficultyLevel.Hard:
            agent.speed = 25f;
            agent.minObstacles = 3;
            agent.maxObstacles = 3;
            break;
    }
}

}
