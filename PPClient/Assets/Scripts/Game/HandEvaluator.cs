using System.Collections.Generic;
using System.Linq;

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

public partial class HandEvaluator
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
		var rankDict = GetRankCount(cards);
		var suitDict = GetSuitCount(cards);

		bool isStraight = IsStraight(rankDict);
		bool isFlush = IsFlush(suitDict);

		int quads = 0, trips = 0, pairs = 0;
		foreach( var cnt in rankDict.Values )
		{
			if( cnt == 4 ) quads++;
			else if( cnt == 3 ) trips++;
			else if( cnt == 2 ) pairs++;
		}

		//1.스트레이트 플러쉬
		if( isStraight && isFlush )
			return HandRank.StraightFlush;
		//2.포카드
		if( quads > 0 )
			return HandRank.FourOfAKind;
		//3.풀하우스
		if( trips > 0 && (pairs > 0 || trips > 1) )
			return HandRank.FullHouse;
		//4.플러쉬
		if( isFlush )
			return HandRank.Flush;
		//5.스트레이트
		if( isStraight )
			return HandRank.Straight;
		//6.트리플
		if( trips > 0 )
			return HandRank.ThreeOfAKind;
		//7.페어
		if( pairs == 2 )
			return HandRank.TwoPair;
		if( pairs == 1 )
			return HandRank.OnePair;

		return HandRank.HighCard;
	}
}

public partial class HandEvaluator
{
	private static Dictionary<Rank, int> GetRankCount( List<Card> cards )
	{
		var rankDict = new Dictionary<Rank, int>();
		foreach( Card card in cards )
		{
			if( rankDict.TryGetValue( card.Rank, out int count ) )
				rankDict[card.Rank] = count + 1;
			else
				rankDict.Add( card.Rank, 1 );
		}
		return rankDict;
	}
	private static Dictionary<Suit, int> GetSuitCount( List<Card> cards )
	{
		var suitDict = new Dictionary<Suit, int>();
		foreach( Card card in cards )
		{
			if( suitDict.TryGetValue( card.Suit, out int count ) )
				suitDict[card.Suit] = count + 1;
			else
				suitDict.Add( card.Suit, 1 );
		}
		return suitDict;
	}

	private static bool IsFlush( Dictionary<Suit, int> suitDict )
	{
		return suitDict.Values.Any( val => val >= 5 );
	}
	private static bool IsStraight( Dictionary<Rank, int> rankDict )
	{
		if( IsLowStratight( rankDict ) || IsCircularStraight( rankDict ) )
			return true;

		// Rank 키를 정렬하여 리스트로 변환
		List<int> ranks = rankDict.Keys
		.Select(rank => (int)rank)
		.OrderBy(r => r)
		.ToList();

		// 연속된 숫자가 5개 이상인지 확인 (최적화된 방식)
		int stack = 0;
		for( int i = 1; i < ranks.Count; i++ )
		{
			if( ranks[i] == ranks[i - 1] + 1 )
				stack++;
			else
				stack = 0; // 연속이 끊기면 초기화

			if( stack >= 4 )
				return true;
		}

		return false;
	}
	private static bool IsLowStratight( Dictionary<Rank, int> rankDict )
	{
		return rankDict.ContainsKey( Rank.Ace )
			&& rankDict.ContainsKey( Rank.Two )
			&& rankDict.ContainsKey( Rank.Three )
			&& rankDict.ContainsKey( Rank.Four )
			&& rankDict.ContainsKey( Rank.Five );
	}
	private static bool IsCircularStraight( Dictionary <Rank, int> rankDict )
	{
		int[][] CircularStraights =
		{
		new[] {10, 11, 12, 13, 14}, // 10-J-Q-K-A
        new[] {11, 12, 13, 14, 2},  // J-Q-K-A-2
        new[] {12, 13, 14, 2, 3}    // Q-K-A-2-3
		};

		foreach( var straight in CircularStraights )
		{
			if( straight.All( rank => rankDict.ContainsKey( (Rank)rank ) ) )
				return true;
		}
		return false;
	}
}
