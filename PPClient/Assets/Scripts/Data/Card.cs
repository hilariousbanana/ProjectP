public enum Suit
{
	Spades,
	Hearts,
	Diamonds,
	Clubs,
}

public enum Rank
{
	Two = 2,
	Three,
	Four,
	Five,
	Six,
	Seven,
	Eight,
	Nine,
	Ten,
	Jack,
	Queen,
	King,
	Ace,
}

public class Card
{
	public Suit Suit { get; }
	public Rank Rank { get; }

	public Card( Suit suit, Rank rank )
	{
		this.Suit = suit;
		this.Rank = rank;
	}
	public override string ToString()
	{
		return $"{Rank} of {Suit}";
	}
}
