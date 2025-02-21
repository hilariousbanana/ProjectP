using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Board
{
	private const int MAX_CARD_COUNT = 5;

	private readonly List<Card> _cards = new ( MAX_CARD_COUNT );
	public ReadOnlyCollection<Card> Cards => _cards.AsReadOnly();

	public void Clear()
	{
		_cards.Clear();
	}
	public bool Add( Card card )
	{
		if( _cards.Count < MAX_CARD_COUNT )
		{
			_cards.Add( card );
			return true;
		}
		return false;
	}
	public override string ToString()
	{
		return $"Board: {string.Join( ", ", _cards )}";
	}
}
