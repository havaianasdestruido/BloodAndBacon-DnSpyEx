using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000B3 RID: 179
	internal class greenpixieSystem : ParticleSystem
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x00148AED File Offset: 0x00146CED
		public greenpixieSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00148AF8 File Offset: 0x00146CF8
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "greenfire";
			settings.MaxParticles = 5500;
			settings.Duration = TimeSpan.FromSeconds(12.0);
			settings.DurationRandomness = 1f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 18f;
			settings.MaxStartSize = 18f;
			settings.MinEndSize = 11f;
			settings.MaxEndSize = 17f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
