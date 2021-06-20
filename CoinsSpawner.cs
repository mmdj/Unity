using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    private CoinPlace[] _coinPlaces = null;
    private int _currentCoinPlace = 0;

    private void Awake()
    {
        _coinPlaces = GetComponentsInChildren<CoinPlace>();
    }

    private void Start()
    {
        SpawnNextCoin();
    }

    public void SpawnNextCoin()
    {
        if (_coinPlaces != null && _coinPlaces.Length > 0)
        {
            CoinPlace place = _coinPlaces[_currentCoinPlace];
            Instantiate(_coin, place.transform.position, Quaternion.identity, place.transform);

            ChangeCurrentToNextCoinPlace();
        }
    }

    public void ChangeCurrentToNextCoinPlace()
    {
        _currentCoinPlace++;

        if (_currentCoinPlace >= _coinPlaces.Length)
        {
            _currentCoinPlace = 0;
        }
    }

}
