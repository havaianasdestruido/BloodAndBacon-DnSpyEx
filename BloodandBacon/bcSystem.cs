using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200002F RID: 47
	internal class bcSystem : ParticleSystem
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0004A7AC File Offset: 0x000489AC
		public bcSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0004A7B8 File Offset: 0x000489B8
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "spark2";
			settings.MaxParticles = 13500;
			settings.Duration = TimeSpan.FromSeconds(3.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 3f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0.1f;
			settings.MinColor = new Color(153, 153, 153, 53);
			settings.MaxColor = new Color(255, 255, 255, 205);
			settings.MinRotateSpeed = 12f;
			settings.MaxRotateSpeed = 25f;
			settings.MinStartSize = 0.8f;
			settings.MaxStartSize = 2.5f;
			settings.MinEndSize = 0.1f;
			settings.MaxEndSize = 0.2f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
