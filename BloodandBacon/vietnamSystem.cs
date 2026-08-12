using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000094 RID: 148
	internal class vietnamSystem : ParticleSystem
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x00135A53 File Offset: 0x00133C53
		public vietnamSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00135A60 File Offset: 0x00133C60
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "sparke2";
			settings.MaxParticles = 9500;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, -20f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 108, 108, 200);
			settings.MaxColor = new Color(255, 205, 205, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 30f;
			settings.MaxStartSize = 45f;
			settings.MinEndSize = 8f;
			settings.MaxEndSize = 8f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
