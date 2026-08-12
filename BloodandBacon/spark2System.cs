using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000077 RID: 119
	internal class spark2System : ParticleSystem
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x000E689C File Offset: 0x000E4A9C
		public spark2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000E68A8 File Offset: 0x000E4AA8
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "sparke2";
			settings.MaxParticles = 6000;
			settings.Duration = TimeSpan.FromSeconds(1.5);
			settings.DurationRandomness = 0.2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, -100f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(245, 155, 155, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 5f;
			settings.MaxRotateSpeed = 15f;
			settings.MinStartSize = 0.5f;
			settings.MaxStartSize = 3f;
			settings.MinEndSize = 0.1f;
			settings.MaxEndSize = 0.1f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
