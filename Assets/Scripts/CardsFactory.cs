using UnityEngine;


[CreateAssetMenu]
public class CardsFactory : GameObjectFactory
{
    [SerializeField]
    private Card _cardPrefab;

    public void Reclaim(Card card)
    {
        Destroy(card.gameObject);
    }    

    public Card Get()
    {
        Card card = CreateGameObjectInstance(_cardPrefab);
        card.OriginFactory = this;
        card.SetPosition(GameConfig.Instance.StartCardPosition);
        return card;
    }
}
