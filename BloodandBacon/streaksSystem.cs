using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000AB RID: 171
	internal class streaksSystem : ParticleSystem
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x00147900 File Offset: 0x00145B00
		public streaksSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0014790C File Offset: 0x00145B0C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "trail";
			settings.MaxParticles = 16000;
			settings.Duration = TimeSpan.FromSeconds(0.800000011920929);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(80, 80, 80, 80);
			settings.MaxColor = new Color(80, 80, 80, 80);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 5f;
			settings.MaxStartSize = 5f;
			settings.MinEndSize = 0.1f;
			settings.MaxEndSize = 0.1f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
