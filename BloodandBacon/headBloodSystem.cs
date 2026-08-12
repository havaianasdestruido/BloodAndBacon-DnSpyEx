using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200015F RID: 351
	internal class headBloodSystem : ParticleSystem
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x00376694 File Offset: 0x00374894
		public headBloodSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x003766A0 File Offset: 0x003748A0
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "bloodgrey";
			settings.MaxParticles = 80000;
			settings.Duration = TimeSpan.FromSeconds(1.4);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, -10f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(125, 125, 125, 255);
			settings.MaxColor = new Color(125, 125, 125, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 10f;
			settings.MaxStartSize = 12f;
			settings.MinEndSize = 1f;
			settings.MaxEndSize = 1f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
