using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200004F RID: 79
	internal class lineSystem : ParticleSystem
	{
		// Token: 0x06000322 RID: 802 RVA: 0x000CEEF1 File Offset: 0x000CD0F1
		public lineSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000CEEFC File Offset: 0x000CD0FC
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer2";
			settings.MaxParticles = 1200;
			settings.Duration = TimeSpan.FromSeconds(2.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 0f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(155, 155, 155, 255);
			settings.MaxColor = new Color(155, 155, 155, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 10f;
			settings.MaxStartSize = 10f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 2f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
