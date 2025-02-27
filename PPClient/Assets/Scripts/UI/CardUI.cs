using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
	[SerializeField]
	private Image img_Card;

	public void Apply( Card card )
	{
		img_Card.sprite = ResourceUtil.LoadCardSprite( card.Suit, card.Rank );
	}
}
