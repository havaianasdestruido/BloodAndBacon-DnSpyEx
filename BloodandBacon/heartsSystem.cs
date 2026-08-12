using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200006F RID: 111
	internal class heartsSystem : ParticleSystem
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x000E3A15 File Offset: 0x000E1C15
		public heartsSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000E3A20 File Offset: 0x000E1C20
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "heart";
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
			settings.MinRotateSpeed = 0.3f;
			settings.MaxRotateSpeed = 0.8f;
			settings.MinStartSize = 18f;
			settings.MaxStartSize = 28f;
			settings.MinEndSize = 42f;
			settings.MaxEndSize = 55f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
