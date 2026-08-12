using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000062 RID: 98
	internal class followerSystem : ParticleSystem
	{
		// Token: 0x0600038E RID: 910 RVA: 0x000D8703 File Offset: 0x000D6903
		public followerSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000D8710 File Offset: 0x000D6910
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "star";
			settings.MaxParticles = 6000;
			settings.Duration = TimeSpan.FromSeconds(2.5);
			settings.DurationRandomness = 0.3f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 11f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 11f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 11f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(85, 85, 85, 85);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 3f;
			settings.MaxStartSize = 13f;
			settings.MinEndSize = 25f;
			settings.MaxEndSize = 35f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
