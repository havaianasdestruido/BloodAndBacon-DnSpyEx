using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200008E RID: 142
	internal class gunsmokeSystem : ParticleSystem
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x0011BBF6 File Offset: 0x00119DF6
		public gunsmokeSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0011BC00 File Offset: 0x00119E00
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "mistmini";
			settings.MaxParticles = 25;
			settings.Duration = TimeSpan.FromSeconds(1.1);
			settings.DurationRandomness = 0.2f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, 20f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(155, 155, 255, 170);
			settings.MaxColor = new Color(255, 255, 255, 190);
			settings.MinRotateSpeed = -1.35f;
			settings.MaxRotateSpeed = 2.35f;
			settings.MinStartSize = 1f;
			settings.MaxStartSize = 10f;
			settings.MinEndSize = 35f;
			settings.MaxEndSize = 45f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
