using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : MonoBehaviour
{
	[SerializeField] CardUI card1;
	[SerializeField] CardUI card2;

	public void Apply( Card card1, Card card2 )
	{
		this.card1.Apply( card1 );
		this.card2.Apply( card2 );
	}
}
