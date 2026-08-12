using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200015B RID: 347
	internal class explode3System : ParticleSystem
	{
		// Token: 0x06000CA9 RID: 3241 RVA: 0x003753F1 File Offset: 0x003735F1
		public explode3System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x003753FC File Offset: 0x003735FC
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "raindrop";
			settings.MaxParticles = 3000;
			settings.Duration = TimeSpan.FromSeconds(4.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 655f;
			settings.MinVerticalVelocity = -155f;
			settings.MaxVerticalVelocity = 655f;
			settings.Gravity = new Vector3(0f, -160f, 0f);
			settings.EndVelocity = 35f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(170, 170, 255, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 88f;
			settings.MaxStartSize = 124f;
			settings.MinEndSize = 6f;
			settings.MaxEndSize = 12f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
