﻿namespace Dimmer_MAUI.Utilities.IServices;
public interface IPlaybackUtilsService
{

    IObservable<ObservableCollection<SongModelView>> NowPlayingSongs { get; } //to display songs in queue
    IObservable<ObservableCollection<SongModelView>> BackEndShufflableSongsQueue { get; } 
    IObservable<ObservableCollection<SongModelView>> SecondaryQueue { get; } // This will be used to show songs from playlist
    IObservable<ObservableCollection<SongModelView>> TertiaryQueue { get; } //This will be used to show songs loaded externally
    Task<bool> PlaySongAsync(SongModelView song, int CurrentQueue = 0, ObservableCollection<SongModelView>? SecQueueSongs = null, 
        double lastPosition = 0, int repeatMode = 0, 
        int repeatMaxCount = 0, 
        bool IsFromPreviousOrNext = false, AppState CurrentAppState = AppState.OnForeGround); //to play song
    Task<bool> PlayNextSongAsync(); //to play next song
    Task<bool> PlayPreviousSongAsync(); //to play previous song
    Task<bool> StopSongAsync(); //to stop song
    Task<bool> PauseResumeSongAsync(double lastPosition, bool isPause=false); //to pause/resume song
    IObservable<MediaPlayerState> PlayerState { get; } //to update play/pause button
    void RemoveSongFromQueue(SongModelView song); //to remove song from queue
    void AddSongToQueue(SongModelView song); //to add song to queue

    int LoadingSongsProgressPercentage { get; }
    SongModelView CurrentlyPlayingSong { get; }
    SongModelView PreviouslyPlayingSong { get; }
    SongModelView NextPlayingSong { get; }
    string TotalSongsSizes { get; }
    string TotalSongsDuration { get; }
    bool IsShuffleOn { get; set; }
    int CurrentRepeatMode { get; set; }
    int CurrentRepeatCount { get; set; }
    IObservable<PlaybackInfo> CurrentPosition { get; } //to read position and update slider
    Task SetSongPosition(double positionInSec); // to set position from slider
    Task<bool> LoadSongsFromFolderAsync(List<string> folderPath);//to load songs from folder
    void ChangeVolume(double newVolumeValue);
    void SearchSong(string songTitleOrArtistName,List<string>? selectedFilters, int Rating); //to search song with title
    void DecreaseVolume();
    void IncreaseVolume();
    void ToggleShuffle(bool isShuffleOn);
    int ToggleRepeatMode();
    void UpdateSongToFavoritesPlayList(SongModelView song);
    int CurrentQueue { get; set; }
    void UpdateCurrentQueue(IList<SongModelView> songs, int QueueNumber = 1);
    Task<bool> PlaySelectedSongsOutsideAppAsync(List<string> filePaths);

    Task DeleteSongFromHomePage(SongModelView song);
    Task MultiDeleteSongFromHomePage(ObservableCollection<SongModelView> songs);
    
    //Playlist Section

    ObservableCollection<PlaylistModelView> AllPlaylists { get; }
    void AddSongToPlayListWithPlayListID(SongModelView song, PlaylistModelView playlistModel);
    
    void RemoveSongFromPlayListWithPlayListID(SongModelView song, ObjectId playlistID);
    
    ObservableCollection<PlaylistModelView> GetAllPlaylists();
    List<SongModelView> GetSongsFromPlaylistID(ObjectId playlistID);
    bool DeletePlaylistThroughID(ObjectId playlistID);
    string SelectedPlaylistName { get; }

    //Artist Section
    ObservableCollection<ArtistModelView> GetAllArtists();
    ObservableCollection<AlbumModelView> GetAllAlbums();
    ObservableCollection<ArtistModelView> AllArtists { get; }
    ObservableCollection<SongModelView> GetAllArtistsAlbumSongsAlbumID(ObjectId albumID);
    ObservableCollection<SongModelView> GetallArtistsSongsByArtistId(ObjectId artistID);
}