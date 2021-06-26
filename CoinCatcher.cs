using UnityEngine;

public class CoinCatcher : MonoBehaviour
{
    private CoinsSpawner _coinsSpawner = null;
    private bool _isCollisionWithHero = false;

    private void Start()
    {
        _coinsSpawner = transform.parent.GetComponentInParent<CoinsSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _isCollisionWithHero = collider.gameObject.GetComponentInChildren<Hero>() != null;

        if (_isCollisionWithHero)
        {
            if (_coinsSpawner != null)
                _coinsSpawner.SpawnNextCoin();
        }
    }
}
