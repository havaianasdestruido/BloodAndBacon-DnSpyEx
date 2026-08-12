using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000B4 RID: 180
	internal class follower3System : ParticleSystem
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x00148C21 File Offset: 0x00146E21
		public follower3System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00148C2C File Offset: 0x00146E2C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "horseshoe";
			settings.MaxParticles = 6000;
			settings.Duration = TimeSpan.FromSeconds(2.5);
			settings.DurationRandomness = 0.2f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 10f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 10f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 10f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(85, 85, 85, 85);
			settings.MinRotateSpeed = 0.5f;
			settings.MaxRotateSpeed = 3f;
			settings.MinStartSize = 3f;
			settings.MaxStartSize = 13f;
			settings.MinEndSize = 25f;
			settings.MaxEndSize = 35f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
