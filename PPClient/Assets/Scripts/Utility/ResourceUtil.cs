using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUtil : MonoBehaviour
{
    public static Sprite LoadCardSprite( Suit suit, Rank rank )
	{
		string path = $"Card/{suit}/{rank}";
		return Resources.Load<Sprite>( path ) as Sprite;
	}
}
