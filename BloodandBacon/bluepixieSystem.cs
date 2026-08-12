using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200015C RID: 348
	internal class bluepixieSystem : ParticleSystem
	{
		// Token: 0x06000CAB RID: 3243 RVA: 0x00375525 File Offset: 0x00373725
		public bluepixieSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00375530 File Offset: 0x00373730
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "bluefire";
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
