using System.Collections;
using System.Collections.Generic;
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
        card.SetPosition(GameSettings.StartCardPosition);
        return card;
    }
}
