using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandRank
{
	HighCard,
	OnePair,
	TwoPair,
	ThreeOfAKind,
	Straight,
	Flush,
	FullHouse,
	FourOfAKind,
	StraightFlush,
	RoyalFlush,
}

public class HandEvaluator
{
	public static HandRank EvaluateHand( Hand hand, Board board )
	{
		List<Card> cards = new List<Card>();
		cards.AddRange( hand.Cards );
		cards.AddRange( board.Cards );

		return DetermineBestHand( cards );
	}

	private static HandRank DetermineBestHand( List<Card> cards )
	{
		HandRank rank = HandRank.HighCard;
		return rank;
	}
}
