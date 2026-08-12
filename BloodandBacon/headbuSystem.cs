using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000032 RID: 50
	internal class headbuSystem : ParticleSystem
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0004B1AC File Offset: 0x000493AC
		public headbuSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0004B1B8 File Offset: 0x000493B8
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "bloodgrey_bu";
			settings.MaxParticles = 90000;
			settings.Duration = TimeSpan.FromSeconds(1.4);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -50f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(60, 60, 60, 35);
			settings.MaxColor = new Color(205, 205, 205, 70);
			settings.MinRotateSpeed = 1.4f;
			settings.MaxRotateSpeed = 1.5f;
			settings.MinStartSize = 4f;
			settings.MaxStartSize = 11f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 3f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
