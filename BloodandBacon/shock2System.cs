using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200005F RID: 95
	internal class shock2System : ParticleSystem
	{
		// Token: 0x06000374 RID: 884 RVA: 0x000D5B83 File Offset: 0x000D3D83
		public shock2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000D5B90 File Offset: 0x000D3D90
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "spire2";
			settings.MaxParticles = 24000;
			settings.Duration = TimeSpan.FromSeconds(1.600000023841858);
			settings.DurationRandomness = 0.1f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(120, 110, 105, 120);
			settings.MaxColor = new Color(255, 255, 255, 205);
			settings.MinRotateSpeed = 8f;
			settings.MaxRotateSpeed = 17f;
			settings.MinStartSize = 25f;
			settings.MaxStartSize = 40f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 6f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
