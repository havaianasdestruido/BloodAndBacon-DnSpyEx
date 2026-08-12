using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000186 RID: 390
	internal class cloud2System : ParticleSystem
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x00403D27 File Offset: 0x00401F27
		public cloud2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00403D34 File Offset: 0x00401F34
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer";
			settings.MaxParticles = 39000;
			settings.Duration = TimeSpan.FromSeconds(40.0);
			settings.DurationRandomness = 4f;
			settings.EmitterVelocitySensitivity = 3f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(170, 170, 170, 170);
			settings.MaxColor = new Color(170, 170, 170, 170);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 8f;
			settings.MaxStartSize = 8f;
			settings.MinEndSize = 40f;
			settings.MaxEndSize = 40f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
