using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200005C RID: 92
	internal class explode1System : ParticleSystem
	{
		// Token: 0x0600036E RID: 878 RVA: 0x000D57ED File Offset: 0x000D39ED
		public explode1System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000D57F8 File Offset: 0x000D39F8
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "diamond";
			settings.MaxParticles = 3000;
			settings.Duration = TimeSpan.FromSeconds(2.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 1f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 55f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 55f;
			settings.Gravity = new Vector3(0f, -150f, 0f);
			settings.EndVelocity = 25f;
			settings.MinColor = new Color(255, 255, 255, 255);
			settings.MaxColor = new Color(255, 255, 255, 255);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 88f;
			settings.MaxStartSize = 124f;
			settings.MinEndSize = 6f;
			settings.MaxEndSize = 12f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
