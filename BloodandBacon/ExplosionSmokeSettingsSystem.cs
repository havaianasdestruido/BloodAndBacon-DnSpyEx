using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000AD RID: 173
	internal class ExplosionSmokeSettingsSystem : ParticleSystem
	{
		// Token: 0x0600061F RID: 1567 RVA: 0x00147B4B File Offset: 0x00145D4B
		public ExplosionSmokeSettingsSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00147B58 File Offset: 0x00145D58
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "smoke";
			settings.MaxParticles = 200;
			settings.Duration = TimeSpan.FromSeconds(4.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 50f;
			settings.MinVerticalVelocity = -10f;
			settings.MaxVerticalVelocity = 50f;
			settings.Gravity = new Vector3(0f, -20f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 211, 211, 211);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = -2f;
			settings.MaxRotateSpeed = 2f;
			settings.MinStartSize = 10f;
			settings.MaxStartSize = 10f;
			settings.MinEndSize = 100f;
			settings.MaxEndSize = 200f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
