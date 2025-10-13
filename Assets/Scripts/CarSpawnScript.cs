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
    }

    [SerializeField] private CarSlot[] carSlots;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] carSpawnPoints;       // Where cars can spawn
    [SerializeField] private Transform[] carPlaceSpawnPoints;  // Where outlines can spawn

    private void Start()
    {
        // Randomize cars
        Shuffle(carSpawnPoints);
        for (int i = 0; i < carSlots.Length && i < carSpawnPoints.Length; i++)
        {
            Transform spawn = carSpawnPoints[i];
            carSlots[i].car.position = spawn.position;
            carSlots[i].car.rotation = spawn.rotation;

            // Save the randomized spawn as the new "original"
            carSlots[i].originalPosition = spawn.position;
            carSlots[i].originalRotation = spawn.rotation;
        }

        // Randomize carPlaces
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

    // Call this method from your "misplaced" logic to reset a specific car
    public void ResetCarToSpawn(Transform car)
    {
        foreach (var slot in carSlots)
        {
            if (slot.car == car)
            {
                slot.car.position = slot.originalPosition;
                slot.car.rotation = slot.originalRotation;
                return;
            }
        }
    }
}
