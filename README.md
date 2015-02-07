Unity3DSound
============

# Unity3D音效功能封装
----------------------

# 是什么(What)
* 本Repository是对unity3d声音部分的封装
* 为了让声音部分使用起来更简单，不做重复的声音工作

# 内容介绍(Introduce)
* 封装后把声音分为：背景音乐，音效(中长的音乐)，声音(短促的音乐)
* 封装后主要用于游戏项目中的背景音乐，音效，声音的播放。
* 有播放mp4视频的功能，虽然这个播放视频的功能还不够完善还在改进中，但已经能使用。

# 如何使用(How To)
* 命名空间 Game.Media . 使用需要引入命名空间: using Game.Media
* MediaMgr是 媒体管理类，也是使用的接口。
* MediaMgr.BGM_VOLUME 为背景音乐的音量
* MediaMgr.ENV_VOLUME 为音效的音量
* MediaMgr.SE_VOLUME 为声音的音量
* MediaMgr.MUTE 是否静音

# 接口( API )
    AudioPlayer MediaMgr.sInstance.PlayBGM( string soundName ) 播放resource里的背景音乐
    AudioPlayer MediaMgr.sInstance.PlayBGM( AudioClip clip ) 播放指定背景音乐
    MediaMgr.sInstance.StopBGM() 停止播放背景音乐

    AudioPlayer MediaMgr.sInstance.PlaySE( string soundName ) 播放resource里的声音
    AudioPlayer MediaMgr.sInstance.PlaySE( AudioClip clip ) 播放指定声音

    AudioPlayer MediaMgr.sInstance.PlayENV( string soundName ) 播放resource里的音乐
    AudioPlayer MediaMgr.sInstance.PlayENV( AudioClip clip ) 播放指定音乐
    MediaMgr.sInstance.StopENV( AudioPlayer player ) 停止播放指定音乐
    MediaMgr.sInstance.StopENV() 停止播放所有音乐

    播放AssetStream里的mp4
    void PlayMovie(string path , System.Action callback , Color bgcolor ,
            FullScreenMovieControlMode controlMode = FullScreenMovieControlMode.Full,
            FullScreenMovieScalingMode scalingMode = FullScreenMovieScalingMode.AspectFit)



# 博客站点：http://www.luzexi.com