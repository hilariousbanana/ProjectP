using System;
using System.Collections.ObjectModel;


public class Hand
{
	public int ID
	{
		get;
		private set;
	}
	private readonly Card[] _cards = new Card[2];
	public ReadOnlyCollection<Card> Cards => Array.AsReadOnly( _cards );

	public Hand( int id, Card card1, Card card2 )
	{
		ID = id;
		_cards[0] = card1;
		_cards[1] = card2;
	}
	public void Clear()
	{
		_cards[0] = null;
		_cards[1] = null;
	}
	public override string ToString()
	{
		return $"<color=skyblue>[ Hand ]</color> ID: {ID} Hands: {string.Join( ", ", Cards )}";
	}
}
