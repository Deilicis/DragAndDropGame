using UnityEngine;

public class CarAndPlaceRandomizer : MonoBehaviour
{
    [System.Serializable]
    public class CarSlot
    {
        public Transform car;       // Car object in scene
        public Transform carPlace;  // Matching outline

        [HideInInspector] public Vector3 originalPosition;
        [HideInInspector] public Quaternion originalRotation;
        [HideInInspector] public Vector3 originalScale;
    }

    [SerializeField] private CarSlot[] carSlots;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] carSpawnPoints;       // Where cars can spawn
    [SerializeField] private Transform[] carPlaceSpawnPoints;  // Where outlines can spawn

    [Header("Randomization Settings")]
    [SerializeField, Range(0.5f, 1f)] private float minScaleMultiplier = 0.9f;
    [SerializeField, Range(1f, 1.5f)] private float maxScaleMultiplier = 1.1f;

    private void Start()
    {
        // Randomize car spawn points
        Shuffle(carSpawnPoints);
        for (int i = 0; i < carSlots.Length && i < carSpawnPoints.Length; i++)
        {
            Transform spawn = carSpawnPoints[i];
            Transform car = carSlots[i].car;

            // Position
            car.position = spawn.position;

            // Random rotation (Z-axis only)
            float randomZ = Random.Range(0f, 360f);
            car.rotation = Quaternion.Euler(0f, 0f, randomZ);

            // Random scale variation around 0.65
            float scaleMultiplier = Random.Range(minScaleMultiplier, maxScaleMultiplier);
            car.localScale = new Vector3(0.65f * scaleMultiplier, 0.65f * scaleMultiplier, 1f);

            // Save randomized transform as "original"
            carSlots[i].originalPosition = car.position;
            carSlots[i].originalRotation = car.rotation;
            carSlots[i].originalScale = car.localScale;
        }

        // Randomize carPlaces (no rotation or scale changes)
        Shuffle(carPlaceSpawnPoints);
        for (int i = 0; i < carSlots.Length && i < carPlaceSpawnPoints.Length; i++)
        {
            Transform spawn = carPlaceSpawnPoints[i];
            carSlots[i].carPlace.position = spawn.position;
            carSlots[i].carPlace.rotation = spawn.rotation;
        }
    }

    void Shuffle(Transform[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rand = Random.Range(i, array.Length);
            Transform temp = array[i];
            array[i] = array[rand];
            array[rand] = temp;
        }
    }

    // Called from DropPlaceScript when a car is misplaced
    public void ResetCarToSpawn(Transform car)
    {
        foreach (var slot in carSlots)
        {
            if (slot.car == car)
            {
                slot.car.position = slot.originalPosition;
                slot.car.rotation = slot.originalRotation;
                slot.car.localScale = slot.originalScale;
                return;
            }
        }
    }
}
