using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000055 RID: 85
	internal class rockSystem : ParticleSystem
	{
		// Token: 0x06000341 RID: 833 RVA: 0x000D17BF File Offset: 0x000CF9BF
		public rockSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000D17CC File Offset: 0x000CF9CC
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "rock1";
			settings.MaxParticles = 35000;
			settings.Duration = TimeSpan.FromSeconds(5.0);
			settings.DurationRandomness = 0.5f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, -650f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(95, 135, 95, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = -12f;
			settings.MaxRotateSpeed = 12f;
			settings.MinStartSize = 5f;
			settings.MaxStartSize = 28f;
			settings.MinEndSize = 1f;
			settings.MaxEndSize = 6f;
			settings.BlendState = BlendState.AlphaBlend;
		}
	}
}
