using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tester : MonoBehaviour
{
	private List<Card> cards = new List<Card>();
	private List<Hand> hands = new List<Hand>();
	private Board board = new Board();
	private Dictionary<int, HandRank> result = new Dictionary < int, HandRank >();

	private void Start()
	{
		RunHandEvaluation( 3, 3 );
	}

	private void RunHandEvaluation( int repeatCnt, int person )
	{
		for( int i = 0; i < repeatCnt; i++ )
		{
			result.Clear();
			SetCards();
			AddHand( person );
			SetBoard();
			foreach( var hand in hands )
			{
				result.Add( hand.ID, HandEvaluator.EvaluateHand( hand, board ) );
				Debug.Log( hand.ToString() );
			}

			var sortedResults = result.OrderByDescending(kvp => kvp.Value).ToList();
			foreach( var entry in sortedResults )
			{
				Debug.Log( $"Player {entry.Key}: {entry.Value}" );
			}

			Debug.Log( $"[{this}]" );
		}
	}

	private void SetCards()
	{
		cards.Clear();
		foreach( Suit suit in Enum.GetValues( typeof( Suit ) ) )
		{
			foreach( Rank rank in Enum.GetValues( typeof( Rank ) ) )
			{
				cards.Add( new Card( suit, rank ) );
			}
		}
	}
	private void AddHand( int person )
	{
		hands.Clear();
		for( int i = 0; i < person; i++ )
		{
			hands.Add( new Hand( i, GetRandomCard(), GetRandomCard() ) );
		}
	}
	private Card GetRandomCard()
	{
		int index = UnityEngine.Random.Range( 0, cards.Count );
		Card card = cards[index];
		cards.RemoveAt( index );
		return card;
	}
	private void SetBoard()
	{
		board.Clear();
		for( int i = 0; i < 5; i++ )
		{
			board.Add( GetRandomCard() );
		}
		Debug.Log( board.ToString() );
	}

	public override string ToString()
	{
		string bestHand = result.Any() ? result.Values.First().ToString() : "No Hands Evaluated";
		return $"<color=green>[ Result ]</color> Participants: {hands.Count} \n Best Hand: {bestHand}";
	}
}
