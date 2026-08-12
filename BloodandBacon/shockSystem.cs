using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000018 RID: 24
	internal class shockSystem : ParticleSystem
	{
		// Token: 0x06000120 RID: 288 RVA: 0x0002C080 File Offset: 0x0002A280
		public shockSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0002C08C File Offset: 0x0002A28C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "spire";
			settings.MaxParticles = 26000;
			settings.Duration = TimeSpan.FromSeconds(1.5);
			settings.DurationRandomness = 0.1f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(80, 80, 245, 150);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 25f;
			settings.MaxStartSize = 45f;
			settings.MinEndSize = 8f;
			settings.MaxEndSize = 12f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
