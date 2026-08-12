using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200010E RID: 270
	internal class debrisBlood2System : ParticleSystem
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00253543 File Offset: 0x00251743
		public debrisBlood2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00253550 File Offset: 0x00251750
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "bloodblue2";
			settings.MaxParticles = 150000;
			settings.Duration = TimeSpan.FromSeconds(20.0);
			settings.DurationRandomness = 3f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(125, 125, 125, 255);
			settings.MaxColor = new Color(150, 150, 150, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 12f;
			settings.MaxStartSize = 12f;
			settings.MinEndSize = 0.5f;
			settings.MaxEndSize = 0.5f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
