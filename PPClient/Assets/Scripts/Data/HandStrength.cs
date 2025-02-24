using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStrength
{
	public HandRank   Rank;
	public Rank       MainCard;
	public List<Card> Kickers;

	public HandStrength( HandRank rank, Rank mainCard, List<Card> kickers )
	{
		this.Rank = rank;
		this.MainCard = mainCard;
		this.Kickers = kickers;
	}
}