using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200005D RID: 93
	internal class cloversSystem : ParticleSystem
	{
		// Token: 0x06000370 RID: 880 RVA: 0x000D5921 File Offset: 0x000D3B21
		public cloversSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000D592C File Offset: 0x000D3B2C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "clover";
			settings.MaxParticles = 6000;
			settings.Duration = TimeSpan.FromSeconds(8.0);
			settings.DurationRandomness = 1f;
			settings.EmitterVelocitySensitivity = 3f;
			settings.MinHorizontalVelocity = 5f;
			settings.MaxHorizontalVelocity = 5f;
			settings.MinVerticalVelocity = 5f;
			settings.MaxVerticalVelocity = 5f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 5f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 1f;
			settings.MinStartSize = 28f;
			settings.MaxStartSize = 28f;
			settings.MinEndSize = 55f;
			settings.MaxEndSize = 55f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
