using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000185 RID: 389
	internal class FireSettingsSystem : ParticleSystem
	{
		// Token: 0x06000E57 RID: 3671 RVA: 0x00403BF9 File Offset: 0x00401DF9
		public FireSettingsSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00403C04 File Offset: 0x00401E04
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "fire";
			settings.MaxParticles = 2400;
			settings.Duration = TimeSpan.FromSeconds(2.0);
			settings.DurationRandomness = 1f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 15f;
			settings.MinVerticalVelocity = -10f;
			settings.MaxVerticalVelocity = 10f;
			settings.Gravity = new Vector3(0f, 15f, 0f);
			settings.EndVelocity = 1f;
			settings.MinColor = new Color(10, 255, 255, 255);
			settings.MaxColor = new Color(40, 255, 255, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 5f;
			settings.MaxStartSize = 10f;
			settings.MinEndSize = 10f;
			settings.MaxEndSize = 40f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
