using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000183 RID: 387
	internal class tracerx2System : ParticleSystem
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x00403991 File Offset: 0x00401B91
		public tracerx2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0040399C File Offset: 0x00401B9C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer";
			settings.MaxParticles = 12000;
			settings.Duration = TimeSpan.FromSeconds(30.0);
			settings.DurationRandomness = 0f;
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
			settings.MinEndSize = 12f;
			settings.MaxEndSize = 12f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
