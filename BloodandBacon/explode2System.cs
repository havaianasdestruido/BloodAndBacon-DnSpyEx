using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000AE RID: 174
	internal class explode2System : ParticleSystem
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00147C81 File Offset: 0x00145E81
		public explode2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00147C8C File Offset: 0x00145E8C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "star";
			settings.MaxParticles = 3000;
			settings.Duration = TimeSpan.FromSeconds(2.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 125f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 155f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 15f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 3f;
			settings.MinStartSize = 5f;
			settings.MaxStartSize = 14f;
			settings.MinEndSize = 60f;
			settings.MaxEndSize = 120f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
