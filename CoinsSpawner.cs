using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coin;

    public CoinPlace[] CoinPlaces { get; private set; } = null;

    private void Awake()
    {
        CoinPlaces = GetComponentsInChildren<CoinPlace>();
    }

    private void Start()
    {
        if (CoinPlaces.Length > 0)
        {
            SpawnCoin(CoinPlaces[0]);
        }
    }

    public void SpawnCoin (CoinPlace coinPlaces)
    {
        Instantiate(_coin, coinPlaces.transform.position, Quaternion.identity, coinPlaces.transform);
    }

}
