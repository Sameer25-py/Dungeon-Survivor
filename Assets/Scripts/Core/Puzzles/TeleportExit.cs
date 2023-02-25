using UnityEngine;

namespace DungeonSurvivor.Core.Puzzles
{
    public class TeleportExit : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Obstacle")) return;
            transform.parent.GetComponent<Collider>().enabled = true;
            print("Teleport Exited");
        }
    }
}
