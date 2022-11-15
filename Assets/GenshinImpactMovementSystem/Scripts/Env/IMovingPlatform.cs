using UnityEngine;

namespace Platformer
{
	public interface IMovingPlatform
	{
		public Vector3 Velocity { get; }
	}
}