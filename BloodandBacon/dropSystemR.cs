using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000024 RID: 36
	internal class dropSystemR : ParticleSystem
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00031606 File Offset: 0x0002F806
		public dropSystemR(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00031610 File Offset: 0x0002F810
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "blood2";
			settings.MaxParticles = 4000;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 1.2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -130f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(100, 100, 100, 255);
			settings.MaxColor = new Color(200, 200, 200, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 2.4f;
			settings.MaxStartSize = 7.5f;
			settings.MinEndSize = 0.1f;
			settings.MaxEndSize = 0.5f;
			settings.BlendState = BlendState.AlphaBlend;
		}
	}
}
