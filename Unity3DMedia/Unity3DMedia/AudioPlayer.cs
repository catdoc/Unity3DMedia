using UnityEngine;
using System.Collections;


//	AudioPlayer.cs
//	Author: Lu Zexi
//	2014-07-04


namespace Game.Media
{
	/// <summary>
	/// Audio player.
	/// </summary>
	[RequireComponent(typeof(AudioSource))]
	public class AudioPlayer : MonoBehaviour
	{
		private float m_fVolume;	//the sound of volume.

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
			this.gameObject.SetActive(true);
			this.enabled = true;
			this.audio.enabled = true;
			this.audio.mute = mute;
			this.audio.loop = loop;
			this.audio.volume = volume * this.m_fVolume;
			this.audio.Play();
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
			this.gameObject.SetActive(false);
		}

		/// <summary>
		/// Stop the audio and notice.
		/// </summary>
		public void StopAndNotice()
		{
			Stop();
			MediaMgr.sInstance.RemoveAudioPlayer(this);
		}

		/// <summary>
		/// Update is called once per frame
		/// </summary>
		void Update ()
		{
			if(audio.isPlaying) return;
			StopAndNotice();
		}
	}


}