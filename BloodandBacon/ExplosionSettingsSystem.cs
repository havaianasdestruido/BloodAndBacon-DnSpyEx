using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000070 RID: 112
	internal class ExplosionSettingsSystem : ParticleSystem
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x000E3B49 File Offset: 0x000E1D49
		public ExplosionSettingsSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000E3B54 File Offset: 0x000E1D54
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "explosion";
			settings.MaxParticles = 100;
			settings.Duration = TimeSpan.FromSeconds(2.0);
			settings.DurationRandomness = 1f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 20f;
			settings.MaxHorizontalVelocity = 30f;
			settings.MinVerticalVelocity = -20f;
			settings.MaxVerticalVelocity = 20f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 128, 128, 128);
			settings.MaxColor = new Color(255, 169, 169, 169);
			settings.MinRotateSpeed = -1f;
			settings.MaxRotateSpeed = 1f;
			settings.MinStartSize = 10f;
			settings.MaxStartSize = 10f;
			settings.MinEndSize = 100f;
			settings.MaxEndSize = 200f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
