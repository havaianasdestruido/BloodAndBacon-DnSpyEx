using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000012 RID: 18
	public class ParticleSettings
	{
		// Token: 0x040005B3 RID: 1459
		public string TextureName;

		// Token: 0x040005B4 RID: 1460
		public int MaxParticles = 100;

		// Token: 0x040005B5 RID: 1461
		public TimeSpan Duration = TimeSpan.FromSeconds(1.0);

		// Token: 0x040005B6 RID: 1462
		public float DurationRandomness;

		// Token: 0x040005B7 RID: 1463
		public float EmitterVelocitySensitivity = 1f;

		// Token: 0x040005B8 RID: 1464
		public float MinHorizontalVelocity;

		// Token: 0x040005B9 RID: 1465
		public float MaxHorizontalVelocity;

		// Token: 0x040005BA RID: 1466
		public float MinVerticalVelocity;

		// Token: 0x040005BB RID: 1467
		public float MaxVerticalVelocity;

		// Token: 0x040005BC RID: 1468
		public Vector3 Gravity = Vector3.Zero;

		// Token: 0x040005BD RID: 1469
		public float EndVelocity = 1f;

		// Token: 0x040005BE RID: 1470
		public Color MinColor = Color.White;

		// Token: 0x040005BF RID: 1471
		public Color MaxColor = Color.White;

		// Token: 0x040005C0 RID: 1472
		public float MinRotateSpeed;

		// Token: 0x040005C1 RID: 1473
		public float MaxRotateSpeed;

		// Token: 0x040005C2 RID: 1474
		public float MinStartSize = 100f;

		// Token: 0x040005C3 RID: 1475
		public float MaxStartSize = 100f;

		// Token: 0x040005C4 RID: 1476
		public float MinEndSize = 100f;

		// Token: 0x040005C5 RID: 1477
		public float MaxEndSize = 100f;

		// Token: 0x040005C6 RID: 1478
		public BlendState BlendState = BlendState.AlphaBlend;
	}
}
