using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000132 RID: 306
	internal class dotSystem2 : ParticleSystem
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x002DFB3A File Offset: 0x002DDD3A
		public dotSystem2(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x002DFB44 File Offset: 0x002DDD44
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "dot";
			settings.MaxParticles = 1200;
			settings.Duration = TimeSpan.FromSeconds(1.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 3f;
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
			settings.MinEndSize = 1f;
			settings.MaxEndSize = 1f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
