using MicrogameCourse.Framework;
using UnityEngine;

namespace MicrogameCourse.Microgames
{
    public class MG3GameManager : MicrogameBehaviour
    {
        [SerializeField] public GameObject targetPrefab;
        public void SpawnHunter()
        {
            Instantiate(targetPrefab, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0), Quaternion.identity);
        }
    }
}
