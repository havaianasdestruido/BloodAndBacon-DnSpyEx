using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000084 RID: 132
	internal class redpixieSystem : ParticleSystem
	{
		// Token: 0x0600049A RID: 1178 RVA: 0x00111B21 File Offset: 0x0010FD21
		public redpixieSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00111B2C File Offset: 0x0010FD2C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "redfire";
			settings.MaxParticles = 4500;
			settings.Duration = TimeSpan.FromSeconds(12.0);
			settings.DurationRandomness = 1f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 18f;
			settings.MaxStartSize = 18f;
			settings.MinEndSize = 11f;
			settings.MaxEndSize = 17f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
