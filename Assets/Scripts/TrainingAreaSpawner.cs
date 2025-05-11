using UnityEngine;

public class TrainingAreaSpawner : MonoBehaviour
{
    [Header("Prefab & Settings")]
    public GameObject trainingAreaPrefab;
    public int numAreas = 6;
    public float spacing = 25f;  // Space between each TrainingArea

    void Start()
    {
        SpawnTrainingAreas();
    }

    void SpawnTrainingAreas()
    {
        for (int i = 0; i < numAreas; i++)
        {
            Vector3 position = new Vector3((i % 3) * spacing, 0, (i / 3) * spacing);  // Grid layout
            GameObject area = Instantiate(trainingAreaPrefab, position, Quaternion.identity, transform);

            // Assign difficulty based on index
            var manager = area.GetComponent<TrainingAreaManager>();
            if (manager != null)
            {
                if (i % 3 == 0)
                    manager.difficulty = DifficultyLevel.Easy;
                else if (i % 3 == 1)
                    manager.difficulty = DifficultyLevel.Medium;
                else
                    manager.difficulty = DifficultyLevel.Hard;
            }
        }
    }
}
