using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200006D RID: 109
	internal class ProjectileTrailSettingsSystem : ParticleSystem
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x000E37BB File Offset: 0x000E19BB
		public ProjectileTrailSettingsSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000E37C8 File Offset: 0x000E19C8
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "smoke";
			settings.MaxParticles = 1000;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 1.5f;
			settings.EmitterVelocitySensitivity = 0.1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 1f;
			settings.MinVerticalVelocity = -1f;
			settings.MaxVerticalVelocity = 1f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 1f;
			settings.MinColor = new Color(255, 64, 96, 128);
			settings.MaxColor = new Color(128, 255, 255, 255);
			settings.MinRotateSpeed = -4f;
			settings.MaxRotateSpeed = 4f;
			settings.MinStartSize = 2f;
			settings.MaxStartSize = 4f;
			settings.MinEndSize = 5f;
			settings.MaxEndSize = 15f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
