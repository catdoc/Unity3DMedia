using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//	MediaManager.cs
//	Author: Lu Zexi
//	2014-07-03


namespace Game.Media
{
	/// <summary>
	/// Media manager.
	/// </summary>
	public class MediaManager : MonoBehaviour
	{
		private const int MAX_SE = 10;
		private const int MAX_ENV = 5;
		private const float FASE_OUT_TIME = 0.5F;

		private AudioPlay m_cBGM;	//BGM

		private LinkedList<AudioPlay> m_lstEnableSE = new LinkedList<AudioPlay>();	//SE enable queue
		private LinkedList<AudioPlay> m_lstEnableENV = new LinkedList<AudioPlay>();	//ENV enable queue

		public float BGM_VOLUME = 1F;	//the volume of the BGM
		public float SE_VOLUME = 1F;	//the volume of the SE
		public float ENV_VOLUME = 1F;	//the volume of the ENV
		public bool MUTE = false;	//the mute

		private static MediaManager s_cInstance;	//the static instance;

		public static MediaManager sInstance
		{
			get
			{
				if(s_cInstance != null )
				{
					s_cInstance = (new GameObject("MediaManager")).AddComponent<MediaManager>();
				}
				return s_cInstance;
			}
		}

		public MediaManager ()
		{
			//
		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		void OnDestroy()
		{
			if( s_cInstance == this )
			{
				s_cInstance = null;
				this.m_cBGM = null;
				this.m_lstEnableSE.Clear();
				this.m_lstEnableENV.Clear();
			}
		}

		/// <summary>
		/// Plaies the background.
		/// </summary>
		/// <returns>The background.</returns>
		/// <param name="clip">Clip.</param>
		public void PlayBGM( AudioClip clip , bool useFade = true )  
		{
			if(this.m_cBGM == null )
			{
				this.m_cBGM = (new GameObject("BGM")).AddComponent<AudioPlay>();
			}
			if( this.m_cBGM.audio.clip == clip ) return;

			if(useFade)
			{
				StartCoroutine(Fadeout(FASE_OUT_TIME , this.m_cBGM.audio ,
				                       () =>{
											this.m_cBGM.Stop();
											this.m_cBGM.Init(clip);
											this.m_cBGM.Play(MUTE,BGM_VOLUME , true);
										}
				                       ));
			}
			else
			{
				this.m_cBGM.Stop();
				this.m_cBGM.Init(clip);
				this.m_cBGM.Play(MUTE , BGM_VOLUME , true);
			}
		}

		/// <summary>
		/// Play the SE.
		/// </summary>
		/// <returns>The S.</returns>
		/// <param name="clip">Clip.</param>
		public AudioPlay PlaySE( AudioClip clip )
		{
			AudioPlay ap = null;
			if( this.m_lstEnableSE.Count > MAX_SE )
			{
				ap = this.m_lstEnableSE.First.Value;
				this.m_lstEnableSE.RemoveFirst();
				ap.Stop();
			}
			else
			{
				ap = (new GameObject("AudioSE")).AddComponent<AudioPlay>();
				ap.transform.parent = this.transform;
			}
			this.m_lstEnableSE.AddLast(ap);
			ap.Init(clip);
			ap.Play(MUTE , SE_VOLUME);
			return ap;
		}

		/// <summary>
		/// Plaies the EN.
		/// </summary>
		/// <returns>The EN.</returns>
		/// <param name="clip">Clip.</param>
		public AudioPlay PlayENV( AudioClip clip )
		{
			AudioPlay ap = null;
			if( this.m_lstEnableENV.Count > MAX_SE )
			{
				ap = this.m_lstEnableENV.First.Value;
				this.m_lstEnableENV.RemoveFirst();
				ap.Stop();
			}
			else
			{
				ap = (new GameObject("AudioENV")).AddComponent<AudioPlay>();
				ap.transform.parent = this.transform;
			}
			this.m_lstEnableENV.AddLast(ap);
			ap.Init(clip);
			ap.Play(MUTE , ENV_VOLUME , true);
			return ap;
		}

		/// <summary>
		/// Changes the volume.
		/// </summary>
		public void ChangeVolume()
		{
			foreach( AudioPlay item in this.m_lstEnableSE )
			{
				item.ChangeVolume(MUTE , SE_VOLUME);
			}
			foreach( AudioPlay item in this.m_lstEnableENV )
			{
				item.ChangeVolume(MUTE , ENV_VOLUME);
			}
		}

		/// <summary>
		/// Removes the ES.
		/// </summary>
		/// <param name="ap">Ap.</param>
		public void RemoveES( AudioPlay ap )
		{
			this.m_lstEnableSE.Remove(ap);
		}

		/// <summary>
		/// Stops the EN.
		/// </summary>
		public void StopENV()
		{
			foreach( AudioPlay ap in this.m_lstEnableENV )
			{
				ap.Stop();
			}
			this.m_lstEnableENV.Clear();
		}

		/// <summary>
		/// Fadeout the specified duration, audio and callback.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="audio">Audio.</param>
		/// <param name="callback">Callback.</param>
		private IEnumerator Fadeout(float duration, AudioSource audio, Action callback)
		{
			StopCoroutine("Fadeout");
			float currentTime = 0.0f;
			float firstVol = audio.volume;
			while (duration > currentTime)
			{
				currentTime += Time.fixedDeltaTime;
				audio.volume = Mathf.Lerp(firstVol, 0.0f, currentTime/duration );
				yield return new WaitForSeconds(Time.fixedDeltaTime);
			}
			if (callback != null) {
				callback();
			}
		}

		/// <summary>
		/// Play the movie.
		/// </summary>
		/// <param name="path">Path.</param>
		/// <param name="callback">Callback.</param>
		/// <param name="color">Color.</param>
		/// <param name="controlMode">Control mode.</param>
		/// <param name="scalingMode">Scaling mode.</param>
		public void PlayMovie(
			string path , System.Action callback , Color bgcolor ,
			FullScreenMovieControlMode controlMode = FullScreenMovieControlMode.Full,
			FullScreenMovieScalingMode scalingMode = FullScreenMovieScalingMode.AspectFit
			)
		{
			Handheld.PlayFullScreenMovie(path , bgcolor , controlMode , scalingMode);

			if(callback != null )
			{
				callback();
			}
		}
	}
}

