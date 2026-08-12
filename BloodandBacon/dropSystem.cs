using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000015 RID: 21
	internal class dropSystem : ParticleSystem
	{
		// Token: 0x0600011A RID: 282 RVA: 0x0002BDFB File Offset: 0x00029FFB
		public dropSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0002BE08 File Offset: 0x0002A008
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "blood2";
			settings.MaxParticles = 15000;
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
