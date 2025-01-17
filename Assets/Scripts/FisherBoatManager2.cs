using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherBoatManager2 : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] private List<Vector3> _destinations = new List<Vector3>();

    private void Update()
    {
        MoveAgent();
    }


    private void MoveAgent()
    {
        // we move the agent to the destination using the pool
        if (_destinations.Count > 0)
        {
            if (Vector3.Distance(transform.position, _destinations[0]) > 1.0f)
            {
                transform.LookAt(_destinations[0]);
                transform.position = Vector3.MoveTowards(transform.position, _destinations[0], speed * Time.deltaTime);
            }
            if (Vector3.Distance(transform.position, _destinations[0]) < 1.0f)
            {
                _destinations.RemoveAt(0);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Fisher boat.");
            SailingWindSimulationManager _player = other.gameObject.GetComponent<SailingWindSimulationManager>();
            _player.meteo = "Vous vous rapprochez d'un chalutier. Passez au large pour �viter ses filets.";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[2]);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SailingWindSimulationManager _player = other.gameObject.GetComponent<SailingWindSimulationManager>();
            _player.meteo = "F�licitations, continuez comme �a.";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[5]);
        }
    }
}
