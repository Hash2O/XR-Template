using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleManager : MonoBehaviour
{
    [SerializeField] float _distanceMax;
    [SerializeField] GameObject _player;

    [SerializeField] float _vitesse;
    [SerializeField] float _vitesseTranquille;
    [SerializeField] float _vitesseRapide;

    [SerializeField] float _distanceMaxPoint;
    [SerializeField] float _distancePlay;

    [SerializeField] Animator _animator;

    Vector3 _newPosition;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _newPosition = CreatePoint();
        _vitesse = _vitesseTranquille;
    }


    void Update()
    {

        {
            float _distancePoint = Vector3.Distance(_newPosition, transform.position);
            if (_distancePoint < 1f)
            {
                _newPosition = CreatePoint();
            }
            MoveToObject(_newPosition); // 
        }

    }

    Vector3 CreatePoint()
    {
        Vector3 _pointPosition = new Vector3(Random.Range(-_distanceMaxPoint, _distanceMaxPoint) + gameObject.transform.position.x, gameObject.transform.position.y, Random.Range(-_distanceMaxPoint, _distanceMaxPoint) + gameObject.transform.position.z);
        return _pointPosition;
    }


    void MoveToObject(Vector3 _movTo)
    {
        float _distanceMoveTo = Vector3.Distance(_movTo, gameObject.transform.position);
        gameObject.transform.LookAt(_movTo);
        Vector3 _vectorMove = _movTo - gameObject.transform.position;
        gameObject.transform.position += _vectorMove.normalized * _vitesse;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Whales !");
            WindSailingBoatManager _player = other.gameObject.GetComponent<WindSailingBoatManager>();
            _player.meteo = "Nous partageons cette baie avec plusieurs baleines, et nous allons en croiser quelques unes. Gardez le cap !";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[6]);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WindSailingBoatManager _player = other.gameObject.GetComponent<WindSailingBoatManager>();
            _player.meteo = "F�licitations, continuez comme �a !";
            _player.meteoText.SetText(_player.meteo);
            _player.audioSource.PlayOneShot(_player.recommandations[5]);
        }
    }
}
