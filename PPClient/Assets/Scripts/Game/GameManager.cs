using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState
{
	PreFlop,
	Flop,
	Turn,
	River,
	Showdown,
}

public class GameManager : MonoBehaviour
{
	private List<Card>                    deck  = new();
	private List<Hand>                    hands = new();
	private Dictionary<int, HandStrength> ranks = new();
	private Board                         board = new();

	[SerializeField]
	private int        playerCount = 3;
	private GameState  state = GameState.PreFlop;

	private void Start()
	{
		Initialize();
	}

	public void Initialize()
	{
		CreateDeck();
		ShuffleDeck();
		DealHands();
		DealBoard();
	}

	private void CreateDeck()
	{
		deck.Clear();
		foreach( Suit suit in Enum.GetValues( typeof( Suit ) ) )
		{
			foreach( Rank rank in Enum.GetValues( typeof( Rank ) ) )
			{
				deck.Add( new Card( suit, rank ) );
			}
		}
	}
	public void ShuffleDeck()
	{
		for( int i = 0; i < deck.Count; i++ )
		{
			var card = deck[i];
			int rand = UnityEngine.Random.Range( i, deck.Count );
			deck[i] = deck[rand];
			deck[rand] = card;
		}
	}

	public void DealHands()
	{
		for( int i = 0; i < playerCount; i++ )
		{
			Card card1 = deck[0];
			deck.RemoveAt( 0 );
			Card card2 = deck[0];
			deck.RemoveAt( 0 );

			hands.Add( new Hand( i, card1, card2 ) );
		}
	}
	public void DealBoard()
	{
		board.Clear();
		for( int i =0; i < 5; i++ )
		{
			board.Add( deck[0] );
			deck.RemoveAt( 0 );
		}
	}

	public void RevealFlop()
	{

	}
	public void RevealTurn()
	{

	}
	public void RevealRiver()
	{

	}

	public void EvaluateGame()
	{
		ranks.Clear();
		for( int i = 0; i < playerCount; i++ )
		{
			ranks.Add( i, HandEvaluator.EvaluateHand( hands[i], board ) );
		}
		var bestHand = BestHand( ranks );

	}
	public KeyValuePair<int, HandStrength> BestHand( Dictionary<int, HandStrength> dict )
	{
		KeyValuePair<int, HandStrength> best = dict.First();
		foreach( var hand in dict )
		{
			if( hand.Key == 0 || hand.Value.Rank > best.Value.Rank )
			{
				best = hand;
			}
			else if( hand.Value.Rank == best.Value.Rank )
			{
				if( hand.Value.MainCard > best.Value.MainCard )
				{
					best = hand;
				}
				else if( hand.Value.MainCard == best.Value.MainCard )
				{
					for( int i =0; i < Math.Min( hand.Value.Kickers.Count, best.Value.Kickers.Count ); i++ )
					{
						if( hand.Value.Kickers[i] >  best.Value.Kickers[i] )
						{
							best = hand;
							break;
						}
						else if( hand.Value.Kickers[i] < best.Value.Kickers[i] )
						{
							break;
						}
					}
				}
			}
		}
		return best;
	}
}
