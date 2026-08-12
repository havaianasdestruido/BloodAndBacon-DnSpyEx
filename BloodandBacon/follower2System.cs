using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000063 RID: 99
	internal class follower2System : ParticleSystem
	{
		// Token: 0x06000390 RID: 912 RVA: 0x000D882D File Offset: 0x000D6A2D
		public follower2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000D8838 File Offset: 0x000D6A38
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "diamond";
			settings.MaxParticles = 6000;
			settings.Duration = TimeSpan.FromSeconds(2.5);
			settings.DurationRandomness = 0.3f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 11f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 11f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 11f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(85, 85, 85, 85);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0.3f;
			settings.MinStartSize = 3f;
			settings.MaxStartSize = 13f;
			settings.MinEndSize = 25f;
			settings.MaxEndSize = 35f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
