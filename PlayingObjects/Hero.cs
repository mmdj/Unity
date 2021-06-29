using UnityEngine;

[RequireComponent(typeof(HeroAnimator))]

public class Hero : MonoBehaviour 
{
    [SerializeField] private CoinsSpawner _coinsSpawner;
    private bool _isCollisionWithCoin = false;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        _isCollisionWithCoin = collider.gameObject.GetComponentInChildren<Coin>() != null;

        if (_isCollisionWithCoin)
        {
            if (_coinsSpawner != null)
                _coinsSpawner.SpawnNextCoin();
        }
    }
}
