using UnityEngine;
using System.Collections;



namespace Game.Media
{
	/// <summary>
	/// Audio play.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class AudioPlay : MonoBehaviour
	{
		private float m_fVolume;	//the sound of volume.
		private bool m_fAutoDestory = false;	//is auto destory.

		/// <summary>
		/// Init the specified clip and volume.
		/// </summary>
		/// <param name="clip">Clip.</param>
		/// <param name="volume">Volume.</param>
		public void Init( AudioClip clip , float volume = 1f )
		{
			this.audio.clip = clip;
			this.m_fVolume = volume;
		}

		/// <summary>
		/// Play the specified mute and volume.
		/// </summary>
		/// <param name="mute">If set to <c>true</c> mute.</param>
		/// <param name="volume">Volume.</param>
		public void Play( bool mute , float volume , bool loop = false )
		{
			this.enabled = true;
			this.audio.enabled = true;
			this.audio.mute = mute;
			this.audio.loop = loop;
			this.audio.volume = volume * this.m_fVolume;
		}

		/// <summary>
		/// Changes the volume.
		/// </summary>
		/// <param name="mute">If set to <c>true</c> mute.</param>
		/// <param name="volume">Volume.</param>
		public void ChangeVolume( bool mute , float volume )
		{
			this.audio.mute = mute;
			this.audio.volume = volume * this.m_fVolume;
		}

		/// <summary>
		/// Stop this audio.
		/// </summary>
		public void Stop()
		{
			this.enabled = false;
			this.audio.Stop();
			this.audio.enabled = false;
		}

		/// <summary>
		/// Stop the audio and notice.
		/// </summary>
		public void StopAndNotice()
		{
			Stop();
			//notice.
			if(this.m_fAutoDestory)
			{
				MediaManager.sInstance.RemoveES(this);
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		void Update ()
		{
			if(audio.isPlaying) return;
			if(!audio.enabled)
			{
				StopAndNotice();
			}
		}
	}


}