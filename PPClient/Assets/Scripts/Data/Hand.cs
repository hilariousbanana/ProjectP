using System;
using System.Collections.ObjectModel;


public class Hand
{
	private readonly Card[] _cards = new Card[2];
	public ReadOnlyCollection<Card> Cards => Array.AsReadOnly( _cards );

	public Hand( Card card1, Card card2 )
	{
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
		return $"Hand: {string.Join( ", ", Cards )}";
	}
}
